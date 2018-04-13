using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DefensiveProgrammingFramework;

namespace TransformationFramework
{
    /// <summary>
    /// The transformation attribute converting a string to a trimmed string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ToTrimmedStringAttribute : TransformationAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ToTrimmedStringAttribute"/> class.
        /// </summary>
        /// <param name="trimCharacters">The trim characters.</param>
        public ToTrimmedStringAttribute(params char[] trimCharacters)
        {
            this.TrimCharacters = trimCharacters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToTrimmedStringAttribute"/> class.
        /// </summary>
        public ToTrimmedStringAttribute()
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the trim characters.
        /// </summary>
        /// <value>
        /// The trim characters.
        /// </value>
        public char[] TrimCharacters
        {
            get;
        }

        #endregion Public Properties

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

                if (this.TrimCharacters == null ||
                    this.TrimCharacters.Length == 0)
                {
                    return ((string)value).Trim();
                }
                else
                {
                    return ((string)value).Trim(this.TrimCharacters);
                }
            }
        }

        #endregion Public Methods
    }
}
