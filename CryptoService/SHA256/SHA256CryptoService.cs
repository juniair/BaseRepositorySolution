using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CryptoService.SHA256
{
    public class SHA256CryptoService : ISHA256CryptoService
    {
        /// <summary>
        /// 인코딩 방식을 선택합니다.
        /// </summary>
        public Encoding Encoder { private get; set; }
        /// <summary>
        /// SHA256 암호화 엔진
        /// </summary>
        private readonly SHA256CryptoServiceProvider _hasher = new SHA256CryptoServiceProvider();


        /// <summary>
        /// 암호화 된 문자열을 복호화 합니다.(현재는 구현이 안되어 있습니다.)
        /// </summary>
        /// <param name="message">암호화 된 문자열</param>
        /// <returns>복호화 된 문자열</returns>
        [Obsolete]
        public string Decrypyto(string message)
        {
            throw new NotImplementedException();
            
        }

        /// <summary>
        /// 암호화를 제공합니다.
        /// </summary>
        /// <param name="message">암호화 할 문자열</param>
        /// <returns>암호화 된 문자열</returns>
        public string Encrypyto(string message)
        {

            var bytes = _hasher.ComputeHash(Encoder.GetBytes(message));

            //var builder = new StringBuilder();
            //foreach (var x in bytes)
            //{
            //    builder.Append(string.Format("{0:x2}", x));
            //}

            //return builder.ToString();
            return BitConverter.ToString(bytes).Replace("-","").ToLower();
        }
    }
}
