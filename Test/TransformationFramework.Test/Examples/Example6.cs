using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformationFramework;

namespace TransformationFramework.Test.Examples
{
    public sealed class Example6 : Transformable
    {
        #region Public Properties

        [TransformToUpperCase(TransformationContext = "long")]
        [TransformToLowerCase(TransformationContext = "short")]
        public string Property1
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        public override IEnumerable<string> GetActiveTransformationContexts()
        {
            if (this.Property1 == null)
            {
                return base.GetActiveTransformationContexts();
            }
            else if (this.Property1.Length < 10)
            {
                return new string[] { "short" };
            }
            else
            {
                return new string[] { "long" };
            }
        }

        #endregion Public Methods
    }
}