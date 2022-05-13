/*
 * CreateDate :2014-02-07
 * desc :卡片类
 * ModiftyDate :2014-02-12
 */
using DataEditorX.Core.Info;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DataEditorX.Core
{
    public struct Card : IEquatable<Card>
    {
        public const int STR_MAX = 0x10;
        public const int SETCODE_MAX = 4;

        #region 构造
        /// <summary>
        /// 卡片
        /// </summary>
        /// <param name="cardCode">密码</param>
        /// <param name="cardName">名字</param>
        public Card(long cardCode)
        {
            id = cardCode;
            name = "";
            ot = 0;
            alias = 0;
            setcode = 0;
            type = 0;
            atk = 0;
            def = 0;
            level = 0;
            race = 0;
            attribute = 0;
            category = 0;
            omega = new long[5]{ 0L, 0L, 0L, 253402207200L, 253402207200L };
            script = "";
            desc = "";
            str = new string[STR_MAX];
            for (int i = 0; i < STR_MAX; i++)
            {
                str[i] = "";
            }
        }
        #endregion

        #region 成员
        /// <summary>卡片密码</summary>
        public long id;
        /// <summary>卡片规则</summary>
        public int ot;
        /// <summary>卡片同名卡</summary>
        public long alias;
        /// <summary>卡片系列号</summary>
        public long setcode;
        /// <summary>卡片种类</summary>
        public long type;
        /// <summary>攻击力</summary>
        public int atk;
        /// <summary>防御力</summary>
        public int def;
        /// <summary>卡片等级</summary>
        public long level;
        /// <summary>卡片种族</summary>
        public long race;
        /// <summary>卡片属性</summary>
        public int attribute;
        /// <summary>效果种类</summary>
        public long category;
        /// <summary>Omega-exclusive parameters</summary>
        public long[] omega;
        public string script;
        /// <summary>卡片名称</summary>
        public string name;
        /// <summary>描述文本</summary>
        public string desc;
        string[] str;
        /// <summary>脚本文件文字</summary>
        public string[] Str
        {
            get
            {
                if (str == null)
                {
                    str = new string[STR_MAX];
                    for (int i = 0; i < STR_MAX; i++)
                    {
                        str[i] = "";
                    }
                }
                return str;
            }
            set { str = value; }
        }
        public long[] GetSetCode()
        {
            long[] setcodes = new long[SETCODE_MAX];
            for (int i = 0, k = 0; i < SETCODE_MAX; k += 0x10, i++)
            {
                setcodes[i] = (setcode >> k) & 0xffff;
            }
            return setcodes;
        }
        public void SetSetCode(params long[] setcodes)
        {
            int i = 0;
            setcode = 0;
            if (setcodes != null)
            {
                foreach (long sc in setcodes)
                {
                    setcode += (sc << i);
                    i += 0x10;
                }
            }
        }
        public void SetSetCode(params string[] setcodes)
        {
            int i = 0;
            setcode = 0;
            if (setcodes != null)
            {
                foreach (string sc in setcodes)
                {
                    _ = long.TryParse(sc, NumberStyles.HexNumber, null, out long temp);
                    setcode += (temp << i);
                    i += 0x10;
                }
            }
        }
        public void SetSupport(long setcodes)
        {
            omega[2] = setcodes;
        }
        public void SetSupport(string setcodes)
        {
            if (long.TryParse(setcodes, NumberStyles.HexNumber, null, out long temp))
                omega[2] = temp;
        }
        public long GetLeftScale()
        {
            return (level >> 24) & 0xff;
        }
        public long GetRightScale()
        {
            return (level >> 16) & 0xff;
        }
        public string GetDate(byte offset = 0)
        {
            return new DateTime(new DateTime(1970, 1, 1).AddSeconds(omega[3 + offset]).ToBinary()).ToString("yyyy-MM-dd HH:mm:ss");
        }
        public void SetDate(string date, byte offset = 0)
        {
            omega[3 + offset] = Math.Abs(DateTime.Parse(date).Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000;
        }
        #endregion

        #region 比较、哈希值、操作符
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public override bool Equals(object obj)
        {
            if (obj is Card)
            {
                return Equals((Card)obj); // use Equals method below
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 比较卡片，除脚本提示文本
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool EqualsData(Card other)
        {
            bool equalBool = true;
            if (id != other.id)
            {
                equalBool = false;
            }
            else if (ot != other.ot)
            {
                equalBool = false;
            }
            else if (alias != other.alias)
            {
                equalBool = false;
            }
            else if (setcode != other.setcode)
            {
                equalBool = false;
            }
            else if (type != other.type)
            {
                equalBool = false;
            }
            else if (atk != other.atk)
            {
                equalBool = false;
            }
            else if (def != other.def)
            {
                equalBool = false;
            }
            else if (level != other.level)
            {
                equalBool = false;
            }
            else if (race != other.race)
            {
                equalBool = false;
            }
            else if (attribute != other.attribute)
            {
                equalBool = false;
            }
            else if (category != other.category)
            {
                equalBool = false;
            }
            else if (!name.Equals(other.name))
            {
                equalBool = false;
            }
            else if (!desc.Equals(other.desc))
            {
                equalBool = false;
            }
            else if (omega[0] > 0 && other.omega[0] > 0)
                for (byte i = 1; i < 5; i++)
                {
                    if (omega[i] != other.omega[i])
                    {
                        equalBool = false;
                        break;
                    }
                }
            return equalBool;
        }
        /// <summary>
        /// 比较卡片是否一致？
        /// </summary>
        /// <param name="other">比较的卡片</param>
        /// <returns>结果</returns>
        public bool Equals(Card other)
        {
            bool equalBool = EqualsData(other);
            if (!equalBool)
            {
                return false;
            }
            else if (str.Length != other.str.Length)
            {
                equalBool = false;
            }
            else
            {
                int l = str.Length;
                for (int i = 0; i < l; i++)
                {
                    if (!str[i].Equals(other.str[i]))
                    {
                        equalBool = false;
                        break;
                    }
                }
            }
            return equalBool;

        }
        /// <summary>
        /// 得到哈希值
        /// </summary>
        public override int GetHashCode()
        {
            // combine the hash codes of all members here (e.g. with XOR operator ^)
            int hashCode = id.GetHashCode() + name.GetHashCode();
            return hashCode;//member.GetHashCode();
        }
        /// <summary>
        /// 比较卡片是否相等
        /// </summary>
        public static bool operator ==(Card left, Card right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// 是否是某类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public bool IsType(CardType type)
        {
            if ((this.type & (long)type) == (long)type)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 是否是某系列
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
		public bool IsSetCode(long sc)
        {
            long settype = sc & 0xfff;
            long setsubtype = sc & 0xf000;
            long setcode = this.setcode;
            while (setcode != 0)
            {
                if ((setcode & 0xfff) == settype && (setcode & 0xf000 & setsubtype) == setsubtype)
                {
                    return true;
                }

                setcode >>= 0x10;
            }
            return false;
        }
        /// <summary>
        /// 卡片是否不相等
        /// </summary>
        public static bool operator !=(Card left, Card right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region 卡片文字信息
        /// <summary>
        /// 密码字符串
        /// </summary>
        public string IdString
        {
            get { return id.ToString("00000000"); }
        }
        /// <summary>
        /// 字符串化
        /// </summary>
        public override string ToString()
        {
            string str;
            if (IsType(CardType.TYPE_MONSTER))
            {
                str = name + "[" + IdString + "]\n["
                    + YGOUtil.GetTypeString(type) + "] "
                    + YGOUtil.GetRace(race) + "/" + YGOUtil.GetAttributeString(attribute)
                    + "\n" + levelString() + " " + atk + "/" + def + "\n" + redesc();
            }
            else
            {
                str = name + "[" + IdString + "]\n[" + YGOUtil.GetTypeString(type) + "]\n" + redesc();
            }

            return str;
        }
        public string ToShortString()
        {
            return name + " [" + IdString + "]";
        }
        public string ToLongString()
        {
            return ToString();
        }

        string levelString()
        {
            string star = "[";
            long j = level & 0xff;
            long i;
            for (i = 0; i < j; i++)
            {
                if (i > 0 && (i % 4) == 0)
                {
                    star += " ";
                }

                star += "★";
            }
            return star + "]";
        }
        string redesc()
        {
            string str = desc.Replace(Environment.NewLine, "\n");
            str = Regex.Replace(str, "([。|？|?])", "$1\n");
            str = str.Replace("\n\n", "\n");
            return str;
        }
        #endregion
    }

}
