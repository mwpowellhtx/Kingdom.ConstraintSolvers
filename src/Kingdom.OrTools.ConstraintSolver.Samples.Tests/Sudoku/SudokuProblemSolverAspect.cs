using System.Collections.Generic;
using System.Linq;

namespace Kingdom.OrTools.ConstraintSolver.Samples.Sudoku
{
    using Google.OrTools.ConstraintSolver;
    using static Kingdom.OrTools.Samples.Sudoku.Domain;
    using static Kingdom.OrTools.Samples.Sudoku.SudokuPuzzle;

    public class SudokuProblemSolverAspect : ProblemSolverAspectBase<Solver, Solver, IntVar, Constraint, SudokuProblemSolverAspect>
    {
        /// <summary>
        /// Internal Constructor
        /// </summary>
        internal SudokuProblemSolverAspect()
        {
        }

        /// <summary>
        /// Gets the Cells.
        /// </summary>
        public IntVar[,] Cells { get; private set; }

        public override IEnumerable<IntVar> GetVariables(Solver source)
        {
            var s = source;

            Cells = new IntVar[Size, Size];

            for (var i = MinimumValue; i < MaximumValue; i++)
            {
                for (var j = MinimumValue; j < MaximumValue; j++)
                {
                    Cells[i, j] = s.MakeIntVar(MinimumValue + 1, MaximumValue, $"[{i},{j}]");
                }
            }

            foreach (var c in Cells.Flatten())
            {
                yield return c;
            }
        }

        private IEnumerable<IntVar> GetGroup(IEnumerable<int> rows, IEnumerable<int> columns)
        {
            return from i in rows from j in columns select Cells[i, j];
        }

        public override IEnumerable<Constraint> GetConstraints(Solver source)
        {
            var s = source;

            foreach (var cell in Cells.Flatten())
            {
                var c = s.MakeBetweenCt(cell, MinimumValue + 1, MaximumValue);
                s.Add(c);
                yield return c;
            }

            for (var i = MinimumValue; i < MaximumValue; i++)
            {
                var perpendicularIndexes = Enumerable.Range(0, Size).ToArray();
                var rowOrColumnIdx = new[] {i};

                {
                    var vector = new IntVarVector(GetGroup(rowOrColumnIdx, perpendicularIndexes).ToList()).TrackClrObject(this);
                    var c = s.MakeAllDifferent(vector).TrackClrObject(this);
                    s.Add(c);
                    yield return c;
                }

                {
                    var vector = new IntVarVector(GetGroup(perpendicularIndexes, rowOrColumnIdx).ToList()).TrackClrObject(this);
                    var c = s.MakeAllDifferent(vector).TrackClrObject(this);
                    s.Add(c);
                    yield return c;
                }
            }

            for (var i = MinimumValue; i < BlockSize; i++)
            {
                var rowIdx = Enumerable.Range(i * BlockSize, BlockSize).ToArray();

                for (var j = MinimumValue; j < BlockSize; j++)
                {
                    var columnIndexes = Enumerable.Range(j * BlockSize, BlockSize).ToArray();
                    var vector = new IntVarVector(GetGroup(rowIdx, columnIndexes).ToList()).TrackClrObject(this);
                    var c = s.MakeAllDifferent(vector).TrackClrObject(this);
                    s.Add(c);
                    yield return c;
                }
            }
        }

        /* In this case we do not care about any overlapping concerns. We just want to demonstrate
         * the fundamental modeling, constraints, etc. */
    }
}