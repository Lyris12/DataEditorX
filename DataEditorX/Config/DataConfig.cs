/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-23
 * 时间: 7:54
 * 
 */
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DataEditorX.Config
{
    /// <summary>
    /// DataEditor的数据
    /// </summary>
    public class DataConfig
    {
        public DataConfig()
        {
            this.InitMember(MyPath.Combine(Application.StartupPath, DEXConfig.TAG_CARDINFO + ".txt"));
        }
        public DataConfig(string conf)
        {
            this.InitMember(conf);
        }
        /// <summary>
        /// 初始化成员
        /// </summary>
        /// <param name="conf"></param>
        public void InitMember(string conf)
        {
            //conf = MyPath.Combine(datapath, MyConfig.FILE_INFO);
            if (!File.Exists(conf))
            {
                this.dicCardRules = new Dictionary<long, string>();
                this.dicSetnames = new Dictionary<long, string>();
                this.dicCardTypes = new Dictionary<long, string>();
                this.dicLinkMarkers = new Dictionary<long, string>();
                this.dicCardcategorys = new Dictionary<long, string>();
                this.dicCardflags = new Dictionary<long, string>();
                this.dicCardAttributes = new Dictionary<long, string>();
                this.dicCardRaces = new Dictionary<long, string>();
                this.dicCardLevels = new Dictionary<long, string>();
                return;
            }
            //提取内容
            string text = File.ReadAllText(conf);
            this.dicCardRules = DataManager.Read(text, DEXConfig.TAG_RULE);
            this.dicSetnames = DataManager.Read(text, DEXConfig.TAG_SETNAME);
            this.dicCardTypes = DataManager.Read(text, DEXConfig.TAG_TYPE);
            this.dicLinkMarkers = DataManager.Read(text, DEXConfig.TAG_MARKER);
            this.dicCardcategorys = DataManager.Read(text, DEXConfig.TAG_CATEGORY);
            this.dicCardflags = DataManager.Read(text, DEXConfig.TAG_FLAGS);
            this.dicCardAttributes = DataManager.Read(text, DEXConfig.TAG_ATTRIBUTE);
            this.dicCardRaces = DataManager.Read(text, DEXConfig.TAG_RACE);
            this.dicCardLevels = DataManager.Read(text, DEXConfig.TAG_LEVEL);

        }
        /// <summary>
        /// 规则
        /// </summary>
        public Dictionary<long, string> dicCardRules = null;
        /// <summary>
        /// 属性
        /// </summary>
        public Dictionary<long, string> dicCardAttributes = null;
        /// <summary>
        /// 种族
        /// </summary>
        public Dictionary<long, string> dicCardRaces = null;
        /// <summary>
        /// 等级
        /// </summary>
        public Dictionary<long, string> dicCardLevels = null;
        /// <summary>
        /// 系列名
        /// </summary>
        public Dictionary<long, string> dicSetnames = null;
        /// <summary>
        /// 卡片类型
        /// </summary>
        public Dictionary<long, string> dicCardTypes = null;
        /// <summary>
        /// 连接标志
        /// </summary>
        public Dictionary<long, string> dicLinkMarkers = null;
        /// <summary>
        /// 效果类型
        /// </summary>
        public Dictionary<long, string> dicCardcategorys = null;
        /// <summary>
        /// Omega-exclusive
        /// </summary>
        public Dictionary<long, string> dicCardflags = null;
    }
}
