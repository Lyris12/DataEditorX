using System.Xml;

namespace DataEditorX.Common
{
    public class XMLReader
    {
        #region XML操作config
        /// <summary>
        /// 保存值
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appValue"></param>
        public static void Save(string appKey, string appValue)
        {
            XmlDocument xDoc = new();
            xDoc.Load(XMLConfigFile);

            XmlNode xNode = xDoc.SelectSingleNode("//appSettings");

            XmlElement xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
            if (xElem != null) //存在，则更新
            {
                xElem.SetAttribute("value", appValue);
            }
            else//不存在，则插入
            {
                XmlElement xNewElem = xDoc.CreateElement("add");
                xNewElem.SetAttribute("key", appKey);
                xNewElem.SetAttribute("value", appValue);
                _ = xNode.AppendChild(xNewElem);
            }
            xDoc.Save(XMLConfigFile);
        }
        static string XMLConfigFile 
        { 
            get
            {
                string path = Application.ExecutablePath + ".config";
                if (!File.Exists(path))
                    path = path.Replace(".exe", ".dll");
                return path;
            } 
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string appKey)
        {
            XmlDocument xDoc = new();
            xDoc.Load(XMLConfigFile);

            XmlNode xNode = xDoc.SelectSingleNode("//appSettings");

            XmlElement xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");

            if (xElem != null)
            {
                return xElem.Attributes["value"].Value;
            }
            return string.Empty;
        }
        #endregion
    }
}
