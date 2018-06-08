using CryptoService.SHA256;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UnitTest
{
    [TestClass]
    public class CryptoTest
    {
        [TestMethod]
        public void EnCryptoTest()
        {
            ISHA256CryptoService service = new SHA256CryptoService()
            {
                Encoder = Encoding.ASCII
            };

            string hashString = service.Encrypyto("123456");



            Assert.AreEqual("8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", hashString);
        }

        [TestMethod]
        public void Test()
        {

        }
    }
}
