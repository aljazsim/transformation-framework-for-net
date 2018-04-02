using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TransformationFramework.Test.Exceptions
{
    [TestClass]
    public class TransformationErrorExceptionTest
    {
        [TestMethod]
        public void TestException()
        {
            try
            {
                throw new TransformationErrorException("test message", typeof(string), typeof(Transformable), "Property1", new InvalidCastException("aaa"));
            }
            catch (TransformationErrorException ex)
            {
                Assert.IsNotNull(ex);
                Assert.AreEqual("test message", ex.Message);
                Assert.AreEqual(typeof(string), ex.TransformationAttributeType);
                Assert.AreEqual(typeof(Transformable), ex.TransformationSourceType);
                Assert.AreEqual("Property1", ex.PropertyName);
                Assert.AreEqual(typeof(InvalidCastException), ex.InnerException.GetType());
                Assert.AreEqual("aaa", ex.InnerException.Message);
            }
        }
    }
}