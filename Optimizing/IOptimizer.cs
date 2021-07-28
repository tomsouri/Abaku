using System;

namespace Optimizing
{
    public interface IOptimizer
    {
        /// <summary>
        /// Substitutes for public constructor.
        /// </summary>
        /// <returns>New instance of IOptimizer.</returns>
        IOptimizer GetOptimizer();
    }
}
