/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2015-5-24
 * 时间: 10:55
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System.Text;

namespace DataEditorX.Common
{
    /// <summary>
    /// Description of MyUtils.
    /// </summary>
    public class MyUtils
    {
        /// <summary>
        /// 计算文件的MD5校验
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new();
                for (int i = 0; i < retVal.Length; i++)
                {
                    _ = sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch
            {

            }
            return "";
        }

        public static bool Md5isEmpty(string md5)
        {
            return md5 == null || md5.Length < 16;
        }
    }
}
