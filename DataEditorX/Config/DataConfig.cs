﻿/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-23
 * 时间: 7:54
 * 
 */

namespace DataEditorX.Config
{
    /// <summary>
    /// DataEditor的数据
    /// </summary>
    public class DataConfig
    {
        public DataConfig()
        {
            InitMember(MyPath.Combine(Application.StartupPath, DEXConfig.TAG_CARDINFO + ".txt"));
        }
        public DataConfig(string conf)
        {
            InitMember(conf);
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
                dicCardRules = new Dictionary<long, string>();
                dicSetnames = new Dictionary<long, string>();
                dicCardTypes = new Dictionary<long, string>();
                dicLinkMarkers = new Dictionary<long, string>();
                dicCardcategorys = new Dictionary<long, string>();
                dicCardflags = new Dictionary<long, string>();
                dicCardAttributes = new Dictionary<long, string>();
                dicCardRaces = new Dictionary<long, string>();
                dicCardLevels = new Dictionary<long, string>();
                return;
            }
            //提取内容
            string text = File.ReadAllText(conf);
            dicCardRules = DataManager.Read(text, DEXConfig.TAG_RULE);
            dicSetnames = DataManager.Read(text, DEXConfig.TAG_SETNAME);
            dicCardTypes = DataManager.Read(text, DEXConfig.TAG_TYPE);
            dicLinkMarkers = DataManager.Read(text, DEXConfig.TAG_MARKER);
            dicCardcategorys = DataManager.Read(text, DEXConfig.TAG_CATEGORY);
            dicCardflags = DataManager.Read(text, DEXConfig.TAG_FLAGS);
            dicCardAttributes = DataManager.Read(text, DEXConfig.TAG_ATTRIBUTE);
            dicCardRaces = DataManager.Read(text, DEXConfig.TAG_RACE);
            dicCardLevels = DataManager.Read(text, DEXConfig.TAG_LEVEL);

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
