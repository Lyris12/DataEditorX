/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-13
 * 时间: 9:02
 * 
 */
using DataEditorX.Common;

namespace DataEditorX.Config
{
    /// <summary>
    /// 裁剪图片是、配置
    /// </summary>
	public class ImageSet
    {
        public ImageSet()
        {
            Init();
        }
        //初始化
        void Init()
        {
            normalArea = DEXConfig.ReadArea(DEXConfig.TAG_IMAGE_OTHER);

            xyzArea = DEXConfig.ReadArea(DEXConfig.TAG_IMAGE_XYZ);

            pendulumArea = DEXConfig.ReadArea(DEXConfig.TAG_IMAGE_PENDULUM);

            int[] ints = DEXConfig.ReadIntegers(DEXConfig.TAG_IMAGE_SIZE, 4);

            w = ints[0];
            h = ints[1];
            W = ints[2];
            H = ints[3];

            quality = DEXConfig.ReadInteger(DEXConfig.TAG_IMAGE_QUALITY, 95);
        }
        /// <summary>
        /// jpeg质量
        /// </summary>
		public int quality;
        /// <summary>
        /// 小图的宽
        /// </summary>
        public int w;
        /// <summary>
        /// 小图的高
        /// </summary>
        public int h;
        /// <summary>
        /// 大图的宽
        /// </summary>
        public int W;
        /// <summary>
        /// 大图的高
        /// </summary>
        public int H;
        /// <summary>
        /// 怪兽的中间图
        /// </summary>
        public Area normalArea;
        /// <summary>
        /// xyz怪兽的中间图
        /// </summary>
        public Area xyzArea;
        /// <summary>
        /// p怪的中间图
        /// </summary>
        public Area pendulumArea;
    }
}
