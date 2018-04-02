using System.Collections.Generic;

namespace TransformationFramework
{
    /// <summary>
    /// The transformable base class.
    /// </summary>
    /// <seealso cref="TransformationFramework.ITransformable" />
    public abstract class Transformable : ITransformable
    {
        #region Public Methods

        /// <summary>
        /// Gets the active transformation contexts.
        /// </summary>
        /// <returns>
        /// The list of active transformation contexts.
        /// </returns>
        public virtual IEnumerable<string> GetActiveTransformationContexts()
        {
            return new List<string>();
        }

        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="transformationContext">The transformation context.</param>
        public virtual void Transform(string propertyName, object propertyValue, string transformationContext)
        {
            this.TransformAttributes(propertyName, propertyValue, transformationContext);
        }

        #endregion Public Methods
    }
}
