using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler;

namespace ContosoUniversity.Misc
{
    public static class MapperExtensions
    {
        // https://lostechies.com/jimmybogard/2014/05/07/projecting-computed-properties-with-linq-and-automapper/
        public static List<TDestination>
            ToList<TDestination>(this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile().ToList();
        }
    }
}