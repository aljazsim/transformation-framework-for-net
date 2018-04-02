using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransformationFramework;

namespace TransformationFramework.Test
{
    [TestClass]
    public class ToLowerCaseAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void Transform()
        {
            var attribute = new TransformToLowerCaseAttribute();

            Assert.AreEqual(null, attribute.Transform(null));
            Assert.AreEqual(DBNull.Value, attribute.Transform(DBNull.Value));
            Assert.AreEqual("aaa", attribute.Transform("aaa"));
            Assert.AreEqual("bbb", attribute.Transform("BBB"));
            Assert.AreEqual("ccccc", attribute.Transform("cCcCc"));
            Assert.AreEqual(" d d d d d ", attribute.Transform(" D d D d D "));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void TransformFail(object value)
        {
            var attribute = new TransformToLowerCaseAttribute();

            try
            {
                attribute.Transform(value);

                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value must be of type String.", ex.Message);
            }
        }

        #endregion Public Methods
    }
}