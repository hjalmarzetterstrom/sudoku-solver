using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_Solver
{
    class Point : IEqualityComparer<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public List<int> Options { get; set; }
        public int numOfOptions { get { return Options.Count(); } }

        public Point()
        {
            Options = new List<int>();
        }

        #region IEqual
        public static bool operator ==(Point point1, Point point2)
        {
            if (Object.ReferenceEquals(point1, point2))
                return true;
            if ((object)point1 == null || (object)point2 == null)
                return false;

            bool xEqual = point1.X == point2.X;
            bool yEqual = point1.Y == point2.Y;
            return xEqual && yEqual;
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }

        public bool Equals(Point x, Point y)
        {
            return x == y;
        }

        public int GetHashCode(Point obj)
        {
            return X ^ Y ^ Value;
        }
        #endregion
    }

    struct SubBoard : IEqualityComparer<Point>
    {
        public Point[] Points { get; set; }
        public Point PositionOnBoard { get; set; }
        public Point ActivePoint { get; set; }
        public bool HasActivePoint { get { return ActivePoint != null; } }
        #region IEqual
        public static bool operator ==(SubBoard sub1, SubBoard sub2)
        {
            bool xEqual = sub1.PositionOnBoard.X == sub2.PositionOnBoard.X;
            bool yEqual = sub1.PositionOnBoard.Y == sub2.PositionOnBoard.Y;
            return xEqual && yEqual;
        }

        public static bool operator !=(SubBoard sub1, SubBoard sub2)
        {
            return !(sub1 == sub2);
        }

        public bool Equals(Point x, Point y)
        {
            return x == y;
        }

        public int GetHashCode(Point obj)
        {
            return base.GetHashCode();
        }
        #endregion
    }

    class Sudoku
    {
        #region Props/Fields
        public delegate void Progress(Point[] board);
        public event Progress ShowProgress;
        public Point[] Board { get; set; }
        private Point[] SortedBoard { get { return SortByOptions(); } }
        private Stack<Point[]> _altBoards { get; set; }
        private SubBoard[] SubBoards
        {
            get
            {
                SubBoard[] newCollection = new SubBoard[9];
                for (int i = 0, x = 0; x < 9; x += 3)
                {
                    for (int y = 0; y < 9; i++, y += 3)
                    {
                        newCollection[i] = GetSub(new Point { X = x, Y = y });
                    }
                }
                return newCollection;
            }
        }
        public bool Correct { get { return CorrectGame(); } }
        public bool Finished { get { return Correct && !HasEmptyCells(); } }
        private bool _unsolvable;
        Queue<Point> CellQueue; 
        #endregion

        public Sudoku(int[][] board)
        {
            _unsolvable = false;
            _altBoards = new Stack<Point[]>();
            Board = new Point[81];

            for (int i = 0, j = 0; i < 9; i++)
            {
                for (int k = 0; k < 9; k++, j++)
                {
                    Board[j] = new Point { X = k, Y = i, Value = board[i][k] };
                }
            }

            UpdateOptions();
        }
        
        #region Solve
        public void Solve()
        {
            Point emptyMatch = GetNextEmptyCell();
            if (emptyMatch == null) return;

            int[] numbers = SortByCommon();

            foreach (int i in emptyMatch.Options)
            {
                if (!ZoneContains(emptyMatch, i) && CheckAvailability(emptyMatch, i))
                {
                    UpdatePoint(emptyMatch, i);
                    _unsolvable = false;
                    Reset();
                    break;
                }
            }

            Solve();

            if (!Finished)
            {
                Reset();
                UpdateOptions();

                CellQueue = new Queue<Point>();
                foreach (Point p in SortedBoard.Where(x => x.Value < 0))
                {
                    CellQueue.Enqueue(p);
                }

                Crossroads();
            }
        }

        private void Crossroads()
        {
            if (Finished || CellQueue.Count == 0) return;

            _altBoards.Push(CreateClone(Board));
            Point currentCell = CellQueue.Dequeue();
            foreach (int i in currentCell.Options)
            {
                if (Finished) break;
                else Board = CreateClone(_altBoards.Peek());

                UpdatePoint(currentCell, i);

                SolveCrossroad();
            }

            if (!Finished)
                Board = _altBoards.Pop();

            Crossroads();
        }

        private void SolveCrossroad()
        {
            Point emptyMatch = GetNextEmptyCell();
            if (emptyMatch == null) return;

            int[] numbers = SortByCommon();

            foreach (int i in emptyMatch.Options)
            {
                if (!ZoneContains(emptyMatch, i) && CheckAvailability(emptyMatch, i))
                {
                    UpdatePoint(emptyMatch, i);

                    _unsolvable = false;
                    Reset();
                    break;
                }
            }

            SolveCrossroad();
        }
        #endregion

        #region Contains

        private bool ZoneContains(Point centerPoint, int i)
        {
            bool horizontalContains = HorizontalContains(centerPoint, i);
            bool verticalContains = VerticalContains(centerPoint, i);
            bool subContains = SubContains(centerPoint, i);

            return horizontalContains || verticalContains || subContains;
        }

        private bool HorizontalContains(Point centerPoint, int i)
        {
            return Board.Where(x => x.X == centerPoint.X && x != centerPoint).Select(x => x.Value).Contains(i);
        }

        private bool VerticalContains(Point centerPoint, int i)
        {
            return Board.Where(x => x.Y == centerPoint.Y && x != centerPoint).Select(x => x.Value).Contains(i);
        }

        private bool SubContains(Point centerPoint, int i)
        {
            SubBoard sub = GetSub(centerPoint);
            return sub.Points.Where(x => x != centerPoint).Select(x => x.Value).Contains(i);
        }

        private bool SubContains(SubBoard sub, int i)
        {
            return sub.Points.Select(x => x.Value).Contains(i);
        }

        private SubBoard GetSub(Point point)
        {
            Point[] subPoints = new Point[9];
            Point inSubPoint = GetPositionOfPointInSub(point);
            Point subPosition = GetPositionOfSubOnBoard(point);
            int horizontalStart = -inSubPoint.X + point.X;
            int horizontalStop = 2 - inSubPoint.X + point.X;
            int verticalStart = -inSubPoint.Y + point.Y;
            int verticalStop = 2 - inSubPoint.Y + point.Y;
            int subLenght = 0;

            for (int j = horizontalStart; j <= horizontalStop; j++)
            {
                for (int i = verticalStart; i <= verticalStop; i++, subLenght++)
                {
                    subPoints[subLenght] = Board.First(x => x.X == j && x.Y == i);
                }
            }

            return new SubBoard { Points = subPoints, PositionOnBoard = subPosition, ActivePoint = inSubPoint };
        }

        private SubBoard[] GetNeighboringSubs(SubBoard sub)
        {
            return SubBoards.Where(x => x.PositionOnBoard.X == sub.PositionOnBoard.X || x.PositionOnBoard.Y == sub.PositionOnBoard.Y).ToArray();
        }

        #endregion

        #region Positions
        private Point GetPositionOfPointInSub(Point CurrentPoint)
        {
            Point newPoint = new Point();
            int currentX = CurrentPoint.X;
            int currentY = CurrentPoint.Y;

            if ((currentX + 1) % 3 == 0)
                newPoint.X = 2;
            else if ((currentX + 2) % 3 == 0)
                newPoint.X = 1;
            else
                newPoint.X = 0;

            if ((currentY + 1) % 3 == 0)
                newPoint.Y = 2;
            else if ((currentY + 2) % 3 == 0)
                newPoint.Y = 1;
            else
                newPoint.Y = 0;

            return newPoint;
        }

        private Point GetPositionOfSubOnBoard(Point CurrentPoint)
        {
            Point newPoint = new Point();
            int currentX = CurrentPoint.X;
            int currentY = CurrentPoint.Y;

            if (currentX > 5)
                newPoint.X = 2;
            else if (currentX > 2)
                newPoint.X = 1;
            else
                newPoint.X = 0;

            if (currentY > 5)
                newPoint.Y = 2;
            else if (currentY > 2)
                newPoint.Y = 1;
            else
                newPoint.Y = 0;

            return newPoint;
        }
        #endregion

        #region AvailabilityChecks
        private bool CheckAvailability(Point point, int i)
        {
            var sub = GetUnavailablePointsInSub(point, i);
            var hor = GetUnavailablePointsInHorizontalRow(point, i);
            var ver = GetUnavailablePointsInVericalRow(point, i);

            bool singleAvailableSub = sub.Count() == 8;
            bool singleAvailableHor = hor.Count() == 8;
            bool singleAvailableVer = ver.Count() == 8;

            return singleAvailableSub || singleAvailableHor || singleAvailableVer;
        }

        private List<Point> GetUnavailablePointsInSub(Point point, int i)
        {
            SubBoard sub = GetSub(point);
            SubBoard[] neighbors = GetNeighboringSubs(sub);
            List<Point> UnavailablePoints = sub.Points.Where(x => x.Value > 0).ToList();

            foreach (var neighbor in neighbors.SkipWhile(x => x == sub))
            {
                if (SubContains(neighbor, i))
                    if (sub.PositionOnBoard.X == neighbor.PositionOnBoard.X)
                        UnavailablePoints.AddRange(sub.Points.Where(x => x.X == neighbor.Points.First(y => y.Value == i).X));
                    else
                        UnavailablePoints.AddRange(sub.Points.Where(x => x.Y == neighbor.Points.First(y => y.Value == i).Y));
            }

            return UnavailablePoints.Distinct().ToList();
        }

        private List<Point> GetUnavailablePointsInVericalRow(Point point, int i)
        {
            return Board.Where(x => x.X == point.X && (x.Value > 0 || ZoneContains(x, i))).Distinct().ToList();
        }

        private List<Point> GetUnavailablePointsInHorizontalRow(Point point, int i)
        {
            return Board.Where(x => x.Y == point.Y && (x.Value > 0 || ZoneContains(x, i))).Distinct().ToList();
        }
        #endregion

        #region Counters
        private int GetCountHorizontalRow(Point point)
        {
            return Board.Count(x => x.Y == point.Y && x.Value > 0);
        }

        private int GetCountVerticalRow(Point point)
        {
            return Board.Count(x => x.X == point.X && x.Value > 0);
        }

        private int GetCountSub(Point point)
        {
            SubBoard sub = GetSub(point);
            return sub.Points.Count(x => x.Value > 0);
        }
        #endregion

        #region META
        private bool CorrectGame()
        {
            foreach (Point p in Board.Where(x => x.Value > 0))
            {
                if (ZoneContains(p, p.Value))
                    return false;
            }
            return true;
        }

        private bool HasEmptyCells()
        {
            return Board.Select(x => x.Value).Contains(-1) || Board.Select(x => x.Value).Contains(0);
        }

        private int CountEmptyCells()
        {
            return Board.Count(x => x.Value < 1);
        }

        private Point[] CreateClone(Point[] array)
        {
            Point[] clone = new Point[Board.Length];
            for (int i = 0; i < Board.Length; i++)
            {
                Point val = new Point { X = Board[i].X, Y = Board[i].Y, Value = Board[i].Value, Options = Board[i].Options };
                clone[i] = val;
            }
            return clone;
        }

        private void Reset()
        {
            foreach (var p in Board.Where(x => x.Value == 0))
            {
                UpdatePoint(p, -1);
            }
        }
        
        private Point GetNextEmptyCell()
        {
            Point next = Board.OrderBy(x => x.numOfOptions).FirstOrDefault(x => x.Value == -1);

            if (next != null)
            {
                UpdatePoint(next, 0);
            }
            else if (Board.Select(x => x.Value).Contains(0))
            {
                Reset();

                next = _unsolvable ? null : GetNextEmptyCell();
                _unsolvable = true;
            }

            return next;
        }
        #endregion

        #region Sort
        private int[] SortByCommon()
        {
            int[][] countArray = new int[9][];
            int[] sortedArray;

            for (int i = 0; i < 9; i++)
            {
                countArray[i] = new int[10];
                countArray[i][0] = i;
                countArray[i][1] = Board.Count(x => x.Value == i);
            }
            var restult = Board.GroupBy(x => x.Value).Select(g => new { Value = g.Key, Count = g.Count() }).Where(w => w.Value > 0 && w.Count != 9).OrderByDescending(o => o.Count);

            sortedArray = restult.Select(x => x.Value).ToArray();

            return sortedArray;
        }

        private Point[] SortByOptions()
        {
            List<Point> newCollection = new List<Point>();
            int[] numbers = SortByCommon();

            foreach (Point p in Board)
            {
                foreach (int number in numbers)
                {
                    if (!ZoneContains(p, number))
                    {
                        p.Options.Add(number);
                    }
                }

                p.Options.Reverse();
                newCollection.Add(p);
            }

            return newCollection.OrderBy(x => x.numOfOptions).ToArray();
        }
        #endregion

        #region Update
        private void UpdateOptions()
        {
            int[] numbers = SortByCommon();
            foreach (Point p in Board)
            {
                foreach (int number in numbers)
                {
                    if (!ZoneContains(p, number))
                    {
                        p.Options.Add(number);
                    }
                }
            }
        }

        private void UpdatePoint(Point point, int value)
        {
            for (int i = 0; i < Board.Length; i++)
            {
                if (Board[i] == point)
                {
                    Board[i].Value = value;
                    break;
                }
            }
            if (value != 0)
                ShowProgress(Board);
        }
        #endregion
    }
}