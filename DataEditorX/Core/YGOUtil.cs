using DataEditorX.Config;
using DataEditorX.Core.Info;
using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace DataEditorX.Core
{
    static class YGOUtil
    {
        static DataConfig _datacfg;
        static YGOUtil()
        {
            _datacfg = new DataConfig();
        }
        public static void SetConfig(DataConfig dcfg)
        {
            _datacfg = dcfg;
        }

        #region 判断文件类型
        public static bool IsScript(string file)
        {
            if (file != null && file.EndsWith(".lua", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
        public static bool IsDataBase(string file)
        {
            if (file != null && (file.EndsWith(".cdb", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".db", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region 获取属性，种族
        public static string GetAttributeString(int attr)
        {
            return DataManager.GetValue(_datacfg.dicCardAttributes, attr);
        }


        public static string GetRace(long race)
        {
            return DataManager.GetValue(_datacfg.dicCardRaces, race);
        }
        #endregion

        #region 获取卡片类型
        public static string GetCardType(Card c)
        {
            string str = "???";
            if (c.IsType(CardType.TYPE_MONSTER))
            {//卡片类型和第1效果
                if (c.IsType(CardType.TYPE_XYZ))
                {
                    str = GetType(CardType.TYPE_XYZ);
                }
                else if (c.IsType(CardType.TYPE_TOKEN))
                {
                    str = GetType(CardType.TYPE_TOKEN);
                }
                else if (c.IsType(CardType.TYPE_RITUAL))
                {
                    str = GetType(CardType.TYPE_RITUAL);
                }
                else if (c.IsType(CardType.TYPE_FUSION))
                {
                    str = GetType(CardType.TYPE_FUSION);
                }
                else if (c.IsType(CardType.TYPE_SYNCHRO))
                {
                    str = GetType(CardType.TYPE_SYNCHRO);
                }
                else if (c.IsType(CardType.TYPE_EFFECT))
                {
                    str = GetType(CardType.TYPE_EFFECT);
                }
                else
                {
                    str = GetType(CardType.TYPE_NORMAL);
                }

                str += GetType(CardType.TYPE_MONSTER);
            }
            else if (c.IsType(CardType.TYPE_SPELL))
            {
                if (c.IsType(CardType.TYPE_EQUIP))
                {
                    str = GetType(CardType.TYPE_EQUIP);
                }
                else if (c.IsType(CardType.TYPE_QUICKPLAY))
                {
                    str = GetType(CardType.TYPE_QUICKPLAY);
                }
                else if (c.IsType(CardType.TYPE_FIELD))
                {
                    str = GetType(CardType.TYPE_FIELD);
                }
                else if (c.IsType(CardType.TYPE_CONTINUOUS))
                {
                    str = GetType(CardType.TYPE_CONTINUOUS);
                }
                else if (c.IsType(CardType.TYPE_RITUAL))
                {
                    str = GetType(CardType.TYPE_RITUAL);
                }
                else
                {
                    str = GetType(CardType.TYPE_NORMAL);
                }

                str += GetType(CardType.TYPE_SPELL);
            }
            else if (c.IsType(CardType.TYPE_TRAP))
            {
                if (c.IsType(CardType.TYPE_CONTINUOUS))
                {
                    str = GetType(CardType.TYPE_CONTINUOUS);
                }
                else if (c.IsType(CardType.TYPE_COUNTER))
                {
                    str = GetType(CardType.TYPE_COUNTER);
                }
                else
                {
                    str = GetType(CardType.TYPE_NORMAL);
                }

                str += GetType(CardType.TYPE_TRAP);
            }
            return str.Replace(" ", "");
        }

        static string GetType(CardType type)
        {
            return DataManager.GetValue(_datacfg.dicCardTypes, (long)type);
        }

        public static string GetTypeString(long type)
        {
            string str = "";
            foreach (long k in _datacfg.dicCardTypes.Keys)
            {
                if ((type & k) == k)
                {
                    str += GetType((CardType)k) + "|";
                }
            }
            if (str.Length > 0)
            {
                str = str[..^1];
            }
            else
            {
                str = "???";
            }

            return str;
        }
        #endregion

        #region 系列名
        public static string GetSetNameString(long setcode)
        {
            long sc1 = setcode & 0xffff;
            long sc2 = (setcode >> 0x10) & 0xffff;
            long sc3 = (setcode >> 0x20) & 0xffff;
            long sc4 = (setcode >> 0x30) & 0xffff;
            string setname = DataManager.GetValue(_datacfg.dicSetnames, sc1)
                    + " " + DataManager.GetValue(_datacfg.dicSetnames, sc2)
                    + " " + DataManager.GetValue(_datacfg.dicSetnames, sc3)
                    + " " + DataManager.GetValue(_datacfg.dicSetnames, sc4);

            return setname;
        }
        #endregion

        #region 根据文件读取数据库
        /// <summary>
        /// 读取ydk文件为密码数组
        /// </summary>
        /// <param name="file">ydk文件</param>
        /// <returns>密码数组</returns>
        public static string[] ReadYDK(string ydkfile)
        {
            string str;
            List<string> IDs = new();
            if (File.Exists(ydkfile))
            {
                using FileStream f = new(ydkfile, FileMode.Open, FileAccess.Read);
                StreamReader sr = new(f, Encoding.Default);
                str = sr.ReadLine();
                while (str != null)
                {
                    if (int.TryParse(str.Trim(), out _) && str.Length > 0)
                    {
                        if (IDs.IndexOf(str) < 0)
                        {
                            IDs.Add(str);
                        }
                    }
                    str = sr.ReadLine();
                }
                sr.Close();
                f.Close();
            }
            if (IDs.Count == 0)
            {
                return null;
            }

            return IDs.ToArray();
        }
        #endregion

        #region 图像
        public static string[] ReadImage(string path)
        {
            List<string> list = new();
            string[] files = Directory.GetFiles(path, "*.*");
            int n = files.Length;
            for (int i = 0; i < n; i++)
            {
                string ex = Path.GetExtension(files[i]).ToLower();
                if ((ex == ".jpg" || ex == ".png" || ex == ".bmp") && int.TryParse(Path.GetFileNameWithoutExtension(files[i]), out int s))
                        list.Add(s.ToString());
            }
            return list.ToArray();
        }
        public static string[] ReadScript(string path)
        {
            List<string> list = new();
            string[] files = Directory.GetFiles(path, "*.lua");
            int n = files.Length;
            for (int i = 0; i < n; i++)
            {
                if (int.TryParse(Path.GetFileNameWithoutExtension(files[i])[(Path.GetFileName(files[i])[0] == 'c' ? 1 : 0)..], out int s))
                    list.Add(s.ToString());
            }
            return list.ToArray();
        }
        #endregion

        #region 删除资源
        //删除资源
        public static void CardDelete(long id, YgoPath ygopath)
        {
            string[] files = ygopath.GetCardfiles(id);
            for (int i = 0; i < files.Length; i++)
            {
                if (FileSystem.FileExists(files[i]))
                {
                    FileSystem.DeleteFile(files[i], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
            }
        }
        #endregion

        #region 资源改名
        //资源改名
        public static void CardRename(long newid, long oldid, YgoPath ygopath)
        {
            string[] newfiles = ygopath.GetCardfiles(newid);
            string[] oldfiles = ygopath.GetCardfiles(oldid);

            for (int i = 0; i < oldfiles.Length; i++)
            {
                if (File.Exists(oldfiles[i]))
                {
                    try
                    {
                        File.Move(oldfiles[i], newfiles[i]);
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region 复制资源
        public static void CardCopy(long newid, long oldid, YgoPath ygopath)
        {
            string[] newfiles = ygopath.GetCardfiles(newid);
            string[] oldfiles = ygopath.GetCardfiles(oldid);

            for (int i = 0; i < oldfiles.Length; i++)
            {
                if (File.Exists(oldfiles[i]))
                {
                    try
                    {
                        File.Copy(oldfiles[i], newfiles[i], false);
                    }
                    catch { }
                }
            }
        }
        #endregion
    }
}
