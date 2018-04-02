using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransformationFramework;

namespace TransformationFramework.Test.Examples
{
    public sealed class Example5 : Transformable
    {
        #region Public Properties

        [TransformToUpperCase]
        public string Property1
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        public override void Transform(string propertyName, object propertyValue, string transformationContext)
        {
            base.Transform(propertyName, propertyValue, transformationContext);

            if (propertyName == nameof(this.Property1))
            {
                if (propertyValue != null)
                {
                    this.Property1 = $"{this.Property1}...";
                }
            }
        }

        #endregion Public Methods
    }
}