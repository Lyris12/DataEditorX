/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-12
 * 时间: 12:48
 * 
 */
using DataEditorX.Common;
using DataEditorX.Core.Info;
using DataEditorX.Language;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace DataEditorX.Core.Mse
{
    /// <summary>
    /// MSE制作
    /// </summary>
    public class MseMaker
    {
        #region 常量
        public const string TAG_CARD = "card";
        public const string TAG_CARDTYPE = "card_type";
        public const string TAG_NAME = "name";
        public const string TAG_ATTRIBUTE = "attribute";
        public const string TAG_LEVEL = "level";
        public const string TAG_IMAGE = "image";
        /// <summary>种族</summary>
        public const string TAG_TYPE1 = "type_1";
        /// <summary>效果1</summary>
        public const string TAG_TYPE2 = "type_2";
        /// <summary>效果2/summary>
        public const string TAG_TYPE3 = "type_3";
        /// <summary>效果3</summary>
        public const string TAG_TYPE4 = "type_4";
        public const string TAG_TYPE5 = "type_5";
        public const string TAG_TEXT = "rule_text";
        public const string TAG_ATK = "attack";
        public const string TAG_DEF = "defense";
        public const string TAG_NUMBER = "number";
        public const string TAG_RARITY = "rarity";
        public const string TAG_PENDULUM = "pendulum";
        public const string TAG_PSCALE1 = "red_scale";
        public const string TAG_PSCALE2 = "blue_scale";
        public const string TAG_PEND_TEXT = "pendulum_text";
        public const string TAG_CODE = "gamecode";
        public const string UNKNOWN_ATKDEF = "?";
        public const int UNKNOWN_ATKDEF_VALUE = -2;
        public const string TAG_REP_TEXT = "%text%";
        public const string TAG_REP_PTEXT = "%ptext%";

        public const string TAG_Link_Marker_Up = "linku";
        public const string TAG_Link_Marker_UL = "linkul";
        public const string TAG_Link_Marker_UR = "linkur";
        public const string TAG_Link_Marker_Down = "linkd";
        public const string TAG_Link_Marker_DL = "linkdl";
        public const string TAG_Link_Marker_DR = "linkdr";
        public const string TAG_Link_Marker_Left = "linkl";
        public const string TAG_Link_Marker_Right = "linkr";
        public const string TAG_Link_Number = TAG_DEF;
        #endregion

        #region 成员，初始化
        MSEConfig cfg;
        public int MaxNum
        {
            get { return this.cfg.maxcount; }
        }

        public string ImagePath
        {
            get { return this.cfg.imagepath; }
        }

        public MseMaker(MSEConfig mcfg)
        {
            this.SetConfig(mcfg);
        }
        public void SetConfig(MSEConfig mcfg)
        {
            this.cfg = mcfg;
        }
        public MSEConfig GetConfig()
        {
            return this.cfg;
        }
        #endregion

        #region 数据处理
        //合并
        public string GetLine(string key, string word)
        {
            return "	" + key + ": \"" + word + "\"";
        }
        //特殊字
        public string ReItalic(string str)
        {
            str = this.CN2TW(str);
            foreach (string rs in this.cfg.replaces.Keys)
            {
                str = Regex.Replace(str, rs, this.cfg.replaces[rs]);
            }
            return str;
        }
        //简体转繁体
        public string CN2TW(string str)
        {
            if (this.cfg.Iscn2tw)
            {
                str = Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
                str = str.Replace("巖", "岩");
            }
            return str;
        }
        //获取魔法陷阱的类型符号
        public string GetSpellTrapSymbol(Card c, bool isSpell)
        {
            string level;
            if (c.IsType(CardType.TYPE_EQUIP))
            {
                level = MseSpellTrap.EQUIP;
            }
            else if (c.IsType(CardType.TYPE_QUICKPLAY))
            {
                level = MseSpellTrap.QUICKPLAY;
            }
            else if (c.IsType(CardType.TYPE_FIELD))
            {
                level = MseSpellTrap.FIELD;
            }
            else if (c.IsType(CardType.TYPE_CONTINUOUS))
            {
                level = MseSpellTrap.CONTINUOUS;
            }
            else if (c.IsType(CardType.TYPE_RITUAL))
            {
                level = MseSpellTrap.RITUAL;
            }
            else if (c.IsType(CardType.TYPE_COUNTER))
            {
                level = MseSpellTrap.COUNTER;
            }
            else if (this.cfg.str_spell == MSEConfig.TAG_REP && this.cfg.str_trap == MSEConfig.TAG_REP)
            {
                level = MseSpellTrap.NORMAL;//带文字的图片
            }
            else
            {
                level = "";
            }

            if (isSpell)
            {
                level = this.cfg.str_spell.Replace(MSEConfig.TAG_REP, level);
            }
            else
            {
                level = this.cfg.str_trap.Replace(MSEConfig.TAG_REP, level);
            }

            return level;
        }
        //获取图片路径
        public static string GetCardImagePath(string picpath, Card c)
        {
            //密码，带0密码，卡名
            string jpg = MyPath.Combine(picpath, c.id + ".jpg");
            string jpg2 = MyPath.Combine(picpath, c.IdString + ".jpg");
            string jpg3 = MyPath.Combine(picpath, c.name + ".jpg");
            string png = MyPath.Combine(picpath, c.id + ".png");
            string png2 = MyPath.Combine(picpath, c.IdString + ".png");
            string png3 = MyPath.Combine(picpath, c.name + ".png");
            if (File.Exists(jpg))
            {
                return jpg;
            }
            else if (File.Exists(jpg2))
            {
                return jpg2;
            }
            else if (File.Exists(jpg3))
            {
                File.Copy(jpg3, jpg, true);
                if (File.Exists(jpg))
                {//复制失败
                    return jpg;
                }
            }
            else if (File.Exists(png))
            {
                return png;
            }
            else if (File.Exists(png2))
            {
                return png2;
            }
            else if (File.Exists(png3))
            {
                File.Copy(png3, png, true);
                if (File.Exists(png))
                {//复制失败
                    return png;
                }
            }
            return "nil";
        }
        //获取属性
        public static string GetAttribute(int attr)
        {
            CardAttribute cattr = (CardAttribute)attr;
            string sattr = MseAttribute.NONE;
            switch (cattr)
            {
                case CardAttribute.ATTRIBUTE_DARK:
                    sattr = MseAttribute.DARK;
                    break;
                case CardAttribute.ATTRIBUTE_DEVINE:
                    sattr = MseAttribute.DIVINE;
                    break;
                case CardAttribute.ATTRIBUTE_EARTH:
                    sattr = MseAttribute.EARTH;
                    break;
                case CardAttribute.ATTRIBUTE_FIRE:
                    sattr = MseAttribute.FIRE;
                    break;
                case CardAttribute.ATTRIBUTE_LIGHT:
                    sattr = MseAttribute.LIGHT;
                    break;
                case CardAttribute.ATTRIBUTE_WATER:
                    sattr = MseAttribute.WATER;
                    break;
                case CardAttribute.ATTRIBUTE_WIND:
                    sattr = MseAttribute.WIND;
                    break;
            }
            return sattr;
        }
        //获取效果文本
        public static string GetDesc(string cdesc, string regx)
        {
            string desc = cdesc;
            desc = desc.Replace("\r\n", "\n");
            desc = desc.Replace("\r", "\n");
            Regex regex = new Regex(regx, RegexOptions.Multiline);
            Match mc = regex.Match(desc);
            if (mc.Success)
            {
                return ((mc.Groups.Count > 1) ?
                        mc.Groups[1].Value : mc.Groups[0].Value);
            }

            return "";
        }

        public string ReText(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            sb.Replace("\r\n", "\\n");
            sb.Replace("\r", "");
            sb.Replace("\n\n", "\\n");
            sb.Replace("\"", "\\\"");
            return sb.ToString().Trim('\n');
        }
        //获取星星
        public static string GetStar(long level)
        {
            long j = level & 0xff;
            string star = "";
            for (int i = 0; i < j; i++)
            {
                star += "*";
            }
            return star;
        }
        //获取种族
        public string GetRace(long race)
        {
            if (this.cfg.raceDic.ContainsKey(race))
            {
                return this.cfg.raceDic[race].Trim();
            }

            return race.ToString("x");
        }
        //获取类型文字
        public string GetType(CardType ctype)
        {
            long type = (long)ctype;
            if (this.cfg.typeDic.ContainsKey(type))
            {
                return this.cfg.typeDic[type].Trim();
            }

            return type.ToString("x");
        }

        //获取卡片类型
        public string[] GetTypes(Card c)
        {
            //卡片类型，效果1，效果2，效果3
            int MAX_TYPE= 5;
            var types = new string[MAX_TYPE+1];
            types[0] = MseCardType.CARD_NORMAL;
            for (int i = 1; i < types.Length; i++)
            {
                types[i] = "";
            }
            if (c.IsType(CardType.TYPE_MONSTER))
            {
                CardType[] cardTypes = CardTypes.GetMonsterTypes(c.type, this.cfg.no10);
                int count = cardTypes.Length;
                for (int i = 0; i < count && i < MAX_TYPE; i++)
                {
                    types[i + 1] = this.GetType(cardTypes[i]);
                }
                if (cardTypes.Length > 0)
                {
                    if (c.IsType(CardType.TYPE_LINK))
                    {
                        types[0] = MseCardType.CARD_LINK;
                    }
                    else if (c.IsType(CardType.TYPE_TOKEN))
                    {
                        types[0] = (c.race == 0) ?
                            MseCardType.CARD_TOKEN2
                            : MseCardType.CARD_TOKEN;
                    }
                    else if (c.IsType(CardType.TYPE_RITUAL))
                    {
                        types[0] = MseCardType.CARD_RITUAL;
                    }
                    else if (c.IsType(CardType.TYPE_FUSION))
                    {
                        types[0] = MseCardType.CARD_FUSION;
                    }
                    else if (c.IsType(CardType.TYPE_SYNCHRO))
                    {
                        types[0] = MseCardType.CARD_SYNCHRO;
                    }
                    else if (c.IsType(CardType.TYPE_XYZ))
                    {
                        types[0] = MseCardType.CARD_XYZ;
                    }
                    else if (c.IsType(CardType.TYPE_EFFECT))
                    {
                        types[0] = MseCardType.CARD_EFFECT;
                    }
                    else
                    {
                        types[0] = MseCardType.CARD_NORMAL;
                        if (cardTypes.Length == 1)
                        {
                            //xxx/通常
                        }
                    }
                }
            }
            if (c.race == 0)//如果没有种族
            {
                types[1] = "";
                types[2] = "";
                types[3] = "";
                types[4] = "";
            }
            return types;
        }
        #endregion

        #region 写存档
        //写存档
        public Dictionary<Card, string> WriteSet(string file, Card[] cards, string cardpack_db, bool rarity = true)
        {
            //			MessageBox.Show(""+cfg.replaces.Keys[0]+"/"+cfg.replaces[cfg.replaces.Keys[0]]);
            Dictionary<Card, string> list = new Dictionary<Card, string>();
            string pic = this.cfg.imagepath;
            using (FileStream fs = new FileStream(file,
                                                  FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine(this.cfg.head);
                foreach (Card c in cards)
                {
                    string jpg = GetCardImagePath(pic, c);
                    if (!string.IsNullOrEmpty(jpg) && jpg != "nil")
                    {
                        list.Add(c, jpg);
                        jpg = "local_image_file(\"" + Path.GetFileName(jpg) + "\")";
                    }
                    CardPack cardpack=DataBase.FindPack(cardpack_db, c.id);
                    if (c.IsType(CardType.TYPE_SPELL) || c.IsType(CardType.TYPE_TRAP))
                    {
                        sw.WriteLine(this.getSpellTrap(c, jpg, c.IsType(CardType.TYPE_SPELL), cardpack, rarity));
                    }
                    else
                    {
                        sw.WriteLine(this.getMonster(c, jpg, cardpack, rarity));
                    }
                }
                sw.WriteLine(this.cfg.end);
                sw.Close();
            }

            return list;
        }
        int getLinkNumber(long link)
        {
            string str = Convert.ToString(link, 2);
            char[] cs  = str.ToCharArray();
            int i = 0;
            foreach (char c in cs)
            {
                if (c == '1')
                {
                    i++;
                }
            }
            return i;
        }
        //怪兽，pendulum怪兽
        string getMonster(Card c, string img, CardPack cardpack = null, bool rarity = true)
        {
            StringBuilder sb = new StringBuilder();
            string[] types = this.GetTypes(c);
            string race = this.GetRace(c.race);
            sb.AppendLine(TAG_CARD + ":");
            sb.AppendLine(this.GetLine(TAG_CARDTYPE, types[0]));
            sb.AppendLine(this.GetLine(TAG_NAME, this.ReItalic(c.name)));
            sb.AppendLine(this.GetLine(TAG_ATTRIBUTE, GetAttribute(c.attribute)));
            bool noStar = false;
            if (this.cfg.noStartCards != null)
            {
                foreach (long id in this.cfg.noStartCards)
                {
                    if (c.alias == id || c.id == id)
                    {
                        noStar = true;
                        break;
                    }
                }
            }
            if (!noStar)
            {
                sb.AppendLine(this.GetLine(TAG_LEVEL, GetStar(c.level)));
            }
            sb.AppendLine("	" + TAG_IMAGE + ": " + img);
            sb.AppendLine(this.GetLine(TAG_TYPE1, this.CN2TW(race)));
            sb.AppendLine(this.GetLine(TAG_TYPE2, this.CN2TW(types[1])));
            sb.AppendLine(this.GetLine(TAG_TYPE3, this.CN2TW(types[2])));
            sb.AppendLine(this.GetLine(TAG_TYPE4, this.CN2TW(types[3])));
            sb.AppendLine(this.GetLine(TAG_TYPE5, this.CN2TW(types[4])));
            if (cardpack != null)
            {
                sb.AppendLine(this.GetLine(TAG_NUMBER, cardpack.pack_id));
                if (rarity)
                {
                    sb.AppendLine(this.GetLine(TAG_RARITY, cardpack.GetMseRarity()));
                }
            }
            string text = c.desc;
            string[] texts = Regex.Split(text, "\r?\n--+\r?\n");
            List<string> ctexts = new List<string>();
            if (texts.Length > 0)
                for (int k = 0; k < texts.Length; ++k)
                    if (texts[k].IndexOf(']') > 0)
                        ctexts.Add(texts[k].Substring(texts[k].IndexOf(']') + 3));
                    else ctexts.Add(texts[k]);
            string mtext;
            string ptext = "";
            if (ctexts.Count > 2 || ctexts.Count > 1 && c.IsType(CardType.TYPE_PENDULUM)) { mtext = ctexts[1]; ptext = ctexts[0]; } else mtext = ctexts[0];
            if (ptext == "-n/a-") ptext = "";
            if (c.IsType(CardType.TYPE_LINK))
            {
                if (CardLink.IsLink(c.def, CardLink.DownLeft))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_DL, "on"));
                }
                if (CardLink.IsLink(c.def, CardLink.Down))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_Down, "on"));
                }
                if (CardLink.IsLink(c.def, CardLink.DownRight))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_DR, "on"));
                }
                if (CardLink.IsLink(c.def, CardLink.UpLeft))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_UL, "on"));
                }
                if (CardLink.IsLink(c.def, CardLink.Up))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_Up, "on"));
                }
                if (CardLink.IsLink(c.def, CardLink.UpRight))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_UR, "on"));
                }
                if (CardLink.IsLink(c.def, CardLink.Left))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_Left, "on"));
                }
                if (CardLink.IsLink(c.def, CardLink.Right))
                {
                    sb.AppendLine(this.GetLine(TAG_Link_Marker_Right, "on"));
                }
                sb.AppendLine(this.GetLine(TAG_DEF, "" + this.getLinkNumber(c.def)));
                sb.AppendLine(this.GetLine(TAG_TEXT, this.ReText(this.ReItalic(mtext))));
            }
            else
            {
                if (c.IsType(CardType.TYPE_PENDULUM))//P怪兽
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        text = c.desc;
                    }

                    sb.AppendLine(this.GetLine(TAG_TEXT, this.ReText(this.ReItalic(mtext))));
                    sb.AppendLine(this.GetLine(TAG_PENDULUM, "pendulum"));
                    sb.AppendLine(this.GetLine(TAG_PSCALE1, ((c.level >> 0x18) & 0xff).ToString()));
                    sb.AppendLine(this.GetLine(TAG_PSCALE2, ((c.level >> 0x10) & 0xff).ToString()));
                    sb.AppendLine(this.GetLine(TAG_PEND_TEXT, this.ReText(this.ReItalic(ptext))));
                }
                else//一般怪兽
                {
                    sb.AppendLine(this.GetLine(TAG_TEXT, this.ReText(this.ReItalic(mtext))));
                }
                sb.AppendLine(this.GetLine(TAG_DEF, (c.def < 0) ? UNKNOWN_ATKDEF : c.def.ToString()));
            }
            sb.AppendLine(this.GetLine(TAG_ATK, (c.atk < 0) ? UNKNOWN_ATKDEF : c.atk.ToString()));

            sb.AppendLine(this.GetLine(TAG_CODE, c.IdString));
            return sb.ToString();
        }
        //魔法陷阱
        string getSpellTrap(Card c, string img, bool isSpell, CardPack cardpack = null, bool rarity = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(TAG_CARD + ":");
            sb.AppendLine(this.GetLine(TAG_CARDTYPE, isSpell ? "spell card" : "trap card"));
            sb.AppendLine(this.GetLine(TAG_NAME, this.ReItalic(c.name)));
            sb.AppendLine(this.GetLine(TAG_ATTRIBUTE, isSpell ? "spell" : "trap"));
            sb.AppendLine(this.GetLine(TAG_LEVEL, this.GetSpellTrapSymbol(c, isSpell)));
            sb.AppendLine("	" + TAG_IMAGE + ": " + img);
            if (cardpack != null)
            {
                sb.AppendLine(this.GetLine(TAG_NUMBER, cardpack.pack_id));
                if (rarity)
                {
                    sb.AppendLine(this.GetLine(TAG_RARITY, cardpack.GetMseRarity()));
                }
            }
            string[] texts = Regex.Split(c.desc, "\r?\n--+\r?\n");
            sb.AppendLine(this.GetLine(TAG_TEXT, this.ReText(this.ReItalic(texts[0]))));
            sb.AppendLine(this.GetLine(TAG_CODE, c.IdString));
            return sb.ToString();
        }
        #endregion

        #region 读存档
        public static int GetAttributeInt(string cattr)
        {
            int iattr = 0;
            switch (cattr)
            {
                case MseAttribute.DARK:
                    iattr = (int)CardAttribute.ATTRIBUTE_DARK;
                    break;
                case MseAttribute.DIVINE:
                    iattr = (int)CardAttribute.ATTRIBUTE_DEVINE;
                    break;
                case MseAttribute.EARTH:
                    iattr = (int)CardAttribute.ATTRIBUTE_EARTH;
                    break;
                case MseAttribute.FIRE:
                    iattr = (int)CardAttribute.ATTRIBUTE_FIRE;
                    break;
                case MseAttribute.LIGHT:
                    iattr = (int)CardAttribute.ATTRIBUTE_LIGHT;
                    break;
                case MseAttribute.WATER:
                    iattr = (int)CardAttribute.ATTRIBUTE_WATER;
                    break;
                case MseAttribute.WIND:
                    iattr = (int)CardAttribute.ATTRIBUTE_WIND;
                    break;
            }
            return iattr;
        }
        long GetRaceInt(string race)
        {
            if (!string.IsNullOrEmpty(race))
            {
                foreach (long key in this.cfg.raceDic.Keys)
                {
                    if (race.Equals(this.cfg.raceDic[key]))
                    {
                        return key;
                    }
                }
            }
            return (long)CardRace.RACE_NONE;
        }
        long GetTypeInt(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                foreach (long key in this.cfg.typeDic.Keys)
                {
                    if (type.Equals(this.cfg.typeDic[key]))
                    {
                        return key;
                    }
                }
            }
            return 0;
        }
        static string GetValue(string content, string tag)
        {
            Regex regx = new Regex(@"^[\t]+?" + tag + @":([\s\S]*?)$", RegexOptions.Multiline);
            Match m = regx.Match(content);
            if (m.Success)
            {
                if (m.Groups.Count >= 2)
                {
                    return RemoveTag(m.Groups[1].Value);
                }
            }
            return "";
        }
        //多行
        static string GetMultiValue(string content, string tag)
        {
            //TODO
            content = content.Replace("\t\t", "");
            Regex regx = new Regex(@"^[\t]+?" + tag + @":([\S\s]*?)^\t[\S\s]+?:", RegexOptions.Multiline);
            Match m = regx.Match(content);
            if (m.Success)
            {
                if (m.Groups.Count >= 2)
                {
                    string word = m.Groups[1].Value;
                    return RemoveTag(word).Replace("^", "").Replace("\t", "");
                }
            }
            return "";
        }
        long GetSpellTrapType(string level)
        {
            long type = 0;
            //魔法陷阱
            if (level.Contains(MseSpellTrap.EQUIP))
            {
                type = (long)CardType.TYPE_EQUIP;
            }

            if (level.Contains(MseSpellTrap.QUICKPLAY))
            {
                type = (long)CardType.TYPE_QUICKPLAY;
            }

            if (level.Contains(MseSpellTrap.FIELD))
            {
                type = (long)CardType.TYPE_FIELD;
            }

            if (level.Contains(MseSpellTrap.CONTINUOUS))
            {
                type = (long)CardType.TYPE_CONTINUOUS;
            }

            if (level.Contains(MseSpellTrap.RITUAL))
            {
                type = (long)CardType.TYPE_RITUAL;
            }

            if (level.Contains(MseSpellTrap.COUNTER))
            {
                type = (long)CardType.TYPE_COUNTER;
            }

            return type;
        }

        long GetMonsterType(string cardtype)
        {
            long type;
            if (cardtype.Equals(MseCardType.CARD_SPELL))
            {
                type = (long)CardType.TYPE_SPELL;
            }
            else if (cardtype.Equals(MseCardType.CARD_TRAP))
            {
                type = (long)CardType.TYPE_TRAP;
            }
            else
            {
                type = (long)CardType.TYPE_MONSTER;
                switch (cardtype)
                {
                    case MseCardType.CARD_NORMAL:
                        type |= (long)CardType.TYPE_NORMAL;
                        break;
                    case MseCardType.CARD_EFFECT:
                        type |= (long)CardType.TYPE_EFFECT;
                        break;
                    case MseCardType.CARD_XYZ:
                        type |= (long)CardType.TYPE_XYZ;
                        break;
                    case MseCardType.CARD_RITUAL:
                        type |= (long)CardType.TYPE_RITUAL;
                        break;
                    case MseCardType.CARD_FUSION:
                        type |= (long)CardType.TYPE_FUSION;
                        break;
                    case MseCardType.CARD_TOKEN:
                    case MseCardType.CARD_TOKEN2:
                        type |= (long)CardType.TYPE_TOKEN;
                        break;
                    case MseCardType.CARD_SYNCHRO:
                        type |= (long)CardType.TYPE_SYNCHRO;
                        break;
                    default:
                        type |= (long)CardType.TYPE_NORMAL;
                        break;
                }
            }
            return type;
        }
        //卡片类型
        long GetCardType(string cardtype, string level, params string[] types)
        {
            long type = 0;
            //魔法陷阱
            type |= this.GetSpellTrapType(level);
            //怪兽
            type |= this.GetMonsterType(cardtype);
            //types是识别怪兽效果类型
            foreach (string typ in types)
            {
                type |= this.GetTypeInt(typ);
            }

            return type;
        }

        static string RemoveTag(string word)
        {
            //移除标签<>
            word = Regex.Replace(word, "<[^>]+?>", "");
            return word.Trim().Replace("\t", "");
        }
        //解析卡片
        public Card ReadCard(string content, out string img)
        {
            string tmp;
            Card c = new Card
            {
                ot = (int)CardRule.OCGTCG,
                //卡名
                name = GetValue(content, TAG_NAME)
            };
            tmp = GetValue(content, TAG_LEVEL);
            //卡片种族
            c.race = this.GetRaceInt(GetValue(content, TAG_TYPE1));
            //卡片类型
            c.type = this.GetCardType(GetValue(content, TAG_CARDTYPE), tmp,
                                 GetValue(content, TAG_TYPE2),
                                 GetValue(content, TAG_TYPE3),
                                 GetValue(content, TAG_TYPE4),
                                 GetValue(content, TAG_TYPE5));
            long t = this.GetSpellTrapType(GetValue(content, TAG_LEVEL));
            //不是魔法，陷阱卡片的星数
            if (!(c.IsType(CardType.TYPE_SPELL)
                  || c.IsType(CardType.TYPE_TRAP)) && t == 0)
            {
                c.level = GetValue(content, TAG_LEVEL).Length;
            }

            //属性
            c.attribute = GetAttributeInt(GetValue(content, TAG_ATTRIBUTE));
            //密码
            long.TryParse(GetValue(content, TAG_CODE), out c.id);
            //ATK
            tmp = GetValue(content, TAG_ATK);
            if (tmp == UNKNOWN_ATKDEF)
            {
                c.atk = UNKNOWN_ATKDEF_VALUE;
            }
            else
            {
                int.TryParse(tmp, out c.atk);
            }
            //DEF
            tmp = GetValue(content, TAG_DEF);
            if (tmp == UNKNOWN_ATKDEF)
            {
                c.def = UNKNOWN_ATKDEF_VALUE;
            }
            else
            {
                int.TryParse(tmp, out c.def);
            }
            //图片
            img = GetValue(content, TAG_IMAGE);
            //摇摆
            if (c.IsType(CardType.TYPE_PENDULUM))
            {//根据预设的模版，替换内容
                tmp = this.cfg.temp_text.Replace(TAG_REP_TEXT,
                                            GetMultiValue(content, TAG_TEXT));
                tmp = tmp.Replace(TAG_REP_PTEXT,
                                  GetMultiValue(content, TAG_PEND_TEXT));
                c.desc = tmp;
            }
            else
            {
                c.desc = GetMultiValue(content, TAG_TEXT);
            }
            //摇摆刻度
            int.TryParse(GetValue(content, TAG_PSCALE1), out int itmp);
            c.level += (itmp << 0x18);
            int.TryParse(GetValue(content, TAG_PSCALE2), out itmp);
            c.level += (itmp << 0x10);
            return c;
        }
        //读取所有卡片
        public Card[] ReadCards(string set, bool repalceOld)
        {
            List<Card> cards = new List<Card>();
            if (!File.Exists(set))
            {
                return null;
            }

            string allcontent = File.ReadAllText(set, Encoding.UTF8);

            Regex regx = new Regex(@"^card:[\S\s]+?gamecode:[\S\s]+?$",
                                   RegexOptions.Multiline);
            MatchCollection matchs = regx.Matches(allcontent);
            int i = 0;

            foreach (Match match in matchs)
            {
                string content = match.Groups[0].Value;
                i++;
                Card c = this.ReadCard(content, out string img);
                if (c.id <= 0)
                {
                    c.id = i;
                }
                //添加卡片
                cards.Add(c);
                //已经解压出来的图片
                string saveimg = MyPath.Combine(this.cfg.imagepath, img);
                if (!File.Exists(saveimg))//没有解压相应的图片
                {
                    continue;
                }
                //改名后的图片
                img = MyPath.Combine(this.cfg.imagepath, c.IdString + ".jpg");
                if (img == saveimg)//文件名相同
                {
                    continue;
                }

                if (File.Exists(img))
                {
                    if (repalceOld)//如果存在，则备份原图
                    {
                        File.Delete(img + ".bak");//删除备份
                        File.Move(img, img + ".bak");//备份
                        File.Move(saveimg, img);//改名
                    }
                }
                else
                {
                    File.Move(saveimg, img);
                }
            }
            File.Delete(set);
            return cards.ToArray();
        }
        #endregion

        #region images
        /// <summary>
        /// 图片缓存
        /// </summary>
        /// <param name="img"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public string GetImageCache(string img, Card card)
        {
            if (!this.cfg.reimage)
            {
                //不需要调整
                return img;
            }
            bool isPendulum = card.IsType(CardType.TYPE_PENDULUM);
            if (isPendulum)
            {
                if (this.cfg.pwidth <= 0 && this.cfg.pheight <= 0)
                {
                    return img;
                }
            }
            else
            {
                if (this.cfg.width <= 0 && this.cfg.height <= 0)
                {
                    return img;
                }
            }
            string md5=MyUtils.GetMD5HashFromFile(img);
            if (MyUtils.Md5isEmpty(md5) || this.cfg.imagecache == null)
            {
                //md5为空
                return img;
            }
            string file = MyPath.Combine(this.cfg.imagecache, md5);
            if (!File.Exists(file))
            {
                //生成缓存
                Bitmap bmp=MyBitmap.ReadImage(img);
                //缩放
                if (isPendulum)
                {
                    bmp = MyBitmap.Zoom(bmp, this.cfg.pwidth, this.cfg.pheight);
                }
                else
                {
                    bmp = MyBitmap.Zoom(bmp, this.cfg.width, this.cfg.height);
                }
                //保存文件
                MyBitmap.SaveAsJPEG(bmp, file, 100);
            }
            return file;
        }
        #endregion

        #region export
        static System.Diagnostics.Process _mseProcess;
        static EventHandler _exitHandler;
        private static void exportSetThread(object obj)
        {
            string[] args=(string[])obj;
            if (args == null || args.Length < 3)
            {
                MessageBox.Show(LanguageHelper.GetMsg(LMSG.exportMseImagesErr));
                return;
            }
            string mse_path=args[0];
            string setfile=args[1];
            string path=args[2];
            if (string.IsNullOrEmpty(mse_path) || string.IsNullOrEmpty(setfile))
            {
                MessageBox.Show(LanguageHelper.GetMsg(LMSG.exportMseImagesErr));
                return;
            }
            else
            {
                string cmd=" --export "+setfile.Replace("\\\\","\\").Replace("\\","/")+" {card.gamecode}.png";
                _mseProcess = new System.Diagnostics.Process();
                _mseProcess.StartInfo.FileName = mse_path;
                _mseProcess.StartInfo.Arguments = cmd;
                _mseProcess.StartInfo.WorkingDirectory = path;
                _mseProcess.EnableRaisingEvents = true;
                MyPath.CreateDir(path);
                try
                {
                    _mseProcess.Start();
                    //等待结束，需要把当前方法放到线程里面
                    _mseProcess.WaitForExit();
                    _mseProcess.Exited += new EventHandler(_exitHandler);
                    _mseProcess.Close();
                    _mseProcess = null;
                    MessageBox.Show(LanguageHelper.GetMsg(LMSG.exportMseImages));
                }
                catch
                {

                }
            }
        }

        public static bool MseIsRunning()
        {
            return _mseProcess != null;
        }
        public static void MseStop()
        {
            try
            {
                _mseProcess.Kill();
                _mseProcess.Close();
            }
            catch { }
        }
        public static void ExportSet(string mse_path, string setfile, string path, EventHandler handler)
        {
            if (string.IsNullOrEmpty(mse_path) || setfile == null || setfile.Length == 0)
            {
                return;
            }
            ParameterizedThreadStart ParStart = new ParameterizedThreadStart(exportSetThread);
            Thread myThread = new Thread(ParStart)
            {
                IsBackground = true
            };
            myThread.Start(new string[] { mse_path, setfile, path });
            _exitHandler = handler;
        }
        #endregion

        public void TestPendulum(string desc)
        {
            List<string> table = this.GetMPText(desc);
            if (table == null && table.Count != 2)
            {
                MessageBox.Show("desc is null", "info");
            }
            else
            {
                MessageBox.Show(this.ReItalic(table[0]), "Monster Effect");
                MessageBox.Show(this.ReItalic(table[1]), "Pendulum Effect");
            }
        }

        public List<string> GetMPText(string desc)
        {
            if (string.IsNullOrEmpty(desc))
            {
                MessageBox.Show("desc is null", "info");
                return null;
            }
            else
            {
                string ptext;
                string text;
                if (Regex.IsMatch(desc, "【灵摆】"))
                {
                    ptext = GetDesc(desc, @"\A←[ ]*\d*[ ]*【灵摆】[ ]*\d*[ ]*→[\r\n]*([\S\s]*?)[\r\n]*【");
                    text = GetDesc(desc, @"【[^【】\r\n]{3,}】[\r\n]*([\S\s]*?)\z");
                }
                else
                {
                    ptext = GetDesc(desc, @"\A[\S\s]*?[\r\n]*【灵摆效果】[\r\n]*([\S\s]*?)\z");
                    text = GetDesc(desc, @"\A([\S\s]*?)[\r\n]*【灵摆效果】[\r\n]*[\S\s]*?\z");
                }
                if (string.IsNullOrEmpty(text))
                {
                    text = desc;
                }

                List<string> val = new List<string>();
                string[] texts = Regex.Split(text, "\r?\n--+\r?\n");
                if (texts.Length > 0)
                    for (int k = 0; k < texts.Length; ++k)
                        if (texts[k].IndexOf(']') > 0)
                            val.Add(texts[k].Substring(texts[k].IndexOf(']')+3));
                        else val.Add(texts[k]);
                else {
                    val.Add(text);
                    val.Add(ptext);
                }
                return val;
            }
        }

        public string ConvertPTextOld(string text, string ptext, bool normal, int pscale_l, int pscale_r)
        {
            string str = normal ? "【怪兽描述】" : "【怪兽效果】";
            if (string.IsNullOrEmpty(ptext))
            {
                return string.Format("←{0} 【灵摆】 {1}→\r\n{2}\r\n{3}", pscale_l, pscale_r, str, text);
            }
            else
            {
                return string.Format("←{0} 【灵摆】 {1}→\r\n{2}\r\n{3}\r\n{4}", pscale_l, pscale_r, ptext, str, text);
            }
        }

        public string ConvertPTextNew(string text, string ptext)
        {
            if (string.IsNullOrEmpty(ptext))
            {
                return text;
            }
            else
            {
                return string.Format("{0}\r\n\r\n【灵摆效果】\r\n{1}", text, ptext);
            }
        }

        public string ReplaceText(string text, string name)
        {
            // pendulum format
            if (Regex.IsMatch(text, @"【灵摆】"))
            {
                List<string> table = this.GetMPText(text);
                if (table != null)
                {
                    text = this.ConvertPTextNew(table[0], table[1]);
                }
            }

            // give
            text = text.Replace("给与", "给予");

            // set name
            text = Regex.Replace(text, @"名字带有「([^「」]+)」的", "「$1」");

            if (Regex.IsMatch(text, "①"))
            {
                // this card name
                string thisname = string.Format("「{0}」", name);
                text = Regex.Replace(text, thisname + @"在1回合", "这个卡名的卡在1回合");
                text = Regex.Replace(text, thisname + @"在决斗中", "这个卡名的卡在决斗中");
                text = Regex.Replace(text, thisname + @"的效果1回合", "这个卡名的效果1回合");
                text = Regex.Replace(text, thisname + @"的效果在决斗中", "这个卡名的效果在决斗中");
                text = Regex.Replace(text, thisname + @"的怪兽效果1回合", "这个卡名的怪兽效果1回合");
                text = Regex.Replace(text, thisname + @"的怪兽效果在决斗中", "这个卡名的怪兽效果在决斗中");
                text = Regex.Replace(text, thisname + @"的灵摆效果1回合", "这个卡名的灵摆效果1回合");
                text = Regex.Replace(text, thisname + @"的灵摆效果在决斗中", "这个卡名的灵摆效果在决斗中");
                text = Regex.Replace(text, thisname + @"的([①②③④⑤⑥⑦⑧⑨⑩]+)的", "这个卡名的$1的");
                text = Regex.Replace(text, thisname + @"的([①②③④⑤⑥⑦⑧⑨⑩]+)的", "这个卡名的$1的");
                text = Regex.Replace(text, @"作为([①②③④⑤⑥⑦⑧⑨⑩]+)的", "$1的");
            }

            return text;
        }
    }
}
