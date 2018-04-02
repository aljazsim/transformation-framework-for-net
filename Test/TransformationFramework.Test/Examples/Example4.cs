using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformationFramework;

namespace TransformationFramework.Test.Examples
{
    public sealed class Example4 : Transformable
    {
        [TransformToUpperCase(TransformationPriority = 0)]
        [TransformToLowerCase(TransformationPriority = 1)]
        public string Property1
        {
            get;
            set;
        }

        [TransformToLowerCase(TransformationPriority = 1)]
        [TransformToUpperCase(TransformationPriority = 0)]
        public string Property2
        {
            get;
            set;
        }
    }
}