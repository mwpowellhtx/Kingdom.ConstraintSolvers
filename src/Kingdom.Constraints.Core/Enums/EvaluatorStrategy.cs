﻿using System;

namespace Kingdom.Constraints
{
    using Google.OrTools.ConstraintSolver;

    /// <summary>
#pragma warning disable 1574
    /// This enum is used by <see cref="Solver.MakePhase"/> to specify how to select variables
#pragma warning restore 1574
    /// and values during the search.
    /// 
    /// In <see cref="Solver.MakePhase(IntVarVector, int, int)"/>, corresponding to <see
    /// cref="IntVarStrategy"/> and <see cref="IntValueStrategy"/>; see for more in depth
    /// description. Variables are selected first, and then the associated value.
    ///
    /// In <see cref="Solver.MakePhase(IntVarVector, LongToLong, int)"/>, corresponding to
    /// IndexEvaluator2 and EvaluatorStrategy, the selection is done scanning every pair
    /// {variable, possible value}. The next selected pair is then the best among all
    /// possibilities, i.e. the pair with the smallest evaluation. As this is costly, two options
    /// are offered: static or dynamic evaluation. <see cref="LongToLong"/> is used for
    /// IndexEvaluator2. Whereas the non-descript <see cref="System.Int32"/> is the <see
    /// cref="EvaluatorStrategy"/>.
    /// 
    /// See <see cref="VariableChooser"/> for more background on IndexEvaluator2 (<see
    /// cref="LongToLong"/>).
    /// </summary>
    /// <see cref="!:http://github.com/google/or-tools/blob/792c1358a57469c9948edc004b07262348544f94/src/constraint_solver/constraint_solver.h" />
    /// <see cref="Solver.MakePhase(IntVarVector, int, int)"/>
    /// <see cref="Solver.MakePhase(IntVarVector, LongToLong, int)"/>
    public enum EvaluatorStrategy
    {
        /// <summary>
        /// Pairs are compared at the first call of the selector, and results are cached. Next
        /// calls to the selector use the previous computation, and so are not up-to-date, e.g.
        /// some {variable, value} pairs may not be possible anymore due to propagation since
        /// the first to call.
        /// </summary>
        /// <see cref="Solver.CHOOSE_STATIC_GLOBAL_BEST"/>
        ChooseStaticGlobalBest,

        /// <summary>
        /// Pairs are compared each time a variable is selected. That way all pairs are relevant
        /// and evaluation is accurate. This strategy runs in O(number-of-pairs) at each variable
        /// selection, versus O(1) in the static version.
        /// </summary>
        /// <see cref="Solver.CHOOSE_DYNAMIC_GLOBAL_BEST"/>
        ChooseDynamicGlobalBest
    };

    /// <summary>
    /// Provides some helpful extensions adapting the enumerated values back to the
    /// <see cref="Solver"/>.
    /// </summary>
    public static partial class EnumExtensionMethods
    {
        /// <summary>
        /// Returns the <see cref="System.Int32"/> value corresponding to the
        /// <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this EvaluatorStrategy value)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (value)
            {
                case EvaluatorStrategy.ChooseDynamicGlobalBest:
                    return Solver.CHOOSE_DYNAMIC_GLOBAL_BEST;
                case EvaluatorStrategy.ChooseStaticGlobalBest:
                    return Solver.CHOOSE_STATIC_GLOBAL_BEST;
            }

            var message = string.Format("{0} not currently supported by or-tools", value);
            throw new ArgumentException(message, "value");
        }
    }
}
