using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Blog.Encrypt
{
    public enum HASHAlgorithmName
    {
        KeyedHashAlgorithm = 1,
        MD5 = 2,
        RIPEMD160 = 3,
        SHA1 = 4,
        SHA256 = 5,
        SHA384 = 6,
        SHA512 = 7
    }

    /// <summary>
    /// 带密钥的哈希函数
    /// </summary>
    public enum HMACHASHAlgorithmName
    {
        HMACMD5 = 1,
        HMACRIPEMD160 = 2,
        HMACSHA1 = 3,
        HMACSHA256 = 4,
        HMACSHA384 = 5,
        HMACSHA512 = 6
    }

    public class HASHEncryptHelper
    {
        public HASHEncryptHelper(HASHAlgorithmName hashname)
        {
            _algo = HashAlgorithm.Create(hashname.ToString());
            //MD5 m=MD5.Create()
        }
        private HashAlgorithm _algo;
        public HASHEncryptHelper(HMACHASHAlgorithmName hashname, string key = "wonderwander")
        {
            _algo = KeyedHashAlgorithm.Create(hashname.ToString());
            var temp = (KeyedHashAlgorithm)_algo;
            temp.Key = Encoding.UTF8.GetBytes(key);
            _algo = temp;
        }

        public string EncryStr(string sourceStr)
        {
            var src_btye = Encoding.Default.GetBytes(sourceStr);

            var encrypted_byte=_algo.ComputeHash(src_btye);
            StringBuilder sb = new StringBuilder();
            //把字节装换成16位的字母
            for (int i = 0; i < encrypted_byte.Length; i++)
            {
                sb.Append(encrypted_byte[i].ToString("x2"));
            }
            return sb.ToString();
        }



    }
}
