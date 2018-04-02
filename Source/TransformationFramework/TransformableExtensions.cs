using System;
using System.Collections.Generic;
using System.Linq;
using DefensiveProgrammingFramework;

namespace TransformationFramework
{
    /// <summary>
    /// The transformable extensions.
    /// </summary>
    public static class TransformableExtensions
    {
        #region Public Methods

        /// <summary>
        /// Transforms the specified property of the specified transformation source.
        /// </summary>
        /// <param name="transformationSource">The transformation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        public static void Transform(this ITransformable transformationSource, string propertyName)
        {
            transformationSource.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            List<string> transformationContexts;
            object propertyValue;

            // get property value
            var properties = ReflectionExtensions.GetProperties(transformationSource.GetType());

            if (properties.TryGetValue(propertyName, out PropertyData propertyData))
            {
                if (propertyData.PropertyInfo.CanRead &&
                    propertyData.PropertyInfo.CanWrite)
                {
                    propertyValue = propertyData.PropertyInfo.GetValue(transformationSource);

                    // get transformation context
                    transformationContexts = new List<string>();
                    transformationContexts.Add(TransformationContext.Default); // always add the default transformation context
                    transformationContexts.AddRange(transformationSource.GetActiveTransformationContexts() ?? Array.Empty<string>()); // add currently active transformation contexts
                    transformationContexts = transformationContexts.Distinct().ToList();

                    foreach (var transformationContext in transformationContexts)
                    {
                        transformationSource.Transform(propertyName, propertyValue, transformationContext);
                    }
                }
            }
        }

        /// <summary>
        /// Transforms the the properties of the specified transformation source.
        /// </summary>
        /// <param name="transformationSource">The transformation source.</param>
        public static void Transform(this ITransformable transformationSource)
        {
            transformationSource.CannotBeNull();

            var propertyNames = ReflectionExtensions.GetProperties(transformationSource.GetType()).Keys;

            foreach (var propertyName in propertyNames)
            {
                transformationSource.Transform(propertyName);
            }
        }

        /// <summary>
        /// Transforms the value of the specified property by using transformation attributes.
        /// </summary>
        /// <param name="transformationSource">The transformation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="transformationContext">The transformation context.</param>
        public static void TransformAttributes(this ITransformable transformationSource, string propertyName, object propertyValue, string transformationContext)
        {
            transformationSource.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            var propertyData = ReflectionExtensions.GetProperties(transformationSource.GetType())[propertyName];
            var propertyInfo = propertyData.PropertyInfo;
            var transformationAttributes = propertyData.TransformationAttributes;

            // perform attribute based transformation
            foreach (var transformationAttribute in transformationAttributes)
            {
                // only matching transformation context gets transformed
                if (transformationAttribute.TransformationContext == transformationContext)
                {
                    // custom tranformations might cause exceptions that are hard to find
                    try
                    {
                        propertyValue = transformationAttribute.Transform(propertyValue);
                        propertyInfo.SetValue(transformationSource, propertyValue);
                    }
                    catch (Exception ex)
                    {
                        throw new TransformationErrorException("Unhandeled transformation exception occured.", transformationAttribute.GetType(), transformationSource.GetType(), propertyName, ex);
                    }
                }
            }
        }

        #endregion Public Methods
    }
}
