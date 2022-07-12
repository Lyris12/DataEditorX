/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 17:01
 * 
 */
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Text;

namespace DataEditorX.Core
{
    /// <summary>
    /// SQLite 操作
    /// </summary>
    public static class DataBase
    {
        #region 默认
        static readonly string _defaultSQL;
        static readonly string _defaultTableSQL;
        static readonly string _defaultOTableSQL;

        static DataBase()
        {
            _defaultSQL =
                "SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id ";
            StringBuilder st = new();
            _ = st.Append(@"CREATE TABLE texts(id integer primary key,name text,desc text");
            for (int i = 1; i <= 16; i++)
            {
                _ = st.Append(",str");
                _ = st.Append(i);
                _ = st.Append(" text");
            }
            _ = st.Append(");");
            _ = st.Append(@"CREATE TABLE datas(");
            _ = st.Append("id integer primary key,ot integer,alias integer,");
            _ = st.Append("setcode integer,type integer,atk integer,def integer,");
            _ = st.Append("level integer,race integer,attribute integer,category integer) ");
            _defaultTableSQL = st.ToString();
            _ = st.Remove(0, st.Length);
            StringBuilder ost = new();
            _ = ost.Append(@"CREATE TABLE IF NOT EXISTS texts(id integer primary key,name text,desc text");
            for (int i = 1; i <= 16; i++)
            {
                _ = ost.Append(",str");
                _ = ost.Append(i);
                _ = ost.Append(" text");
            }
            _ = ost.Append(");");
            _ = ost.Append(@"CREATE TABLE IF NOT EXISTS datas(id integer primary key default 0,
            ot integer default 0,alias integer default 0,setcode integer default 0,
            type integer default 0,atk integer default 0,def integer default 0,level integer default 0,
            race integer default 0,attribute integer default 0,category integer default 0,
            genre integer default 0,script blob,support integer default 0,
            ocgdate integer default 253402207200,tcgdate integer default 253402207200);
            CREATE TABLE IF NOT EXISTS setcodes(officialcode integer,betacode integer,
            name text unique,cardid integer default 0);
            ");
            _defaultOTableSQL = ost.ToString();
            _ = ost.Remove(0, ost.Length);
        }
        #endregion

        #region 创建数据库
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="Db">新数据库路径</param>
        public static bool Create(string Db)
        {
            if (File.Exists(Db))
            {
                File.Delete(Db);
            }
            using SqliteConnection con = new(@"Data Source=" + Db);
            con.Open();
            con.Close();
            SqliteConnection.ClearAllPools();
            try
            {
                if (Db.EndsWith(".db", StringComparison.OrdinalIgnoreCase) || Db.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)) _ = Command(Db, _defaultOTableSQL);
                else _ = Command(Db, _defaultTableSQL);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool CheckTable(string db)
        {
            try
            {
                if (db.EndsWith(".db", StringComparison.OrdinalIgnoreCase) || db.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)) _ = Command(db, _defaultOTableSQL);
                else _ = Command(db, _defaultTableSQL);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 执行sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="DB">数据库</param>
        /// <param name="SQLs">sql语句</param>
        /// <returns>返回影响行数</returns>
        public static int Command(string DB, params string[] SQLs)
        {
            int result = 0;
            if (File.Exists(DB) && SQLs != null)
            {
                using SqliteConnection con = new(@"Data Source=" + DB);
                con.Open();
                using (SqliteTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        foreach (string SQLstr in SQLs)
                        {
                            using SqliteCommand cmd = con.CreateCommand();
                            cmd.CommandText = SQLstr;
                            result += cmd.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        trans.Rollback();//出错，回滚
                        result = -1;
                    }
                    if (result != -1) trans.Commit();
                }
                con.Close();
                SqliteConnection.ClearAllPools();
            }
            return result;
        }
        #endregion

        #region 根据SQL读取
        static Card ReadCard(SqliteDataReader reader, bool reNewLine)
        {
            Card c = new(0)
            {
                id = reader.GetInt64(reader.GetOrdinal("id")),
                ot = reader.GetInt32(reader.GetOrdinal("ot")),
                alias = reader.GetInt64(reader.GetOrdinal("alias")),
                setcode = reader.GetInt64(reader.GetOrdinal("setcode")),
                type = reader.GetInt64(reader.GetOrdinal("type")),
                atk = reader.GetInt32(reader.GetOrdinal("atk")),
                def = reader.GetInt32(reader.GetOrdinal("def")),
                level = reader.GetInt64(reader.GetOrdinal("level")),
                race = reader.GetInt64(reader.GetOrdinal("race")),
                attribute = reader.GetInt32(reader.GetOrdinal("attribute"))
            };
            try
            {
                c.category = reader.GetInt64(reader.GetOrdinal("genre"));
                c.omega = new long[5];
                c.omega[0] = 1L;
                c.omega[1] = reader.GetInt64(reader.GetOrdinal("category"));
                c.omega[2] = reader.GetInt64(reader.GetOrdinal("support"));
                c.omega[3] = reader.GetInt64(reader.GetOrdinal("ocgdate"));
                c.omega[4] = reader.GetInt64(reader.GetOrdinal("tcgdate"));
                c.script = reader.IsDBNull(reader.GetOrdinal("script")) ? ""
                    : reader.GetString(reader.GetOrdinal("script"));
            }
            catch
            {
                c.category = reader.GetInt64(reader.GetOrdinal("category"));
                c.omega = new long[5] { 0L, 0L, 0L, 253402207200L, 253402207200L };
                c.script = "";
            }
            c.name = reader.GetString(reader.GetOrdinal("name"));
            c.desc = reader.GetString(reader.GetOrdinal("desc"));
            if (reNewLine)
            {
                c.desc = Retext(c.desc);
            }

            for (int i = 0; i < 0x10; i++)
            {
                string temp = reader.GetString(reader.GetOrdinal("str" + (i + 1).ToString()));
                c.Str[i] = temp ?? "";
            }
            return c;
        }
        static string Retext(string text)
        {
            StringBuilder sr = new(text);
            _ = sr.Replace("\r\n", "\n");
            _ = sr.Replace("\n", Environment.NewLine);//换为当前系统的换行符
            text = sr.ToString();
            _ = sr.Remove(0, sr.Length);
            return text;
        }

        public static Card[] Read(string DB, bool reNewLine, params long[] ids)
        {
            List<string> idlist = new();
            foreach (long id in ids)
            {
                idlist.Add(id.ToString());
            }
            return Read(DB, reNewLine, idlist.ToArray());
        }
        /// <summary>
        /// 根据密码集合，读取数据
        /// </summary>
        /// <param name="DB">数据库</param>
        /// <param name="reNewLine">调整换行符</param>
        /// <param name="SQLs">SQL/密码语句集合集合</param>
        public static Card[] Read(string DB, bool reNewLine, params string[] SQLs)
        {
            List<Card> list = new();
            List<long> idlist = new();
            if (File.Exists(DB) && SQLs != null)
            {
                foreach (string str in SQLs)
                {
                    _ = int.TryParse(str, out int tmp);
                    string SQLstr;
                    if (string.IsNullOrEmpty(str))
                    {
                        SQLstr = _defaultSQL;
                    }
                    else if (tmp > 0)
                    {
                        SQLstr = _defaultSQL + " and datas.id=" + tmp.ToString();
                    }
                    else if (str.StartsWith("select", StringComparison.OrdinalIgnoreCase))
                    {
                        SQLstr = str;
                    }
                    else if (str.Contains("and ", StringComparison.CurrentCulture))
                    {
                        SQLstr = _defaultSQL + str;
                    }
                    else
                    {
                        SQLstr = _defaultSQL + " and texts.name like '%" + str + "%'";
                    }
                    using SqliteConnection con = new(@"Data Source=" + DB);
                    con.Open();
                    using SqliteCommand cmd = con.CreateCommand();
                    cmd.CommandText = SQLstr;
                    using SqliteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Card c = ReadCard(reader, reNewLine);
                        if (idlist.IndexOf(c.id) < 0)
                        {//不存在，则添加
                            idlist.Add(c.id);
                            list.Add(c);
                        }
                    }
                    con.Close();
                    SqliteConnection.ClearAllPools();
                }
            }
            if (list.Count == 0)
            {
                return null;
            }

            return list.ToArray();
        }
        #endregion

        #region 复制数据库
        /// <summary>
        /// 复制数据库
        /// </summary>
        /// <param name="DB">复制到的数据库</param>
        /// <param name="cards">卡片集合</param>
        /// <param name="ignore">是否忽略存在</param>
        /// <returns>更新数x2</returns>
        public static int CopyDB(string DB, bool ignore, params Card[] cards)
        {
            int result = 0;
            if (File.Exists(DB) && cards != null)
            {
                using SqliteConnection con = new(@"Data Source=" + DB);
                con.Open();
                using SqliteTransaction trans = con.BeginTransaction();
                foreach (Card c in cards)
                {
                    using SqliteCommand cmd = con.CreateCommand();
                    cmd.CommandText = (DB.EndsWith(".db", StringComparison.OrdinalIgnoreCase) || DB.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)) ? OmegaGetInsertSQL(c, ignore) : GetInsertSQL(c, ignore);
                    result += cmd.ExecuteNonQuery();
                }
                trans.Commit();
                con.Close();
                SqliteConnection.ClearAllPools();
            }
            return result;
        }
        #endregion

        #region 删除记录
        public static int DeleteDB(string DB, params Card[] cards)
        {
            int result = 0;
            if (File.Exists(DB) && cards != null)
            {
                using SqliteConnection con = new(@"Data Source=" + DB);
                con.Open();
                using SqliteTransaction trans = con.BeginTransaction();
                foreach (Card c in cards)
                {
                    using SqliteCommand cmd = con.CreateCommand();
                    cmd.CommandText = GetDeleteSQL(c);
                    result += cmd.ExecuteNonQuery();
                }
                trans.Commit();
                con.Close();
                SqliteConnection.ClearAllPools();
            }
            return result;
        }
        #endregion

        #region 压缩数据库
        public static void Compression(string db)
        {
            if (File.Exists(db))
            {
                using SqliteConnection con = new(@"Data Source=" + db);
                con.Open();
                using SqliteCommand cmd = con.CreateCommand();
                cmd.CommandText = "vacuum";
                _ = cmd.ExecuteNonQuery();
                con.Close();
                SqliteConnection.ClearAllPools();
            }
        }
        #endregion

        #region SQL语句
        #region 查询
        static string ToInt(long l)
        {
            unchecked
            {
                return ((int)l).ToString();
            }
        }
        public static string OmegaGetSelectSQL(Card c)
        {
            StringBuilder sb = new();
            _ = sb.Append("SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id ");
            if (c == null)
            {
                return sb.ToString();
            }

            if (!string.IsNullOrEmpty(c.name))
            {
                if (c.name.Contains("%%", StringComparison.CurrentCulture))
                {
                    c.name = c.name.Replace("%%", "%");
                }
                else
                {
                    c.name = "%" + c.name.Replace("%", "/%").Replace("_", "/_") + "%";
                }

                _ = sb.Append(" and texts.name like '" + c.name.Replace("'", "''") + "' ");
            }
            if (!string.IsNullOrEmpty(c.desc))
            {
                _ = sb.Append(" and texts.desc like '%" + c.desc.Replace("'", "''") + "%' ");
            }

            if (c.ot > 0)
            {
                _ = sb.Append(" and datas.ot = " + c.ot.ToString());
            }

            if (c.attribute > 0)
            {
                _ = sb.Append(" and datas.attribute = " + c.attribute.ToString());
            }

            if ((c.level & 0xff) > 0)
            {
                _ = sb.Append(" and (datas.level & 255) = " + ToInt(c.level & 0xff));
            }

            if ((c.level & 0xff000000) > 0)
            {
                _ = sb.Append(" and (datas.level & 4278190080) = " + ToInt(c.level & 0xff000000));
            }

            if ((c.level & 0xff0000) > 0)
            {
                _ = sb.Append(" and (datas.level & 16711680) = " + ToInt(c.level & 0xff0000));
            }

            if (c.race > 0)
            {
                _ = sb.Append(" and datas.race = " + ToInt(c.race));
            }

            if (c.type > 0)
            {
                _ = sb.Append(" and datas.type & " + ToInt(c.type) + " = " + ToInt(c.type));
            }

            if (c.category > 0)
                _ = sb.Append(" and datas.genre & " + ToInt(c.category) + " = " + ToInt(c.category));

            if (c.omega != null && c.omega[0] > 0)
            {
                if (c.omega[1] > 0)
                    _ = sb.Append(" and datas.category & " + ToInt((long)c.omega[1]) + " = " + ToInt((long)c.omega[1]));
                if (c.omega[2] > 0)
                    _ = sb.Append(" and datas.support & " + ToInt((long)c.omega[2]) + " = " + ToInt((long)c.omega[2]));
                if (c.omega[3] > 0 && c.omega[3] < 253402207200)
                    _ = sb.Append(" and datas.tcgdate = " + ToInt(DateTime.Parse(c.GetDate(1)).Ticks / 10000000));
                if (c.omega[4] > 0 && c.omega[4] < 253402207200)
                    _ = sb.Append(" and datas.ocgdate = " + ToInt(DateTime.Parse(c.GetDate()).Ticks / 10000000));
            }

            if (c.atk == -1)
            {
                _ = sb.Append(" and datas.type & 1 = 1 and datas.atk = 0");
            }
            else if (c.atk < 0 || c.atk > 0)
            {
                _ = sb.Append(" and datas.atk = " + c.atk.ToString());
            }

            if (c.IsType(Info.CardType.TYPE_LINK))
            {
                _ = sb.Append(" and datas.def &" + c.def.ToString() + "=" + c.def.ToString());
            }
            else
            {
                if (c.def == -1)
                {
                    _ = sb.Append(" and datas.type & 1 = 1 and datas.def = 0");
                }
                else if (c.def < 0 || c.def > 0)
                {
                    _ = sb.Append(" and datas.def = " + c.def.ToString());
                }
            }

            if (c.id > 0 && c.alias > 0)
            {
                _ = sb.Append(" and datas.id BETWEEN " + c.alias.ToString() + " and " + c.id.ToString());
            }
            else if (c.id > 0)
            {
                _ = sb.Append(" and ( datas.id=" + c.id.ToString() + " or datas.alias=" + c.id.ToString() + ") ");
            }
            else if (c.alias > 0)
            {
                _ = sb.Append(" and datas.alias= " + c.alias.ToString());
            }

            return sb.ToString();

        }
        public static string GetSelectSQL(Card c)
        {
            StringBuilder sb = new();
            _ = sb.Append("SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id ");
            if (c == null)
            {
                return sb.ToString();
            }

            if (!string.IsNullOrEmpty(c.name))
            {
                if (c.name.Contains("%%", StringComparison.CurrentCulture))
                {
                    c.name = c.name.Replace("%%", "%");
                }
                else
                {
                    c.name = "%" + c.name.Replace("%", "/%").Replace("_", "/_") + "%";
                }

                _ = sb.Append(" and texts.name like '" + c.name.Replace("'", "''") + "' ");
            }
            if (!string.IsNullOrEmpty(c.desc))
            {
                _ = sb.Append(" and texts.desc like '%" + c.desc.Replace("'", "''") + "%' ");
            }

            if (c.ot > 0)
            {
                _ = sb.Append(" and datas.ot = " + c.ot.ToString());
            }

            if (c.attribute > 0)
            {
                _ = sb.Append(" and datas.attribute = " + c.attribute.ToString());
            }

            if ((c.level & 0xff) > 0)
            {
                _ = sb.Append(" and (datas.level & 255) = " + ToInt(c.level & 0xff));
            }

            if ((c.level & 0xff000000) > 0)
            {
                _ = sb.Append(" and (datas.level & 4278190080) = " + ToInt(c.level & 0xff000000));
            }

            if ((c.level & 0xff0000) > 0)
            {
                _ = sb.Append(" and (datas.level & 16711680) = " + ToInt(c.level & 0xff0000));
            }

            if (c.race > 0)
            {
                _ = sb.Append(" and datas.race = " + ToInt(c.race));
            }

            if (c.type > 0)
            {
                _ = sb.Append(" and datas.type & " + ToInt(c.type) + " = " + ToInt(c.type));
            }

            if (c.category > 0)
            {
                _ = sb.Append(" and datas.category & " + ToInt(c.category) + " = " + ToInt(c.category));
            }

            if (c.atk == -1)
            {
                _ = sb.Append(" and datas.type & 1 = 1 and datas.atk = 0");
            }
            else if (c.atk < 0 || c.atk > 0)
            {
                _ = sb.Append(" and datas.atk = " + c.atk.ToString());
            }

            if (c.IsType(Info.CardType.TYPE_LINK))
            {
                _ = sb.Append(" and datas.def &" + c.def.ToString() + "=" + c.def.ToString());
            }
            else
            {
                if (c.def == -1)
                {
                    _ = sb.Append(" and datas.type & 1 = 1 and datas.def = 0");
                }
                else if (c.def < 0 || c.def > 0)
                {
                    _ = sb.Append(" and datas.def = " + c.def.ToString());
                }
            }

            if (c.id > 0 && c.alias > 0)
            {
                _ = sb.Append(" and datas.id BETWEEN " + c.alias.ToString() + " and " + c.id.ToString());
            }
            else if (c.id > 0)
            {
                _ = sb.Append(" and ( datas.id=" + c.id.ToString() + " or datas.alias=" + c.id.ToString() + ") ");
            }
            else if (c.alias > 0)
            {
                _ = sb.Append(" and datas.alias= " + c.alias.ToString());
            }

            return sb.ToString();

        }
        #endregion

        #region 插入
        /// <summary>
        /// 转换为插入语句
        /// </summary>
        /// <param name="c">卡片数据</param>
        /// <param name="ignore"></param>
        /// <returns>SQL语句</returns>
        public static string OmegaGetInsertSQL(Card c, bool ignore, bool hex = false)
        {
            StringBuilder st = new();
            if (ignore)
            {
                _ = st.Append("INSERT or ignore into datas values(");
            }
            else
            {
                _ = st.Append("INSERT or replace into datas values(");
            }

            _ = st.Append(c.id); _ = st.Append(',');
            _ = st.Append(c.ot); _ = st.Append(',');
            _ = st.Append(c.alias); _ = st.Append(',');
            if (hex)
            {
                _ = st.Append("0x" + c.setcode.ToString("x")); _ = st.Append(',');
                _ = st.Append("0x" + c.type.ToString("x")); _ = st.Append(',');
            }
            else
            {
                _ = st.Append(c.setcode); _ = st.Append(',');
                _ = st.Append(c.type); _ = st.Append(',');
            }
            _ = st.Append(c.atk); ; _ = st.Append(',');
            _ = st.Append(c.def); _ = st.Append(',');
            if (hex)
            {
                _ = st.Append("0x" + c.level.ToString("x")); _ = st.Append(',');
                _ = st.Append("0x" + c.race.ToString("x")); _ = st.Append(',');
                _ = st.Append("0x" + c.attribute.ToString("x")); _ = st.Append(',');
                if (c.omega[0] > 0) _ = st.Append("0x" + c.omega[1].ToString("x")); else _ = st.Append("0x0");
                _ = st.Append(',');
                _ = st.Append("0x" + c.category.ToString("x"));
                if (c.omega[0] > 0)
                {
                    _ = st.Append(',');
                    _ = st.Append(string.IsNullOrEmpty(c.script) ? "null" : "'" + c.script.Replace("'", "''") + "'");
                    _ = st.Append(','); _ = st.Append("0x" + c.omega[2].ToString("x"));
                    _ = st.Append(','); _ = st.Append(c.omega[3]);
                    _ = st.Append(','); _ = st.Append(c.omega[4]);
                }
                else _ = st.Append(",null,0x0,253402207200,253402207200");
            }
            else
            {
                _ = st.Append(c.level); _ = st.Append(',');
                _ = st.Append(c.race); _ = st.Append(',');
                _ = st.Append(c.attribute); _ = st.Append(',');
                if (c.omega[0] > 0) _ = st.Append(c.omega[1]); else _ = st.Append('0');
                _ = st.Append(',');
                _ = st.Append(c.category);
                if (c.omega[0] > 0)
                {
                    _ = st.Append(',');
                    _ = st.Append(string.IsNullOrEmpty(c.script) ? "null" : "'" + c.script.Replace("'", "''") + "'");
                    _ = st.Append(','); _ = st.Append(c.omega[2]);
                    _ = st.Append(','); _ = st.Append(c.omega[3]);
                    _ = st.Append(','); _ = st.Append(c.omega[4]);
                }
                else _ = st.Append(",null,0,253402207200,253402207200");
            }
            _ = st.Append(')');
            if (ignore)
            {
                _ = st.Append(";\nINSERT or ignore into texts values(");
            }
            else
            {
                _ = st.Append(";\nINSERT or replace into texts values(");
            }

            _ = st.Append(c.id); _ = st.Append(",'");
            _ = st.Append(c.name.Replace("'", "''")); _ = st.Append("','");
            _ = st.Append(c.desc.Replace("'", "''"));
            for (int i = 0; i < 0x10; i++)
            {
                _ = st.Append("','"); _ = st.Append(c.Str[i].Replace("'", "''"));
            }
            _ = st.Append("');");
            string sql = st.ToString();
            return sql;
        }
        /// <summary>
        /// 转换为插入语句
        /// </summary>
        /// <param name="c">卡片数据</param>
        /// <param name="ignore"></param>
        /// <returns>SQL语句</returns>
        public static string GetInsertSQL(Card c, bool ignore, bool hex = false)
        {
            StringBuilder st = new();
            if (ignore)
            {
                _ = st.Append("INSERT or ignore into datas values(");
            }
            else
            {
                _ = st.Append("INSERT or replace into datas values(");
            }

            _ = st.Append(c.id); _ = st.Append(',');
            _ = st.Append(c.ot); _ = st.Append(',');
            _ = st.Append(c.alias); _ = st.Append(',');
            if (hex)
            {
                _ = st.Append("0x" + c.setcode.ToString("x")); _ = st.Append(',');
                _ = st.Append("0x" + c.type.ToString("x")); _ = st.Append(',');
            }
            else
            {
                _ = st.Append(c.setcode); _ = st.Append(',');
                _ = st.Append(c.type); _ = st.Append(',');
            }
            _ = st.Append(c.atk); ; _ = st.Append(',');
            _ = st.Append(c.def); _ = st.Append(',');
            if (hex)
            {
                _ = st.Append("0x" + c.level.ToString("x")); _ = st.Append(',');
                _ = st.Append("0x" + c.race.ToString("x")); _ = st.Append(',');
                _ = st.Append("0x" + c.attribute.ToString("x")); _ = st.Append(',');
                _ = st.Append("0x" + c.category.ToString("x"));
            }
            else
            {
                _ = st.Append(c.level); _ = st.Append(',');
                _ = st.Append(c.race); _ = st.Append(',');
                _ = st.Append(c.attribute); _ = st.Append(',');
                _ = st.Append(c.category);
            }
            _ = st.Append(')');
            if (ignore)
            {
                _ = st.Append(";\nINSERT or ignore into texts values(");
            }
            else
            {
                _ = st.Append(";\nINSERT or replace into texts values(");
            }

            _ = st.Append(c.id); _ = st.Append(",'");
            _ = st.Append(c.name.Replace("'", "''")); _ = st.Append("','");
            _ = st.Append(c.desc.Replace("'", "''"));
            for (int i = 0; i < 0x10; i++)
            {
                _ = st.Append("','"); _ = st.Append(c.Str[i].Replace("'", "''"));
            }
            _ = st.Append("');");
            string sql = st.ToString();
            return sql;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 转换为更新语句
        /// </summary>
        /// <param name="c">卡片数据</param>
        /// <returns>SQL语句</returns>
        public static string OmegaGetUpdateSQL(Card c)
        {
            StringBuilder st = new();
            _ = st.Append("update datas set ot="); _ = st.Append(c.ot);
            _ = st.Append(",alias="); _ = st.Append(c.alias);
            _ = st.Append(",setcode="); _ = st.Append(c.setcode);
            _ = st.Append(",type="); _ = st.Append(c.type);
            _ = st.Append(",atk="); _ = st.Append(c.atk);
            _ = st.Append(",def="); _ = st.Append(c.def);
            _ = st.Append(",level="); _ = st.Append(c.level);
            _ = st.Append(",race="); _ = st.Append(c.race);
            _ = st.Append(",attribute="); _ = st.Append(c.attribute);
            _ = st.Append(",category=");
            if (c.omega[0] > 0)
            {
                _ = st.Append(c.omega[1]);
                _ = st.Append(",script="); _ = st.Append(string.IsNullOrEmpty(c.script) ? "null" : "'" + c.script.Replace("'", "''") + "'");
                _ = st.Append(",ocgdate="); _ = st.Append((DateTime.Parse(c.GetDate()).Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000);
                _ = st.Append(",tcgdate="); _ = st.Append((DateTime.Parse(c.GetDate(1)).Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000);
                _ = st.Append(",genre=");
            }
            _ = st.Append(c.category);
            _ = st.Append(" where id="); _ = st.Append(c.id);
            _ = st.Append("; update texts set name='"); _ = st.Append(c.name.Replace("'", "''"));
            _ = st.Append("',desc='"); _ = st.Append(c.desc.Replace("'", "''")); _ = st.Append("', ");
            for (int i = 0; i < 0x10; i++)
            {
                _ = st.Append("str"); _ = st.Append(i + 1); _ = st.Append("='");
                _ = st.Append(c.Str[i].Replace("'", "''"));
                if (i < 15)
                {
                    _ = st.Append("',");
                }
            }
            _ = st.Append("' where id="); _ = st.Append(c.id);
            _ = st.Append(';');
            string sql = st.ToString();
            return sql;
        }
        /// <summary>
        /// 转换为更新语句
        /// </summary>
        /// <param name="c">卡片数据</param>
        /// <returns>SQL语句</returns>
        public static string GetUpdateSQL(Card c)
        {
            StringBuilder st = new();
            _ = st.Append("update datas set ot="); _ = st.Append(c.ot);
            _ = st.Append(",alias="); _ = st.Append(c.alias);
            _ = st.Append(",setcode="); _ = st.Append(c.setcode);
            _ = st.Append(",type="); _ = st.Append(c.type);
            _ = st.Append(",atk="); _ = st.Append(c.atk);
            _ = st.Append(",def="); _ = st.Append(c.def);
            _ = st.Append(",level="); _ = st.Append(c.level);
            _ = st.Append(",race="); _ = st.Append(c.race);
            _ = st.Append(",attribute="); _ = st.Append(c.attribute);
            _ = st.Append(",category="); _ = st.Append(c.category);
            _ = st.Append(" where id="); _ = st.Append(c.id);
            _ = st.Append("; update texts set name='"); _ = st.Append(c.name.Replace("'", "''"));
            _ = st.Append("',desc='"); _ = st.Append(c.desc.Replace("'", "''")); _ = st.Append("', ");
            for (int i = 0; i < 0x10; i++)
            {
                _ = st.Append("str"); _ = st.Append(i + 1); _ = st.Append("='");
                _ = st.Append(c.Str[i].Replace("'", "''"));
                if (i < 15)
                {
                    _ = st.Append("',");
                }
            }
            _ = st.Append("' where id="); _ = st.Append(c.id);
            _ = st.Append(';');
            string sql = st.ToString();
            return sql;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 转换删除语句
        /// </summary>
        /// <param name="c">卡片密码</param>
        /// <returns>SQL语句</returns>
        public static string GetDeleteSQL(Card c)
        {
            string id = c.id.ToString();
            return "Delete from datas where id=" + id + ";Delete from texts where id=" + id + ";";
        }
        #endregion
        #endregion

        public static void ExportSql(string file, params Card[] cards)
        {
            using FileStream fs = new(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new(fs, Encoding.UTF8);
            foreach (Card c in cards)
            {
                sw.WriteLine(c.omega[0] > 0 ? OmegaGetInsertSQL(c, false, true) : GetInsertSQL(c, false, true));
            }
            sw.Close();
        }
        public static CardPack FindPack(string db, long id)
        {
            CardPack cardpack = null;
            if (File.Exists(db) && id >= 0)
            {
                using (SqliteConnection sqliteconn = new(@"Data Source=" + db))
                {
                    sqliteconn.Open();
                    using (SqliteCommand sqlitecommand = sqliteconn.CreateCommand())
                    {
                        sqlitecommand.CommandText = "select id,pack_id,pack,rarity,date from pack where id=" + id + " order by date desc";
                        using (SqliteDataReader reader = sqlitecommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cardpack = new CardPack(id)
                                {
                                    pack_id = reader.GetString(1),
                                    pack_name = reader.GetString(2),
                                    rarity = reader.GetString(3),
                                    date = reader.GetString(4)
                                };
                            }
                            reader.Close();
                        }
                    }
                    sqliteconn.Close();
                }
            }
            return cardpack;
        }
    }
}
