using System.Collections.Generic;
using System.Reflection;
using DefensiveProgrammingFramework;

namespace TransformationFramework
{
    /// <summary>
    /// Property data.
    /// </summary>
    internal class PropertyData
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyData"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="transformationAttributes">The transformation attributes.</param>
        public PropertyData(PropertyInfo propertyInfo, IEnumerable<TransformationAttribute> transformationAttributes)
            : this()
        {
            propertyInfo.CannotBeNull();
            transformationAttributes.CannotBeNull();

            this.PropertyInfo = propertyInfo;
            this.TransformationAttributes = transformationAttributes;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="PropertyData"/> class from being created.
        /// </summary>
        private PropertyData()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <value>
        /// The property information.
        /// </value>
        public PropertyInfo PropertyInfo
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IEnumerable<TransformationAttribute> TransformationAttributes
        {
            get;
            private set;
        }

        #endregion Public Properties
    }
}
