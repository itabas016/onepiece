using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    [PluginFamily(Scope = InstanceScope.Unique)]
    public interface IRandomService
    {
        int Random(int min, int max);

        /// <summary>
        /// Random one value from the candidates.
        /// e.g. 
        ///     A: 10, B: 20;
        ///     Get one from A, B.
        /// </summary>
        /// <param name="candidates"></param>
        /// <returns></returns>
        object Random(Dictionary<object, int> candidates);

        T Random<T>(Dictionary<T, int> candidates);
    }
}
