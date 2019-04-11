﻿namespace Kingdom.OrTools.Sat
{
    /// <summary>
    /// Support Parameters serves as the bridge between first class language level Parameter
    /// framework and the Solver string representation.
    /// </summary>
    public interface ISupportParameters
    {
        /// <summary>
        /// Gets or Sets the String Parameters.
        /// </summary>
        string StringParameters { get; set; }

        // TODO: TBD: identify the parameter strategy, depends on some code generation parsing the solver parameter descriptor.
    }
}