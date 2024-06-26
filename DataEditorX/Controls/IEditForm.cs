﻿namespace DataEditorX.Controls
{
    public interface IEditForm
    {
        //获取打开的文件路径
        string GetOpenFile();
        //创建文件
        bool Create(string file);
        //打开文件
        bool Open(string file, string name);
        //是否能打开某个文件
        bool CanOpen(string file);
        //保存
        bool Save(bool shift = false);
        //设置为活动窗口
        void SetActived();
    }
}
