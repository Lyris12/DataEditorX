using DataEditorX.Config;
using DataEditorX.Language;
using System.Collections.Generic;
using System.IO;
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
            this.addCard = new AddCommand(this);
            this.modCard = new ModCommand(this);
            this.delCard = new DelCommand(this);
            this.copyCard = new CopyCommand(this);
        }

        #region 添加
        //添加
        public class AddCommand : IBackableCommand
        {
            private string undoSQL;
            readonly IDataForm dataform;
            public AddCommand(CardEdit cardedit)
            {
                this.dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!this.dataform.CheckOpen())
                {
                    return false;
                }

                Card c = this.dataform.GetCard();
                if (c.id <= 0)//卡片密码不能小于等于0
                {
                    MyMsg.Error(LMSG.CodeCanNotIsZero);
                    return false;
                }
                else if (c.id > 268435455)
                    return false;
                Card[] cards = this.dataform.GetCardList(false);
                foreach (Card ckey in cards)//卡片id存在
                {
                    if (c.id == ckey.id)
                    {
                        MyMsg.Warning(LMSG.ItIsExists);
                        return false;
                    }
                }
                if (DataBase.Command(this.dataform.GetOpenFile(),
                    (this.dataform.GetOpenFile().EndsWith(".db", System.StringComparison.OrdinalIgnoreCase) || this.dataform.GetOpenFile().EndsWith(".bytes", System.StringComparison.OrdinalIgnoreCase) ? DataBase.OmegaGetInsertSQL(c, true) : DataBase.GetInsertSQL(c, true))) >= 2)
                {
                    MyMsg.Show(LMSG.AddSucceed);
                    this.undoSQL = DataBase.GetDeleteSQL(c);
                    this.dataform.Search(true);
                    this.dataform.SetCard(c);
                    return true;
                }
                MyMsg.Error(LMSG.AddFail);
                return false;
            }
            public void Undo()
            {
                DataBase.Command(this.dataform.GetOpenFile(), this.undoSQL);
            }

            public object Clone()
            {
                return this.MemberwiseClone();
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
                this.dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!this.dataform.CheckOpen())
                {
                    return false;
                }

                bool modfiles = (bool)args[0];

                Card c = this.dataform.GetCard();
                Card oldCard = this.dataform.GetOldCard();
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
                    sql = (this.dataform.GetOpenFile().EndsWith(".db", System.StringComparison.OrdinalIgnoreCase) || this.dataform.GetOpenFile().EndsWith(".bytes", System.StringComparison.OrdinalIgnoreCase)) ? DataBase.OmegaGetInsertSQL(c, false) : DataBase.GetInsertSQL(c, false);//插入
                    bool delold = MyMsg.Question(LMSG.IfDeleteCard);
                    if (delold)//是否删除旧卡片
                    {
                        if (DataBase.Command(this.dataform.GetOpenFile(),
                            DataBase.GetDeleteSQL(oldCard)) < 2)
                        {
                            //删除失败
                            MyMsg.Error(LMSG.DeleteFail);
                            delold = false;
                        }
                        else
                        {//删除成功，添加还原sql
                            this.undoSQL = DataBase.GetDeleteSQL(c) + ((this.dataform.GetOpenFile().EndsWith(".db", System.StringComparison.OrdinalIgnoreCase) || this.dataform.GetOpenFile().EndsWith(".bytes", System.StringComparison.OrdinalIgnoreCase)) ? DataBase.OmegaGetInsertSQL(oldCard, false) : DataBase.GetInsertSQL(oldCard, false));
                        }
                    }
                    else
                    {
                        this.undoSQL = DataBase.GetDeleteSQL(c);//还原就是删除
                    }
                    //如果删除旧卡片，则把资源修改名字,否则复制资源
                    if (modfiles)
                    {
                        if (delold)
                        {
                            YGOUtil.CardRename(c.id, oldCard.id, this.dataform.GetPath());
                        }
                        else
                        {
                            YGOUtil.CardCopy(c.id, oldCard.id, this.dataform.GetPath());
                        }

                        this.modifiled = true;
                        this.oldid = oldCard.id;
                        this.newid = c.id;
                        this.delold = delold;
                    }
                }
                else
                {//更新数据
                    sql = (this.dataform.GetOpenFile().EndsWith(".db", System.StringComparison.OrdinalIgnoreCase)) || this.dataform.GetOpenFile().EndsWith(".bytes", System.StringComparison.OrdinalIgnoreCase) ? DataBase.OmegaGetUpdateSQL(c) : DataBase.GetUpdateSQL(c);
                    this.undoSQL = (this.dataform.GetOpenFile().EndsWith(".db", System.StringComparison.OrdinalIgnoreCase)) || this.dataform.GetOpenFile().EndsWith(".bytes", System.StringComparison.OrdinalIgnoreCase) ? DataBase.OmegaGetUpdateSQL(oldCard) : DataBase.GetUpdateSQL(oldCard);
                }
                if (DataBase.Command(this.dataform.GetOpenFile(), sql) > 0)
                {
                    MyMsg.Show(LMSG.ModifySucceed);
                    this.dataform.Search(true);
                    this.dataform.SetCard(c);
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
                DataBase.Command(this.dataform.GetOpenFile(), this.undoSQL);
                if (this.modifiled)
                {
                    if (this.delold)
                    {
                        YGOUtil.CardRename(this.oldid, this.newid, this.dataform.GetPath());
                    }
                    else
                    {
                        YGOUtil.CardDelete(this.newid, this.dataform.GetPath());
                    }
                }
            }

            public object Clone()
            {
                return this.MemberwiseClone();
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
                this.dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!this.dataform.CheckOpen())
                {
                    return false;
                }

                bool deletefiles = (bool)args[0];

                Card[] cards = this.dataform.GetCardList(true);
                if (cards == null || cards.Length == 0)
                {
                    return false;
                }

                string undo = "";
                if (!MyMsg.Question(LMSG.IfDeleteCard))
                {
                    return false;
                }

                List<string> sql = new List<string>();
                foreach (Card c in cards)
                {
                    sql.Add(DataBase.GetDeleteSQL(c));//删除
                    undo += (this.dataform.GetOpenFile().EndsWith(".db", System.StringComparison.OrdinalIgnoreCase) || this.dataform.GetOpenFile().EndsWith(".bytes", System.StringComparison.OrdinalIgnoreCase)) ? DataBase.OmegaGetInsertSQL(c, true) : DataBase.GetInsertSQL(c, true);
                    //删除资源
                    if (deletefiles)
                    {
                        YGOUtil.CardDelete(c.id, this.dataform.GetPath());
                    }
                }
                if (DataBase.Command(this.dataform.GetOpenFile(), sql.ToArray()) >= (sql.Count * 2))
                {
                    MyMsg.Show(LMSG.DeleteSucceed);
                    this.dataform.Search(true);
                    this.undoSQL = undo;
                    return true;
                }
                else
                {
                    MyMsg.Error(LMSG.DeleteFail);
                    this.dataform.Search(true);
                }
                return false;
            }
            public void Undo()
            {
                DataBase.Command(this.dataform.GetOpenFile(), this.undoSQL);
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
        #endregion

        #region 打开脚本
        //打开脚本
        public bool OpenScript(bool openinthis, string addrequire)
        {
            if (!this.dataform.CheckOpen())
            {
                return false;
            }

            Card c = this.dataform.GetCard();
            long id = c.id;
            string lua;
            if (c.id > 0)
            {
                lua = this.dataform.GetPath().GetScript(id);
            }
            else if (addrequire.Length > 0)
            {
                lua = this.dataform.GetPath().GetModuleScript(addrequire);
            }
            else
            {
                return false;
            }
            if (!File.Exists(lua))
            {
                MyPath.CreateDirByFile(lua);
                if (MyMsg.Question(LMSG.IfCreateScript))//是否创建脚本
                {
                    using (FileStream fs = new FileStream(lua,
                        FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false));
                        if (string.IsNullOrEmpty(addrequire))
                        {
                            // OCG script
                            sw.WriteLine("--" + c.name);
                            sw.WriteLine("function c" + id.ToString() + ".initial_effect(c)");
                            sw.WriteLine("\t");
                            sw.WriteLine("end");
                        }
                        else
                        {
                            // DIY script
                            sw.WriteLine("--" + c.name);
                            sw.WriteLine("local id=" + id.ToString());
                            sw.WriteLine("local this=_G[\"c\"..m]");
                            sw.WriteLine("Duel.LoadScript(\"" + addrequire + ".lua\")");
                            sw.WriteLine("function this.initial_effect(c)");
                            sw.WriteLine("\t");
                            sw.WriteLine("end");
                        }
                        /*else
                        { //module script
                            sw.WriteLine("--Module script \"" + addrequire + "\"");
                        }*/
                        sw.Close();
                        fs.Close();
                    }
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
                    System.Diagnostics.Process.Start(lua);
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
                this.dataform = cardedit.dataform;
            }

            public bool Execute(params object[] args)
            {
                if (!this.dataform.CheckOpen())
                {
                    return false;
                }

                Card[] cards = (Card[])args[0];

                if (cards == null || cards.Length == 0)
                {
                    return false;
                }

                bool replace = false;
                Card[] oldcards = DataBase.Read(this.dataform.GetOpenFile(), true, "");
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
                DataBase.CopyDB(this.dataform.GetOpenFile(), !replace, cards);
                this.copied = true;
                this.newCards = cards;
                this.replace = replace;
                this.oldCards = oldcards;
                return true;
            }
            public void Undo()
            {
                DataBase.DeleteDB(this.dataform.GetOpenFile(), this.newCards);
                DataBase.CopyDB(this.dataform.GetOpenFile(), !this.replace, this.oldCards);
            }

            public object Clone()
            {
                CopyCommand replica = new CopyCommand(this.cardedit)
                {
                    copied = this.copied,
                    newCards = (Card[])this.newCards.Clone(),
                    replace = this.replace
                };
                if (this.oldCards != null)
                {
                    replica.oldCards = (Card[])this.oldCards.Clone();
                }

                return replica;
            }
        }
        #endregion
    }
}
