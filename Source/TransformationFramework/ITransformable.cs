using System.Collections.Generic;

namespace TransformationFramework
{
    /// <summary>
    /// The transformabel interface.
    /// </summary>
    public interface ITransformable
    {
        #region Public Methods

        /// <summary>
        /// Gets the active transformation contexts.
        /// </summary>
        /// <returns>The list of active transformation contexts.</returns>
        IEnumerable<string> GetActiveTransformationContexts();

        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="transformationContext">The transformation context.</param>
        void Transform(string propertyName, object propertyValue, string transformationContext);

        #endregion Public Methods
    }
}
