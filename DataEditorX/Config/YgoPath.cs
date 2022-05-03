using System.IO;

namespace DataEditorX.Config
{
    public class YgoPath
    {
        public YgoPath(string gamepath)
        {
            this.SetPath(gamepath);
        }
        public void SetPath(string gamepath)
        {
            this.gamepath = gamepath;
            this.picpath = MyPath.Combine(gamepath, "pics");
            this.fieldpath = MyPath.Combine(this.picpath, "field");
            this.picpath2 = MyPath.Combine(this.picpath, "thumbnail");
            this.luapath = MyPath.Combine(gamepath, "script");
            this.ydkpath = MyPath.Combine(gamepath, "deck");
            this.replaypath = MyPath.Combine(gamepath, "replay");
        }
        /// <summary>游戏目录</summary>
        public string gamepath;
        /// <summary>大图目录</summary>
        public string picpath;
        /// <summary>小图目录</summary>
        public string picpath2;
        /// <summary>场地图目录</summary>
        public string fieldpath;
        /// <summary>脚本目录</summary>
        public string luapath;
        /// <summary>卡组目录</summary>
        public string ydkpath;
        /// <summary>录像目录</summary>
        public string replaypath;

        public string GetImage(long id)
        {
            return this.GetImage(id.ToString());
        }
        //public string GetImageThum(long id)
        //{
        //	return GetImageThum(id.ToString());
        //}
        public string GetImageField(long id)
        {
            return this.GetImageField(id.ToString());//场地图
        }
        public string GetScript(long id)
        {
            return this.GetScript(id.ToString());
        }
        public string GetYdk(string name)
        {
            return MyPath.Combine(this.ydkpath, name + ".ydk");
        }
        //字符串id
        public string GetImage(string id)
        {
            string img = MyPath.Combine(this.picpath, id + ".png");
            if (!File.Exists(img)) img = MyPath.Combine(this.picpath, id + ".jpg");
            return img;
        }
        //public string GetImageThum(string id)
        //{
        //	return MyPath.Combine(picpath2, id + ".jpg");
        //}
        public string GetImageField(string id)
        {
            return MyPath.Combine(this.fieldpath, id + ".png");//场地图
        }
        public string GetScript(string id)
        {
            return MyPath.Combine(this.luapath, "c" + id + ".lua");
        }
        public string GetModuleScript(string modulescript)
        {
            return MyPath.Combine(this.luapath, modulescript + ".lua");
        }

        public string[] GetCardfiles(long id)
        {
            string[] files = new string[]{
                this.GetImage(id),//大图
				//GetImageThum(id),//小图
				this.GetImageField(id),//场地图
				this.GetScript(id)
           };
            return files;
        }
        public string[] GetCardfiles(string id)
        {
            string[] files = new string[]{
                this.GetImage(id),//大图
				//GetImageThum(id),//小图
				this.GetImageField(id),//场地图
				this.GetScript(id)
           };
            return files;
        }
    }
}
