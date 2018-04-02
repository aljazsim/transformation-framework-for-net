using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DefensiveProgrammingFramework;

namespace TransformationFramework
{
    /// <summary>
    /// The transformation attribute converting a string to lower case.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class TransformToLowerCaseAttribute : TransformationAttribute
    {
        #region Public Methods

        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The transformed value.</returns>
        public override object Transform(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return value;
            }
            else
            {
                value.MustBeTypeOf(typeof(string));

                return ((string)value).ToLower(Thread.CurrentThread.CurrentCulture);
            }
        }

        #endregion Public Methods
    }
}
