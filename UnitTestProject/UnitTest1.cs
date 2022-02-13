using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string otp = null;
            bool result = MySecurityLib.Class1.GetOTP(out otp);
            Assert.AreEqual(true, result);
            Assert.AreNotEqual(String.Empty, otp);
            //Assert.is
        }
        [TestMethod]
        public void TestMethod2()
        {
            string data = null;
            bool result = MySecurityLib.Class1.Encrypt("pswd", out data);
            Assert.AreEqual(true, result);
            Assert.AreNotEqual(String.Empty, data);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string data = null;
            string encpwd = "uonFDG0wiRw=";
            bool result = MySecurityLib.Class1.Decrypt(encpwd, out data);
            Assert.AreEqual(true, result);
        }
    }
   
}
