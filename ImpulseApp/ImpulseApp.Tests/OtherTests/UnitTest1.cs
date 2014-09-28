using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImpulseApp.Utilites;

namespace ImpulseApp.Tests.OtherTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGenerator()
        {
                string s1 = Generator.GenerateShortAdUrl();
                string s2 = Generator.GenerateShortAdUrl();
                
                Assert.AreNotEqual(s1, s2);
        }
    }
}
