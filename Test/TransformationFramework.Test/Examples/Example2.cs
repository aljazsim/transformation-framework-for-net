using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformationFramework;

namespace TransformationFramework.Test.Examples
{
    public sealed class Example2 : Transformable
    {
        [TransformToUpperCase]
        public int Property1
        {
            get;
            set;
        }
    }
}