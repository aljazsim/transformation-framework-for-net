using System;
using System.Collections.Generic;
using System.Text;

namespace TransformationFramework
{
    /// <summary>
    /// The base transformation attribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class TransformationAttribute : Attribute
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationAttribute" /> class.
        /// </summary>
        protected TransformationAttribute()
        {
            this.TransformationContext = TransformationFramework.TransformationContext.Default;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the transformation context.
        /// </summary>
        /// <value>
        /// The transformation context.
        /// </value>
        public string TransformationContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the transformation priority.
        /// </summary>
        /// <value>
        /// The transformation priority.
        /// </value>
        public int TransformationPriority
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The transformed value.</returns>
        public abstract object Transform(object value);

        #endregion Public Methods
    }
}
