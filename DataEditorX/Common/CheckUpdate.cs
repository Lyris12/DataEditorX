/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 6月10 星期二
 * 时间: 9:58
 * 
 */
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DataEditorX.Common
{
    /// <summary>
    /// 检查更新
    /// </summary>
    public static class CheckUpdate
    {
        static CheckUpdate()
        {
            //连接数
            ServicePointManager.DefaultConnectionLimit = 255;
        }
        /// <summary>
        /// 下载URL
        /// </summary>
        public static string URL = "";
        /// <summary>
        /// 从HEAD获取版本号
        /// </summary>
        public const string DEFAULT = "0.0.0.0";

        #region 检查版本
        /// <summary>
        /// 获取新版本
        /// </summary>
        /// <param name="VERURL">链接</param>
        /// <returns>版本号</returns>
        public static string GetNewVersion(string VERURL)
        {
            string urlver = DEFAULT;
            string html = GetHtmlContentByUrl(VERURL);
            if (!string.IsNullOrEmpty(html))
            {
                Regex ver = new(@"\[DataEditorX\]([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)\[DataEditorX\]");
                Regex url = new(@"\[URL\]([^\[]+?)\[URL\]");
                if (ver.IsMatch(html) && url.IsMatch(html))
                {
                    Match mVer = ver.Match(html);
                    Match mUrl = url.Match(html);
                    URL = mUrl.Groups[1].Value;
                    return $"{mVer.Groups[1].Value}";
                }
            }
            return urlver;
        }
        /// <summary>
        /// 检查版本号，格式0.0.0.0
        /// </summary>
        /// <param name="ver">0.0.0.0</param>
        /// <param name="oldver">0.0.0.0</param>
        /// <returns>是否有新版本</returns>
        public static bool CheckVersion(string ver, string oldver)
        {
            bool hasNew = false;
#if DEBUG
            System.Windows.Forms.MessageBox.Show(oldver + "=>" + ver);
#endif
            string[] vers = ver.Split('.');
            string[] oldvers = oldver.Split('.');
            if (vers.Length == oldvers.Length)
            {
                //从左到右比较数字
                for (int i = 0; i < oldvers.Length; i++)
                {
                    int.TryParse(vers[i], out int j);
                    int.TryParse(oldvers[i], out int k);
                    if (j > k)//新的版本号大于旧的
                    {
                        hasNew = true;
                        break;
                    }
                    else if (j < k)
                    {
                        hasNew = false;
                        break;
                    }
                }
            }
            return hasNew;
        }
        #endregion

        #region 获取网址内容
        /// <summary>
        /// 获取网址内容
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns>内容</returns>
        public static string GetHtmlContentByUrl(string url)
        {
            string htmlContent = string.Empty;
            try
            {
                HttpWebRequest httpWebRequest =
                    (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Timeout = 15000;
                using (HttpWebResponse httpWebResponse =
                       (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader =
                               new(stream, Encoding.UTF8))
                        {
                            htmlContent = streamReader.ReadToEnd();
                            streamReader.Close();
                        }
                        stream.Close();
                    }
                    httpWebResponse.Close();
                }
                return htmlContent;
            }
            catch
            {

            }
            return "";
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filename">保存文件路径</param>
        /// <returns>是否下载成功</returns>
        public static bool DownLoad(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                HttpWebRequest Myrq = (HttpWebRequest)WebRequest.Create(URL);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(filename + ".tmp", FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024 * 512];
                int osize = st.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, by.Length);
                }
                so.Close();
                st.Close();
                File.Move(filename + ".tmp", filename);
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
