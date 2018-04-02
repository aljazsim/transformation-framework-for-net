using System;
using System.Collections.Generic;
using System.Text;
using DefensiveProgrammingFramework;

namespace TransformationFramework
{
    /// <summary>
    /// The transformation exception thrown when there is an error when executing a transformation through a transformation attribute.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class TransformationErrorException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationErrorException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="transformationAttributeType">Type of the transformation attribute.</param>
        /// <param name="transformationSourceType">Type of the transformation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="innerException">The inner exception.</param>
        public TransformationErrorException(string message, Type transformationAttributeType, Type transformationSourceType, string propertyName, Exception innerException)
            : base(message, innerException)
        {
            message.CannotBeNullOrEmpty();
            innerException.CannotBeNull();
            transformationAttributeType.CannotBeNull();
            transformationSourceType.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            this.TransformationAttributeType = transformationAttributeType;
            this.TransformationSourceType = transformationSourceType;
            this.PropertyName = propertyName;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="TransformationErrorException"/> class from being created.
        /// </summary>
        private TransformationErrorException()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName
        {
            get;
        }

        /// <summary>
        /// Gets the type of the transformation attribute.
        /// </summary>
        /// <value>
        /// The type of the transformation attribute.
        /// </value>
        public Type TransformationAttributeType
        {
            get;
        }

        /// <summary>
        /// Gets the type of the transformation source.
        /// </summary>
        /// <value>
        /// The type of the transformation source.
        /// </value>
        public Type TransformationSourceType
        {
            get;
        }

        #endregion Public Properties
    }
}