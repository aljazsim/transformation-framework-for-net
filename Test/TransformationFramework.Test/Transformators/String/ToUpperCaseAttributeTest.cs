using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransformationFramework;

namespace TransformationFramework.Test
{
    [TestClass]
    public class ToUpperCaseAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void Transform()
        {
            var attribute = new TransformToUpperCaseAttribute();

            Assert.AreEqual(null, attribute.Transform(null));
            Assert.AreEqual(DBNull.Value, attribute.Transform(DBNull.Value));
            Assert.AreEqual("AAA", attribute.Transform("aaa"));
            Assert.AreEqual("BBB", attribute.Transform("BBB"));
            Assert.AreEqual("CCCCC", attribute.Transform("cCcCc"));
            Assert.AreEqual(" D D D D D ", attribute.Transform(" D d D d D "));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void TransformFail(object value)
        {
            var attribute = new TransformToUpperCaseAttribute();

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