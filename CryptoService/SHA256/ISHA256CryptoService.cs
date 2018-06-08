using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoService.SHA256
{
    /// <summary>
    /// SHA256 방식 암호화 서비스를 제공합니다.
    /// </summary>
    public interface ISHA256CryptoService : ICryptoService
    {
        /// <summary>
        /// 인코딩 방식을 선택합니다.
        /// </summary>
        Encoding Encoder { set; }
    }
}
