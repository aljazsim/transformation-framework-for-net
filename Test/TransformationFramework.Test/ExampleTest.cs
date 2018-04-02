using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransformationFramework;
using TransformationFramework.Test.Examples;

namespace TransformationFramework.Test
{
    [TestClass]
    public class ExampleTest
    {
        #region Public Methods

        [TestMethod]
        public void Example1Test()
        {
            Example1 example;

            example = new Example1();
            example.Property1 = "aaa";
            example.Property2 = "BbB";
            example.Transform();

            Assert.AreEqual("AAA", example.Property1);
            Assert.AreEqual("bbb", example.Property2);

            example = new Example1();
            example.Property1 = "aaa";
            example.Property2 = "BbB";
            example.Transform(nameof(example.Property1));

            Assert.AreEqual("AAA", example.Property1);
            Assert.AreEqual("BbB", example.Property2);

            example = new Example1();
            example.Property1 = "aaa";
            example.Property2 = "BbB";
            example.Transform(nameof(example.Property2));

            Assert.AreEqual("aaa", example.Property1);
            Assert.AreEqual("bbb", example.Property2);
        }

        [TestMethod]
        public void Example2Test()
        {
            Example2 example;

            example = new Example2();
            example.Property1 = 100;

            try
            {
                example.Transform();

                Assert.Fail();
            }
            catch (TransformationErrorException ex)
            {
                Assert.AreEqual("Unhandeled transformation exception occured.", ex.Message);
                Assert.AreEqual(typeof(TransformToUpperCaseAttribute), ex.TransformationAttributeType);
                Assert.AreEqual(typeof(Example2), ex.TransformationSourceType);
                Assert.AreEqual(nameof(example.Property1), ex.PropertyName);
                Assert.IsNotNull(ex.InnerException);
                Assert.AreEqual(typeof(ArgumentException), ex.InnerException.GetType());
                Assert.AreEqual("Value must be of type String.", ex.InnerException.Message);
            }
        }

        [TestMethod]
        public void Example3Test()
        {
            Example3 example;

            example = new Example3();
            example.Property1 = "aAa";
            example.Property2 = "bBb";
            example.Transform();

            Assert.AreEqual("aaa", example.Property1);
            Assert.AreEqual("BBB", example.Property2);
        }

        [TestMethod]
        public void Example4Test()
        {
            Example4 example;

            example = new Example4();
            example.Property1 = "aAa";
            example.Property2 = "bBb";
            example.Transform();

            Assert.AreEqual("aaa", example.Property1);
            Assert.AreEqual("bbb", example.Property2);
        }

        [TestMethod]
        public void Example5Test()
        {
            Example5 example;

            example = new Example5();
            example.Property1 = "aAa";
            example.Transform();

            Assert.AreEqual("AAA...", example.Property1);
        }

        [TestMethod]
        public void Example6Test()
        {
            Example6 example;

            example = new Example6();
            example.Property1 = null;
            example.Transform();

            Assert.AreEqual(null, example.Property1);

            example.Property1 = "aAaAaA";
            example.Transform();

            Assert.AreEqual("aaaaaa", example.Property1);

            example.Property1 = "aAaAaAaAaAaAaAaAaAaAaA";
            example.Transform();

            Assert.AreEqual("AAAAAAAAAAAAAAAAAAAAAA", example.Property1);
        }

        #endregion Public Methods
    }
}