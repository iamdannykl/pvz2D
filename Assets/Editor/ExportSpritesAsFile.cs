//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年4月23日 21:02:58
//------------------------------------------------------------
using UnityEngine;
using UnityEditor;
using System.IO;

namespace ETEditor
{
    public static class ExportSpritesAsFile
    {
        /// <summary>
        /// 注意，使用此编辑器拓展需要先选中图集文件
        /// </summary>
        [MenuItem("Tools/图集元素转文件工具")]
        [System.Obsolete]
        static void ProcessToSprite()
        {
            Texture2D image = Selection.activeObject as Texture2D;//获取选择的对象

            string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));//获取路径名称

            string path = rootPath + "/" + image.name + ".PNG";//图片路径名称

            TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口


            AssetDatabase.CreateFolder(rootPath, image.name);//创建文件夹


            foreach (SpriteMetaData metaData in texImp.spritesheet)//遍历小图集
            {
                Texture2D myimage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);

                for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)//Y轴像素
                {
                    for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                        myimage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, image.GetPixel(x, y));
                }


                if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)
                {
                    Texture2D newTexture = new Texture2D(myimage.width, myimage.height);
                    newTexture.SetPixels(myimage.GetPixels(0), 0);
                    myimage = newTexture;
                }
                var pngData = myimage.EncodeToPNG();

                File.WriteAllBytes(rootPath + "/" + image.name + "/" + metaData.name + ".PNG", pngData);
                // 刷新资源窗口界面
                AssetDatabase.Refresh();
            }
        }
    }

}
