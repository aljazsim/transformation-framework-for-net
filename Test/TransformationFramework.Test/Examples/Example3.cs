using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformationFramework;

namespace TransformationFramework.Test.Examples
{
    public sealed class Example3 : Transformable
    {
        [TransformToUpperCase]
        [TransformToLowerCase]
        public string Property1
        {
            get;
            set;
        }

        [TransformToLowerCase]
        [TransformToUpperCase]
        public string Property2
        {
            get;
            set;
        }
    }
}