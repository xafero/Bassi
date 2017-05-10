using System;
using System.Collections.Generic;
using static System.Linq.Dynamic.DynamicExpression;
using static Bassi.Core.LinqExts;

namespace Bassi.Core
{
    public class Config
    {
        public IList<Filter> Filters { get; set; } = new List<Filter>();

        public class Filter
        {
            public string Name { get; set; }
            public string Query { get; set; }

            private readonly Lazy<Func<IHandle, bool>> lambda;
            public Func<IHandle, bool> ToLambda() => lambda.Value;

            public Filter()
            {
                lambda = new Lazy<Func<IHandle, bool>>(() => ParseLambda<IHandle, bool>(Transform(Query)).Compile());
            }
        }
    }
}