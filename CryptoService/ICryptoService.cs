using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoService
{
    /// <summary>
    /// 암호화 서비스를 제공합니다.
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// 암호화를 제공합니다.
        /// </summary>
        /// <param name="message">암호화 할 문자열</param>
        /// <returns>암호화 된 문자열</returns>
        string Encrypyto(string message);

        /// <summary>
        /// 암호화 된 문자열을 복호화 합니다.(현재는 구현이 안되어 있습니다.)
        /// </summary>
        /// <param name="message">암호화 된 문자열</param>
        /// <returns>복호화 된 문자열</returns>
        [Obsolete]
        string Decrypyto(string message);
    }
}
