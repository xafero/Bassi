using System;
using System.Collections.Generic;

using static System.Linq.Dynamic.DynamicExpression;

namespace Bassi.Core
{
    public class Config
    {
        public IList<Filter> Filters { get; set; } = new List<Filter>();

        public class Filter
        {
            public string Name { get; set; }
            public string Query { get; set; }

            private readonly Lazy<Func<Handle, bool>> lambda;
            public Func<Handle, bool> ToLambda() => lambda.Value;

            public Filter()
            {
                lambda = new Lazy<Func<Handle, bool>>(() => ParseLambda<Handle, bool>(Query).Compile());
            }
        }
    }
}