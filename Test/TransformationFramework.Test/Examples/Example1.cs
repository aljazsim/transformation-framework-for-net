using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformationFramework;

namespace TransformationFramework.Test.Examples
{
    public sealed class Example1 : Transformable
    {
        [TransformToUpperCase]
        public string Property1
        {
            get;
            set;
        }

        [TransformToLowerCase]
        public string Property2
        {
            get;
            set;
        }
    }
}