using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Blog.Encrypt
{
    /// <summary>
    /// 对称加密算法枚举名
    /// </summary>
    public enum SymmetricEncryptName
    {
        /// <summary>
        /// 三重Des
        /// </summary>
        TripleDES=1,
        Rijndael=2,
        Aes=3,
        DES=4,
        RC2=5,
    }


    /// <summary>
    /// 目前只有对称加密
    /// </summary>
    public class SymmetricEncryptHelper
    {
        // 对称加密算法提供器
        private ICryptoTransform encryptor;     // 加密器对象
        private ICryptoTransform decryptor;     // 解密器对象
        private const int BufferSize = 1024;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithmName">加密算法</param>
        /// <param name="key">密钥必须16位</param>
        public SymmetricEncryptHelper(SymmetricEncryptName algorithmName, string key)
        {

            //对称加密算法
            SymmetricAlgorithm provider = SymmetricAlgorithm.Create(algorithmName.ToString());
            provider.Key = Encoding.UTF8.GetBytes(key);
            //向量值,不应该是固定的
            provider.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            encryptor = provider.CreateEncryptor();
            
            decryptor = provider.CreateDecryptor();
        }
        /// <summary>
        /// 默认使用TripleDES加密算法,可逆加密算法
        /// </summary>
        /// <param name="key">密钥</param>
        public SymmetricEncryptHelper(string key) : this(SymmetricEncryptName.TripleDES, key) { }


        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="clearText">明文</param>
        /// <returns></returns>
        public string Encrypt(string clearText)
        {
            // 创建明文流
            byte[] clearBuffer = Encoding.UTF8.GetBytes(clearText);
            MemoryStream clearStream = new MemoryStream(clearBuffer);
            // 创建空的加密流,用于装密文
            MemoryStream encryptedStream = new MemoryStream();
            //加密时候,明文写入空的加密流中,所以 CryptoStreamMode.Write
            CryptoStream cryptoStream =new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write);
            // 将明文流写入到buffer中


            // 将buffer中的数据写入到cryptoStream中
            int bytesRead = 0;
            byte[] buffer = new byte[BufferSize];
            do
            {
                bytesRead = clearStream.Read(buffer, 0, BufferSize);
                cryptoStream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            cryptoStream.FlushFinalBlock();
            // 获取加密后的文本
            buffer = encryptedStream.ToArray();
            string encryptedText = Convert.ToBase64String(buffer);
            return encryptedText;
        }

        /// <summary>
        /// 解密算法
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedText)
        {
            if (!IsBase64String(encryptedText))
            {
                return "";
            }

            byte[] encryptedBuffer = Convert.FromBase64String(encryptedText);
            Stream encryptedStream = new MemoryStream(encryptedBuffer);
            MemoryStream clearStream = new MemoryStream();
            CryptoStream cryptoStream =
                new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read);
            int bytesRead = 0;
            byte[] buffer = new byte[BufferSize];
            do
            {
                bytesRead = cryptoStream.Read(buffer, 0, BufferSize);
                clearStream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);
            buffer = clearStream.GetBuffer();
            string clearText =
                Encoding.UTF8.GetString(buffer, 0, (int)clearStream.Length);
            return clearText;
        }
        /// <summary>
        /// 快速使用加密算法
        /// </summary>
        /// <param name="clearText">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string EncryptFast(string clearText, string key = "ABCDEFGHIJKLMNOP")
        {
            SymmetricEncryptHelper helper = new SymmetricEncryptHelper(key);
            return helper.Encrypt(clearText);
        }
        /// <summary>
        /// 快速使用解密
        /// </summary>
        /// <param name="encryptedText">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DecryptFast(string encryptedText, string key = "ABCDEFGHIJKLMNOP")
        {
            SymmetricEncryptHelper helper = new SymmetricEncryptHelper(key);
            return helper.Decrypt(encryptedText);
        }

        /// <summary>
        /// 判断是否为Base64编码后的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

    }
}
