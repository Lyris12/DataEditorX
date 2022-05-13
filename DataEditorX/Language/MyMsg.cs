/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月20 星期二
 * 时间: 7:40
 * 
 */

namespace DataEditorX.Language
{
    /// <summary>
    /// 消息
    /// </summary>
    public static class MyMsg
    {
        static readonly string _info, _warning, _error, _question;
        static MyMsg()
        {
            _info = LanguageHelper.GetMsg(LMSG.titleInfo);
            _warning = LanguageHelper.GetMsg(LMSG.titleWarning);
            _error = LanguageHelper.GetMsg(LMSG.titleError);
            _question = LanguageHelper.GetMsg(LMSG.titleQuestion);
        }
        public static void Show(string strMsg)
        {
            _ = MessageBox.Show(strMsg, _info,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Warning(string strWarn)
        {
            _ = MessageBox.Show(strWarn, _warning,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Error(string strError)
        {
            _ = MessageBox.Show(strError, _error,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool Question(string strQues)
        {
            if (MessageBox.Show(strQues, _question,
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Question) == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Show(LMSG msg)
        {
            _ = MessageBox.Show(LanguageHelper.GetMsg(msg), _info,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Warning(LMSG msg)
        {
            _ = MessageBox.Show(LanguageHelper.GetMsg(msg), _warning,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Error(LMSG msg)
        {
            _ = MessageBox.Show(LanguageHelper.GetMsg(msg), _error,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool Question(LMSG msg)
        {
            return (MessageBox.Show(LanguageHelper.GetMsg(msg), _question,
                           MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) == DialogResult.OK);
        }
    }
}
