using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextureToPointsEditor : UnityEngine.Object{

    public static TextureToPoints data;


    public static List<Vector2> getOutline(Sprite spriteIn)
    {
        string path = AssetDatabase.GetAssetPath(spriteIn.texture);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
        importer.isReadable = true;
        AssetDatabase.ImportAsset(path);

        Texture2D forOutline = new Texture2D((int)spriteIn.textureRect.width, (int)spriteIn.textureRect.height);
        
        Color[] colors = spriteIn.texture.GetPixels();
        Color[] outColors = getSpriteColors(colors,spriteIn.texture.width, spriteIn.textureRect);
        forOutline.SetPixels(outColors);
        forOutline.alphaIsTransparency = true;
        forOutline.Apply();

        OutlineMaker maker = new OutlineMaker(forOutline);
        maker.setMaker(data.alphaThresh, data.distThresh);

        float xOffset = (data.centerX + 1) * (maker.getMax(0).x / 2.0f);
        float yOffset = (data.centerY + 1) * (maker.getMax(0).y / 2.0f);

        List<Vector2> ret = new List<Vector2>(maker.getLoopVertices(0));

        for(int i =0;i < ret.Count;i++)
        {
            Vector2 val = new Vector2();
            val.x = (ret[i].x - xOffset) * data.pixelScale;
            val.y = (ret[i].y - yOffset) * data.pixelScale;
            ret[i] = val;
        }


        importer.isReadable = false;
        AssetDatabase.ImportAsset(path);
        return ret;
    }

    public static Color[] getSpriteColors(Color[] colorsIn,float originWidth, Rect texRect)
    {
        int width = (int)texRect.width;
        int height = (int)texRect.height;
        int offsetX = (int)texRect.x;
        int offsetY = (int)texRect.y;
        int mainWidth = (int)originWidth;
        Color[] ret = new Color[width * height];
        for (int i = 0; i < width; i++)
        {
            int xPos = (offsetX + i);
            for (int j = 0; j < height; j++)
            {
                int pos = (offsetY + j) * mainWidth + xPos;
                ret[width * j + i] = colorsIn[pos];
            }
        }
        return ret;
    }

}
