using DataEditorX.Config;
using DataEditorX.Language;
using System.Text;

namespace DataEditorX.Core
{
    public class CardEdit
    {
        readonly IDataForm dataform;
        public AddCommand addCard;
        public ModCommand modCard;
        public DelCommand delCard;
        public CopyCommand copyCard;

        public CardEdit(IDataForm dataform)
        {
            this.dataform = dataform;
            addCard = new AddCommand(this);
            modCard = new ModCommand(this);
            delCard = new DelCommand(this);
            copyCard = new CopyCommand(this);
        }

        #region 添加
        //添加
        public class AddCommand : IBackableCommand
        {
            private string undoSQL;
            readonly IDataForm dataform;
            public AddCommand(CardEdit cardedit)
            {
                dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!dataform.CheckOpen())
                {
                    return false;
                }

                Card c = dataform.GetCard();
                if (c.id <= 0)//卡片密码不能小于等于0
                {
                    MyMsg.Error(LMSG.CodeCanNotIsZero);
                    return false;
                }
                else if (c.id > 268435455)
                {
                    MyMsg.Error(LMSG.AddFail);
                    return false;
                }
                else if (c.omega[0] > 0)
                {
                    if (c.ot > 0x7)
                    {
                        MyMsg.Error(LMSG.AddFail);
                        return false;
                    }
                    else if (c.id > 134217727)
                        MyMsg.Warning("Strings will fail to show up for this passcode.");
                }
                Card[] cards = dataform.GetCardList(false);
                foreach (Card ckey in cards)//卡片id存在
                {
                    if (c.id == ckey.id)
                    {
                        MyMsg.Warning(LMSG.ItIsExists);
                        return false;
                    }
                }
                if (DataBase.Command(dataform.GetOpenFile(),
                    (dataform.GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase) || dataform.GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase) ? DataBase.OmegaGetInsertSQL(c, true) : DataBase.GetInsertSQL(c, true))) >= 2)
                {
                    MyMsg.Show(LMSG.AddSucceed);
                    undoSQL = DataBase.GetDeleteSQL(c);
                    dataform.Search(true);
                    dataform.SetCard(c);
                    return true;
                }
                MyMsg.Error(LMSG.AddFail);
                return false;
            }
            public void Undo()
            {
                _ = DataBase.Command(dataform.GetOpenFile(), undoSQL);
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }
        #endregion

        #region 修改
        //修改
        public class ModCommand : IBackableCommand
        {
            private string undoSQL;
            private bool modifiled = false;
            private long oldid;
            private long newid;
            private bool delold;
            readonly IDataForm dataform;
            public ModCommand(CardEdit cardedit)
            {
                dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!dataform.CheckOpen())
                {
                    return false;
                }

                bool modfiles = (bool)args[0];

                Card c = dataform.GetCard();
                Card oldCard = dataform.GetOldCard();
                if (c.Equals(oldCard))//没有修改
                {
                    MyMsg.Show(LMSG.ItIsNotChanged);
                    return false;
                }
                if (c.id <= 0)
                {
                    MyMsg.Error(LMSG.CodeCanNotIsZero);
                    return false;
                }
                string sql;
                if (c.id != oldCard.id)//修改了id
                {
                    sql = (dataform.GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase) || dataform.GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)) ? DataBase.OmegaGetInsertSQL(c, false) : DataBase.GetInsertSQL(c, false);//插入
                    bool delold = MyMsg.Question(LMSG.IfDeleteCard);
                    if (delold)//是否删除旧卡片
                    {
                        if (DataBase.Command(dataform.GetOpenFile(),
                            DataBase.GetDeleteSQL(oldCard)) < 2)
                        {
                            //删除失败
                            MyMsg.Error(LMSG.DeleteFail);
                            delold = false;
                        }
                        else
                        {//删除成功，添加还原sql
                            undoSQL = DataBase.GetDeleteSQL(c) + ((dataform.GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase) || dataform.GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)) ? DataBase.OmegaGetInsertSQL(oldCard, false) : DataBase.GetInsertSQL(oldCard, false));
                        }
                    }
                    else
                    {
                        undoSQL = DataBase.GetDeleteSQL(c);//还原就是删除
                    }
                    //如果删除旧卡片，则把资源修改名字,否则复制资源
                    if (modfiles)
                    {
                        if (delold)
                        {
                            YGOUtil.CardRename(c.id, oldCard.id, dataform.GetPath());
                        }
                        else
                        {
                            YGOUtil.CardCopy(c.id, oldCard.id, dataform.GetPath());
                        }

                        modifiled = true;
                        oldid = oldCard.id;
                        newid = c.id;
                        this.delold = delold;
                    }
                }
                else
                {//更新数据
                    sql = (dataform.GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase)) || dataform.GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase) ? DataBase.OmegaGetUpdateSQL(c) : DataBase.GetUpdateSQL(c);
                    undoSQL = (dataform.GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase)) || dataform.GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase) ? DataBase.OmegaGetUpdateSQL(oldCard) : DataBase.GetUpdateSQL(oldCard);
                }
                if (DataBase.Command(dataform.GetOpenFile(), sql) > 0)
                {
                    MyMsg.Show(LMSG.ModifySucceed);
                    dataform.Search(true);
                    dataform.SetCard(c);
                    return true;
                }
                else
                {
                    MyMsg.Error(LMSG.ModifyFail);
                }

                return false;
            }

            public void Undo()
            {
                _ = DataBase.Command(dataform.GetOpenFile(), undoSQL);
                if (modifiled)
                {
                    if (delold)
                    {
                        YGOUtil.CardRename(oldid, newid, dataform.GetPath());
                    }
                    else
                    {
                        YGOUtil.CardDelete(newid, dataform.GetPath());
                    }
                }
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }
        #endregion

        #region 删除
        //删除
        public class DelCommand : IBackableCommand
        {
            private string undoSQL;
            readonly IDataForm dataform;
            public DelCommand(CardEdit cardedit)
            {
                dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!dataform.CheckOpen())
                {
                    return false;
                }

                bool deletefiles = (bool)args[0];

                Card[] cards = dataform.GetCardList(true);
                if (cards == null || cards.Length == 0)
                {
                    return false;
                }

                string undo = "";
                if (!MyMsg.Question(LMSG.IfDeleteCard))
                {
                    return false;
                }

                List<string> sql = new();
                foreach (Card c in cards)
                {
                    sql.Add(DataBase.GetDeleteSQL(c));//删除
                    undo += (dataform.GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase) || dataform.GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)) ? DataBase.OmegaGetInsertSQL(c, true) : DataBase.GetInsertSQL(c, true);
                    //删除资源
                    if (deletefiles)
                    {
                        YGOUtil.CardDelete(c.id, dataform.GetPath());
                    }
                }
                if (DataBase.Command(dataform.GetOpenFile(), sql.ToArray()) >= (sql.Count * 2))
                {
                    MyMsg.Show(LMSG.DeleteSucceed);
                    dataform.Search(true);
                    undoSQL = undo;
                    return true;
                }
                else
                {
                    MyMsg.Error(LMSG.DeleteFail);
                    dataform.Search(true);
                }
                return false;
            }
            public void Undo()
            {
                _ = DataBase.Command(dataform.GetOpenFile(), undoSQL);
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }
        #endregion

        #region 打开脚本
        //打开脚本
        public bool OpenScript(bool openinthis, string addrequire)
        {
            if (!dataform.CheckOpen())
            {
                return false;
            }

            Card c = dataform.GetCard();
            long id = c.id;
            string lua;
            if (c.id > 0)
            {
                lua = dataform.GetPath().GetScript(id);
                if (c.omega[0] > 0 && !(dataform.GetOpenFile().EndsWith(".cdb") && File.Exists(lua)))
                {
                    lua = MyPath.Combine(dataform.GetPath().gamepath, "../Scripts", "c" + id + ".lua");
                    if (c.omega[0] > 0 && !string.IsNullOrEmpty(c.script)
                        && !byte.TryParse(c.script, out _) && !File.Exists(lua) && openinthis)
                    {
                        DEXConfig.OpenFileInThis(id.ToString() + "```" + c.script);
                        return true;
                    }
                }
            }
            else if (addrequire.Length > 0)
            {
                lua = dataform.GetPath().GetModuleScript(addrequire);
            }
            else
            {
                return false;
            }
            if (!File.Exists(lua))
            {
                if (c.omega[0] > 0 && !string.IsNullOrEmpty(c.script) && !byte.TryParse(c.script, out _)
                    && !openinthis)
                {
                    MyPath.CreateDirByFile(lua);
                    using FileStream fs = new(lua,
                        FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new(fs, new UTF8Encoding(false));
                    sw.Write(c.script);
                    sw.Close();
                    fs.Close();
                }
                if (MyMsg.Question(LMSG.IfCreateScript))//是否创建脚本
                {
                    MyPath.CreateDirByFile(lua);
                    using FileStream fs = new(lua,
                        FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new(fs, new UTF8Encoding(false));
                    sw.WriteLine("--" + c.name);
                    sw.WriteLine("local s,id,o=GetID()");
                    if (!string.IsNullOrEmpty(addrequire))
                        sw.WriteLine("Duel.LoadScript(\"" + addrequire + ".lua\")"); // DIY script
                    sw.WriteLine("function s.initial_effect(c)");
                    sw.WriteLine("\t");
                    sw.WriteLine("end");
                    sw.Close();
                    fs.Close();
                }
            }
            if (File.Exists(lua))//如果存在，则打开文件
            {
                if (openinthis)//是否用本程序打开
                {
                    DEXConfig.OpenFileInThis(lua);
                }
                else
                {
                    _ = System.Diagnostics.Process.Start(lua);
                }

                return true;
            }
            return false;
        }
        #endregion

        #region 复制卡片
        public class CopyCommand : IBackableCommand
        {
            bool copied = false;
            Card[] newCards;
            bool replace;
            Card[] oldCards;
            readonly CardEdit cardedit;
            readonly IDataForm dataform;
            public CopyCommand(CardEdit cardedit)
            {
                this.cardedit = cardedit;
                dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!dataform.CheckOpen())
                {
                    return false;
                }

                Card[] cards = (Card[])args[0];

                if (cards == null || cards.Length == 0)
                {
                    return false;
                }

                bool replace = false;
                Card[] oldcards = DataBase.Read(dataform.GetOpenFile(), true, "");
                if (oldcards != null && oldcards.Length != 0)
                {
                    int i = 0;
                    foreach (Card oc in oldcards)
                    {
                        foreach (Card c in cards)
                        {
                            if (c.id == oc.id)
                            {
                                i += 1;
                                if (i == 1)
                                {
                                    replace = MyMsg.Question(LMSG.IfReplaceExistingCard);
                                    break;
                                }
                            }
                        }
                        if (i > 0)
                        {
                            break;
                        }
                    }
                }
                _ = DataBase.CopyDB(dataform.GetOpenFile(), !replace, cards);
                copied = true;
                newCards = cards;
                this.replace = replace;
                oldCards = oldcards;
                return true;
            }
            public void Undo()
            {
                _ = DataBase.DeleteDB(dataform.GetOpenFile(), newCards);
                _ = DataBase.CopyDB(dataform.GetOpenFile(), !replace, oldCards);
            }

            public object Clone()
            {
                CopyCommand replica = new(cardedit)
                {
                    copied = copied,
                    newCards = (Card[])newCards.Clone(),
                    replace = replace
                };
                if (oldCards != null)
                {
                    replica.oldCards = (Card[])oldCards.Clone();
                }

                return replica;
            }
        }
        #endregion
    }
}
