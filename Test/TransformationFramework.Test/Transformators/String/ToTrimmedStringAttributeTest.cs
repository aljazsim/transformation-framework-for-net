using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransformationFramework;

namespace TransformationFramework.Test
{
    [TestClass]
    public class ToTrimmedStringAttributeTest
    {
        #region Public Methods

        [TestMethod]
        public void Transform()
        {
            var attribute = new ToTrimmedStringAttribute();

            Assert.AreEqual(null, attribute.Transform(null));
            Assert.AreEqual(DBNull.Value, attribute.Transform(DBNull.Value));
            Assert.AreEqual("a a a", attribute.Transform("a a a"));
            Assert.AreEqual("BBB", attribute.Transform("         BBB\t"));
            Assert.AreEqual("c c c c c", attribute.Transform("c c c c c "));
            Assert.AreEqual("D d D d D", attribute.Transform(" D d D d D "));

            attribute = new ToTrimmedStringAttribute('.', ',');

            Assert.AreEqual(null, attribute.Transform(null));
            Assert.AreEqual(DBNull.Value, attribute.Transform(DBNull.Value));
            Assert.AreEqual("a a a", attribute.Transform("a a a"));
            Assert.AreEqual("     .    BBB\t", attribute.Transform("     .    BBB\t"));
            Assert.AreEqual("cc.c.c", attribute.Transform(".,.cc.c.c,"));
            Assert.AreEqual(" . ddd . ", attribute.Transform(", . ddd . ,"));
        }

        [DataRow(1)]
        [DataRow('c')]
        [DataTestMethod]
        public void TransformFail(object value)
        {
            var attribute = new ToTrimmedStringAttribute();

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