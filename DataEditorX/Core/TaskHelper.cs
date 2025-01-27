﻿/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-12
 * 时间: 19:43
 * 
 */
using DataEditorX.Common;
using DataEditorX.Config;
using DataEditorX.Core.Info;
using DataEditorX.Core.Mse;
using DataEditorX.Language;
using System.ComponentModel;
using System.IO.Compression;

namespace DataEditorX.Core
{
    /// <summary>
    /// 任务
    /// </summary>
    public class TaskHelper
    {
        #region Member
        /// <summary>
        /// 当前任务
        /// </summary>
        private MyTask nowTask = MyTask.NONE;
        /// <summary>
        /// 上一次任务
        /// </summary>
        private MyTask lastTask = MyTask.NONE;
        /// <summary>
        /// 当前卡片列表
        /// </summary>
        private Card[] cardlist;
        /// <summary>
        /// 当前卡片列表
        /// </summary>
        public Card[] CardList
        {
            get { return cardlist; }
        }
        /// <summary>
        /// 任务参数
        /// </summary>
        private string[] mArgs;
        /// <summary>
        /// 图片设置
        /// </summary>
        private readonly ImageSet imgSet;
        /// <summary>
        /// MSE转换
        /// </summary>
        private readonly MseMaker mseHelper;
        /// <summary>
        /// 是否取消
        /// </summary>
        private bool isCancel = false;
        /// <summary>
        /// 是否在运行
        /// </summary>
        private bool isRun = false;
        /// <summary>
        /// 后台工作线程
        /// </summary>
        private readonly BackgroundWorker worker;

        public TaskHelper(string datapath, BackgroundWorker worker, MSEConfig mcfg)
        {
            Datapath = datapath;
            this.worker = worker;
            mseHelper = new MseMaker(mcfg);
            imgSet = new ImageSet();
        }
        public MseMaker MseHelper
        {
            get { return mseHelper; }
        }
        public bool IsRuning()
        {
            return isRun;
        }
        public bool IsCancel()
        {
            return isCancel;
        }
        public void Cancel()
        {
            isRun = false;
            isCancel = true;
        }
        public MyTask GetLastTask()
        {
            return lastTask;
        }

        public void TestPendulumText(string desc)
        {
            mseHelper.TestPendulum(desc);
        }
        #endregion

        #region Other
        //设置任务
        public void SetTask(MyTask myTask, Card[] cards, params string[] args)
        {
            nowTask = myTask;
            cardlist = cards;
            mArgs = args;
        }
        //转换图片
        //public void ToImg(string img, string saveimg1, string saveimg2)
        public void ToImg(string img, string saveimg1)
        {
            if (!File.Exists(img))
            {
                return;
            }

            Bitmap bmp = new(img);
            _ = MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.W, imgSet.H),
                                saveimg1, imgSet.quality);
            //MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.w, imgSet.h),
            //					saveimg2, imgSet.quilty);
            bmp.Dispose();
        }
        #endregion

        #region 检查更新
        public static void CheckVersion(bool showNew)
        {
            string newver = CheckUpdate.GetNewVersion(DEXConfig.ReadString(DEXConfig.TAG_UPDATE_URL));
            if (newver == CheckUpdate.DEFAULT)
            {   //检查失败
                if (!showNew)
                {
                    return;
                }

                MyMsg.Error(LMSG.CheckUpdateFail);
                return;
            }

            if (CheckUpdate.CheckVersion(newver, Application.ProductVersion))
            {//有最新版本
                if (!MyMsg.Question(LMSG.HaveNewVersion))
                {
                    return;
                }
            }
            else
            {//现在就是最新版本
                if (!showNew)
                {
                    return;
                }

                if (!MyMsg.Question(LMSG.NowIsNewVersion))
                {
                    return;
                }
            }
            //下载文件
            if (CheckUpdate.DownLoad(
                MyPath.Combine(Application.StartupPath, newver + ".zip")))
            {
                MyMsg.Show(LMSG.DownloadSucceed);
            }
            else
            {
                MyMsg.Show(LMSG.DownloadFail);
            }
        }
        public static void OnCheckUpdate(bool showNew)
        {
            CheckVersion(showNew);
        }
        #endregion

        #region 裁剪图片
        public void CutImages(string imgpath, bool isreplace)
        {
            int count = cardlist.Length;
            int i = 0;
            foreach (Card c in cardlist)
            {
                if (isCancel)
                {
                    break;
                }

                i++;
                worker.ReportProgress(i / count, string.Format("{0}/{1}", i, count));
                string jpg = MyPath.Combine(imgpath, c.id + ".jpg");
                string savejpg = MyPath.Combine(mseHelper.ImagePath, c.id + ".jpg");
                if (File.Exists(jpg) && (isreplace || !File.Exists(savejpg)))
                {
                    Bitmap bp = new(jpg);
                    Bitmap bmp;
                    if (c.IsType(CardType.TYPE_XYZ))//超量
                    {
                        bmp = MyBitmap.Cut(bp, imgSet.xyzArea);
                    }
                    else if (c.IsType(CardType.TYPE_PENDULUM))//P怪兽
                    {
                        bmp = MyBitmap.Cut(bp, imgSet.pendulumArea);
                    }
                    else//一般
                    {
                        bmp = MyBitmap.Cut(bp, imgSet.normalArea);
                    }
                    bp.Dispose();
                    _ = MyBitmap.SaveAsJPEG(bmp, savejpg, imgSet.quality);
                    //bmp.Save(savejpg, ImageFormat.Png);
                }
            }
        }
        #endregion

        //removed thumbnail
        #region 转换图片
        public void ConvertImages(string imgpath, string gamepath, bool isreplace)
        {
            string picspath = MyPath.Combine(gamepath, "pics");
            //string thubpath = MyPath.Combine(picspath, "thumbnail");
            string[] files = Directory.GetFiles(imgpath);
            int i = 0;
            int count = files.Length;

            foreach (string f in files)
            {
                if (isCancel)
                {
                    break;
                }

                i++;
                worker.ReportProgress(i / count, string.Format("{0}/{1}", i, count));
                string ex = Path.GetExtension(f).ToLower();
                string name = Path.GetFileNameWithoutExtension(f);
                string jpg_b = MyPath.Combine(picspath, name + ".jpg");
                //string jpg_s = MyPath.Combine(thubpath, name + ".jpg");
                if (ex == ".jpg" || ex == ".png" || ex == ".bmp")
                {
                    if (File.Exists(f))
                    {
                        Bitmap bmp = new(f);
                        //大图，如果替换，或者不存在
                        if (isreplace || !File.Exists(jpg_b))
                        {

                            _ = MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.W, imgSet.H),
                                                jpg_b, imgSet.quality);
                        }
                        //小图，如果替换，或者不存在
                        //if (isreplace || !File.Exists(jpg_s))
                        //{
                        //	MyBitmap.SaveAsJPEG(MyBitmap.Zoom(bmp, imgSet.w, imgSet.h),
                        //						jpg_s, imgSet.quilty);

                        //}
                    }
                }
            }
        }
        #endregion

        #region MSE存档
        public string MSEImagePath
        {
            get { return mseHelper.ImagePath; }
        }

        public string Datapath { get; }

        public void SaveMSEs(string file, Card[] cards, bool isUpdate)
        {
            if (cards == null)
            {
                return;
            }
            string pack_db = MyPath.GetRealPath(DEXConfig.ReadString("pack_db"));
            bool rarity = DEXConfig.ReadBoolean("mse_auto_rarity", false);
            int c = cards.Length;
            //不分开，或者卡片数小于单个存档的最大值
            if (mseHelper.MaxNum == 0 || c < mseHelper.MaxNum)
            {
                SaveMSE(1, file, cards, pack_db, rarity, isUpdate);
            }
            else
            {
                int nums = c / mseHelper.MaxNum;
                if (nums * mseHelper.MaxNum < c)//计算需要分多少个存档
                {
                    nums++;
                }

                List<Card> clist = new();
                for (int i = 0; i < nums; i++)//分别生成存档
                {
                    clist.Clear();
                    for (int j = 0; j < mseHelper.MaxNum; j++)
                    {
                        int index = i * mseHelper.MaxNum + j;
                        if (index < c)
                        {
                            clist.Add(cards[index]);
                        }
                    }
                    int t = file.LastIndexOf(".mse-set");
                    string fname = (t > 0) ? file[..t] : file;
                    fname += string.Format("_{0}.mse-set", i + 1);
                    SaveMSE(i + 1, fname, clist.ToArray(), pack_db, rarity, isUpdate);
                }
            }
        }
        public void SaveMSE(int num, string file, Card[] cards, string pack_db, bool rarity, bool isUpdate)
        {
            string setFile = file + ".txt";
            Dictionary<Card, string> images = mseHelper.WriteSet(setFile, cards, pack_db, rarity);
            if (isUpdate)//仅更新文字
            {
                return;
            }

            int i = 0;
            int count = images.Count;
            if (File.Exists(file)) File.Delete(file);
            ZipArchive zips = ZipFile.Open(file, ZipArchiveMode.Create, System.Text.Encoding.UTF8);
            zips.CreateEntryFromFile(setFile, "set");
            foreach (Card c in images.Keys)
            {
                string img = images[c];
                if (isCancel)
                {
                    break;
                }

                i++;
                worker.ReportProgress(i / count, string.Format("{0}/{1}-{2}", i, count, num));
                //TODO 先裁剪图片
                zips.CreateEntryFromFile(img, Path.GetFileName(img));
            }
            zips.Dispose();
            File.Delete(setFile);
        }
        public Card[] ReadMSE(string mseset, bool repalceOld)
        {
            //解压所有文件
            using (ZipStorer zips = ZipStorer.Open(mseset, FileAccess.Read))
            {
                zips.EncodeUTF8 = true;
                List<ZipStorer.ZipFileEntry> files = zips.ReadCentralDir();
                int count = files.Count;
                int i = 0;
                foreach (ZipStorer.ZipFileEntry file in files)
                {
                    worker.ReportProgress(i / count, string.Format("{0}/{1}", i, count));
                    string savefilename = MyPath.Combine(mseHelper.ImagePath, file.FilenameInZip);
                    _ = zips.ExtractFile(file, savefilename);
                }
            }
            string setfile = MyPath.Combine(mseHelper.ImagePath, "set");
            return mseHelper.ReadCards(setfile, repalceOld);
        }
        #endregion

        #region 导出数据
        public void ExportData(string path, string zipname, string _cdbfile, string modulescript)
        {
            int i = 0;
            Card[] cards = cardlist;
            if (cards == null || cards.Length == 0)
            {
                return;
            }

            int count = cards.Length;
            YgoPath ygopath = new(path);
            string name = Path.GetFileNameWithoutExtension(zipname);
            //数据库
            string cdbfile = zipname + ".cdb";
            //说明
            string readme = MyPath.Combine(path, name + ".txt");
            //新卡ydk
            string deckydk = ygopath.GetYdk(name);
            //module scripts
            string extra_script = "";
            if (modulescript.Length > 0)
            {
                extra_script = ygopath.GetModuleScript(modulescript);
            }

            File.Delete(cdbfile);
            _ = DataBase.Create(cdbfile);
            _ = DataBase.CopyDB(cdbfile, false, cardlist);
            if (File.Exists(zipname))
            {
                File.Delete(zipname);
            }

            using (ZipStorer zips = ZipStorer.Create(zipname, ""))
            {
                zips.AddFile(cdbfile, Path.GetFileNameWithoutExtension(_cdbfile) + ".cdb", "");
                if (File.Exists(readme))
                {
                    zips.AddFile(readme, "readme_" + name + ".txt", "");
                }

                if (File.Exists(deckydk))
                {
                    zips.AddFile(deckydk, "deck/" + name + ".ydk", "");
                }

                if (modulescript.Length > 0 && File.Exists(extra_script))
                {
                    zips.AddFile(extra_script, extra_script.Replace(path, ""), "");
                }

                foreach (Card c in cards)
                {
                    i++;
                    worker.ReportProgress(i / count, string.Format("{0}/{1}", i, count));
                    string[] files = ygopath.GetCardfiles(c.id);
                    foreach (string file in files)
                    {
                        if (!string.Equals(file, extra_script) && File.Exists(file))
                        {
                            zips.AddFile(file, file.Replace(path, ""), "");
                        }
                    }
                }
            }
            File.Delete(cdbfile);
        }
        #endregion

        #region 运行
        public void Run()
        {
            isCancel = false;
            isRun = true;
            bool replace;
            bool showNew;
            switch (nowTask)
            {
                case MyTask.ExportData:
                    if (mArgs != null && mArgs.Length >= 3)
                    {
                        ExportData(mArgs[0], mArgs[1], mArgs[2], mArgs[3]);
                    }
                    break;
                case MyTask.CheckUpdate:
                    showNew = false;
                    if (mArgs != null && mArgs.Length >= 1)
                    {
                        showNew = mArgs[0] == bool.TrueString;
                    }
                    OnCheckUpdate(showNew);
                    break;
                case MyTask.CutImages:
                    if (mArgs != null && mArgs.Length >= 2)
                    {
                        replace = true;
                        if (mArgs.Length >= 2)
                        {
                            if (mArgs[1] == bool.FalseString)
                            {
                                replace = false;
                            }
                        }
                        CutImages(mArgs[0], replace);
                    }
                    break;
                case MyTask.SaveAsMSE:
                    if (mArgs != null && mArgs.Length >= 2)
                    {
                        replace = false;
                        if (mArgs.Length >= 2)
                        {
                            if (mArgs[1] == bool.TrueString)
                            {
                                replace = true;
                            }
                        }
                        SaveMSEs(mArgs[0], cardlist, replace);
                    }
                    break;
                case MyTask.ReadMSE:
                    if (mArgs != null && mArgs.Length >= 2)
                    {
                        replace = false;
                        if (mArgs.Length >= 2)
                        {
                            if (mArgs[1] == bool.TrueString)
                            {
                                replace = true;
                            }
                        }
                        cardlist = ReadMSE(mArgs[0], replace);
                    }
                    break;
                case MyTask.ConvertImages:
                    if (mArgs != null && mArgs.Length >= 2)
                    {
                        replace = true;
                        if (mArgs.Length >= 3)
                        {
                            if (mArgs[2] == bool.FalseString)
                            {
                                replace = false;
                            }
                        }
                        ConvertImages(mArgs[0], mArgs[1], replace);
                    }
                    break;
            }
            isRun = false;
            lastTask = nowTask;
            nowTask = MyTask.NONE;
            if (lastTask != MyTask.ReadMSE)
            {
                cardlist = null;
            }

            mArgs = null;
        }
        #endregion
    }

}
