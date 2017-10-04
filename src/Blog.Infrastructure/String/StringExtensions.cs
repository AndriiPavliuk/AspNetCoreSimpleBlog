using Blog.Encrypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class StringExtension
    {

        #region 类型转换
        /// <summary>
        /// String to Boolean(字符串 转换成 布尔型)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static bool ToBoolean(this string s, bool def = default(bool))
        {
            bool result;
            return Boolean.TryParse(s, out result) ? result : def;
        }

        public static bool? ToNullableBoolean(this string s, bool? def = default(bool?))
        {
            bool result;
            return Boolean.TryParse(s, out result) ? result : def;
        }

        public static DateTime? ToNullableDateTime(this string s, DateTime? def = default(DateTime?))
        {
            DateTime result;
            return DateTime.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Char(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static char ToChar(this string s, char def = default(char))
        {
            char result;
            return Char.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Decimal(字符串 转换成 数值、十进制)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static decimal ToDecimal(this string s, decimal def = default(decimal))
        {
            decimal result;
            return Decimal.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Double(字符串 转换成 数值、浮点)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static double ToDouble(this string s, double def = default(double))
        {
            double result;
            return Double.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Single(字符串 转换成 数值、浮点)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static float ToSingle(this string s, float def = default(float))
        {
            float result;
            return Single.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Byte(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static byte ToByte(this string s, byte def = default(byte))
        {
            byte result;
            return Byte.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to SByte(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static sbyte ToSByte(this string s, sbyte def = default(sbyte))
        {
            sbyte result;
            return SByte.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Int16(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static short ToInt16(this string s, short def = default(short))
        {
            short result;
            return Int16.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to UInt16(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static ushort ToUInt16(this string s, ushort def = default(ushort))
        {
            ushort result;
            return UInt16.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Int32(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static int ToInt32(this string s, int def = default(int))
        {
            int result;
            return Int32.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to UInt32(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static uint ToUInt32(this string s, uint def = default(uint))
        {
            uint result;
            return UInt32.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Int64(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static long ToInt64(this string s, long def = default(long))
        {
            long result;
            return Int64.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to UInt64(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static ulong ToUInt64(this string s, ulong def = default(ulong))
        {
            ulong result;
            return UInt64.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to DateTime(字符串 转换成 时间)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static DateTime ToDateTime(this string s, DateTime def = default(DateTime))
        {
            DateTime result;
            return DateTime.TryParse(s, out result) ? result : def;
        }

        /// <summary>
        /// String to Guid(字符串 转换成 Guid)
        /// </summary>
        /// <remarks>
        ///  2014-06-23 16:31 Created By iceStone
        /// </remarks>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static Guid ToGuid(this string s, Guid def = default(Guid))
        {
            Guid result;
            return Guid.TryParse(s, out result) ? result : def;
        }


        #endregion


        #region 判断
        /// <summary>
        /// 比较两个字符串是否相等,默认忽略大小写
        /// </summary>
        /// <param name="currentStr"></param>
        /// <param name="tarStr"></param>
        /// <param name="comEnum"></param>
        /// <returns></returns>
        public static bool IsSame(this string currentStr, string tarStr, StringComparison comEnum = StringComparison.CurrentCultureIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(currentStr) || string.IsNullOrWhiteSpace(tarStr))
            {
                return false;
            }

            
            return String.Equals(currentStr, tarStr, comEnum);
        }



        /// <summary>
        /// 是否为空或空字符串
        /// </summary>
        /// <param name="srcStr"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string srcStr)
        {
            return string.IsNullOrWhiteSpace(srcStr);
        }

        public static string IsNullOrEmptyToDef(this string srcStr, string defualt = "")
        {
            if (string.IsNullOrWhiteSpace(srcStr))
            {
                return defualt;
            }
            else
            {
                return srcStr;
            }

        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="srcStr"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string srcStr)
        {
            return string.IsNullOrEmpty(srcStr);
        }
        #endregion


        #region     字符串分割
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="srcStr"></param>
        /// <param name="c">一个分割符</param>
        /// <param name="option">分割选项</param>
        /// <returns></returns>
        public static string[] SplitExt(this string srcStr, char c, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries)
        {
            return srcStr.Split(new char[] { c }, option);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="srcStr">n个分割符</param>
        /// <param name="cs"></param>
        /// <param name="option">分割选项</param>
        /// <returns></returns>
        public static string[] SplitExt(this string srcStr, char[] cs, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries)
        {
            return srcStr.Split(cs, option);
        }


        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="srcStr">一个分割字符串</param>
        /// <param name="str"></param>
        /// <param name="option">分割选项</param>
        /// <returns></returns>
        public static string[] SplitExt(this string srcStr, string str, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries)
        {
            return srcStr.Split(new string[] { str }, option);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="srcStr">n个分割字符串</param>
        /// <param name="strs"></param>
        /// <param name="option">分割选项</param>
        /// <returns></returns>
        public static string[] SplitExt(this string srcStr, string[] strs, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries)
        {
            return srcStr.Split(strs, option);
        }


        /// <summary>
        /// 切割字符串,获得指定字符串后的字符串,不包括切割符
        /// </summary>
        /// <param name="srcStr"></param>
        /// <param name="lastIndex"></param>
        /// <returns></returns>
        public static string SubStringLastIndex(this string srcStr, string lastIndex)
        {
            return srcStr.Substring(srcStr.LastIndexOf(lastIndex) + lastIndex.Length);
        }

        /// <summary>
        /// 切割字符串,获得字符串首部到指定位置的子字符串,不包括切割符
        /// </summary>
        /// <param name="srcStr"></param>
        /// <param name="lastIndex"></param>
        /// <returns></returns>
        public static string SubStringLastIndexBefore(this string srcStr, string lastIndex)
        {
            return srcStr.Substring(0, srcStr.LastIndexOf(lastIndex));
        }

        #endregion



        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="srcStr"></param>
        /// <returns></returns>
        public static string ToMD5(this string srcStr, string encoding = "UTF-8")
        {
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
            byte[] inputBye;
            byte[] outputBye;
            inputBye = System.Text.Encoding.GetEncoding(encoding).GetBytes(srcStr);
            outputBye = m5.ComputeHash(inputBye);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < outputBye.Length; i++)
            {
                sBuilder.Append(outputBye[i].ToString("x2"));
            }
            return (sBuilder.ToString());
        }

        /// <summary>
        /// 使用带密钥的MD5
        /// </summary>
        /// <param name="srcStr"></param>
        /// <param name="salt"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToMD5WithSalt(this string srcStr, string salt, string encoding = "UTF-8")
        {
            HASHEncryptHelper helper = new HASHEncryptHelper(HMACHASHAlgorithmName.HMACMD5, salt);
            return helper.EncryStr(srcStr);
        }


        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="srcStr"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatExt(this string srcStr, params object[] args)
        {
            return string.Format(srcStr, args);
        }

        /// <summary>
        /// 获取中英文混排字符串的实际长度(字节数)
        /// </summary>
        /// <param name="str">要获取长度的字符串</param>
        /// <returns>字符串的实际长度值（字节数）</returns>
        public static int getStringByteLength(this string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
                strlen++;
            }
            return strlen;
        }


        /// <summary>
        /// 获得文件名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExt(this string str)
        {
            return Path.GetFileNameWithoutExtension(str);
        }

        /// <summary>
        /// 把windows磁盘路径转Url
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ToUrlPath(this string src)
        {
            return src.Replace(@"\", @"/");
        }



        /// <summary>
        /// 把字符串末尾替换成点
        /// </summary>
        /// <param name="index">字符串第几个开始替换成点</param>
        /// <param name="dotCount">点的数量(默认3)</param>
        /// <returns></returns>
        public static string ReplaceEndToDot(this string src, int index, int dotCount = 3)
        {
            if (src.Length < index)
            {
                return src;
            }
            string fontStr = src.Substring(0, index);

            //性能优化
            if (dotCount == 3)
            {
                return fontStr += "...";
            }
            for (int i = 0; i < dotCount; i++)
            {
                fontStr += ".";
            }
            return fontStr;
        }
    }
}
