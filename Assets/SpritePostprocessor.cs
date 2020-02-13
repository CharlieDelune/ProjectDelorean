// using UnityEngine;
// using UnityEditor;
// using System.Collections.Generic;
// using UnityEditorInternal;
// using System.IO;
 
// // https://forum.unity.com/threads/sprite-editor-automatic-slicing-by-script.320776/
// // http://www.sarpersoher.com/a-custom-asset-importer-for-unity/
// public class SpritePostprocessor : AssetPostprocessor {
//     private bool use = true;
 
//     /// <summary>
//     /// Default all textures to 2D sprites, pivot at bottom, mip-mapped, uncompressed.
//     /// </summary>
//     private void OnPreprocessTexture() {
//         if(use){
//             Debug.Log("OnPreprocessTexture overwriting defaults");
    
//             TextureImporter importer = assetImporter as TextureImporter;
//             importer.textureType = TextureImporterType.Sprite;
    
//             importer.spriteImportMode = SpriteImportMode.Multiple;
    
//             importer.mipmapEnabled = true;
//             importer.filterMode = FilterMode.Point;
//             importer.textureCompression = TextureImporterCompression.Uncompressed;
//         }
//     }
 
//     public void OnPostprocessTexture(Texture2D texture) {
//         if(use){
//             TextureImporter importer = assetImporter as TextureImporter;
//             if (importer.spriteImportMode != SpriteImportMode.Multiple) {
//                 return;
//             }
    
//             Debug.Log("OnPostprocessTexture generating sprites");
    
//             int minimumSpriteSize = 16;
//             int extrudeSize = 0;
//             Rect[] rects = InternalSpriteUtility.GenerateAutomaticSpriteRectangles(texture, minimumSpriteSize, extrudeSize);
//             List<Rect> rectsList = new List<Rect>(rects);
//             rectsList = SortRects(rectsList, texture.width);
    
//             List<SpriteMetaData> metas = new List<SpriteMetaData>();
//             int rectNum = 0;
    
//             foreach (Rect rect in rectsList) {
//                 SpriteMetaData meta = new SpriteMetaData();
//                 meta.rect = rect;
//                 meta.name = rectNum++ + "";
//                 metas.Add(meta);
//             }
//             SpriteMetaData meta2 = new SpriteMetaData();
//             meta2.rect = new Rect(19,666,26,26);
//             meta2.name = "head";
//             metas.Add(meta2);
//             importer.spritesheet = metas.ToArray();
//         }
//     }
 
//     public void OnPostprocessSprites(Texture2D texture, Sprite[] sprites) {
//         Debug.Log("OnPostprocessSprites sprites: " + sprites.Length);
//     }
 
//     private List<Rect> SortRects(List<Rect> rects, float textureWidth) {
//         List<Rect> list = new List<Rect>();
//         while (rects.Count > 0) {
//             Rect rect = rects[rects.Count - 1];
//             Rect sweepRect = new Rect(0f, rect.yMin, textureWidth, rect.height);
//             List<Rect> list2 = this.RectSweep(rects, sweepRect);
//             if (list2.Count <= 0) {
//                 list.AddRange(rects);
//                 break;
//             }
//             list.AddRange(list2);
//         }
//         return list;
//     }
 
//     private List<Rect> RectSweep(List<Rect> rects, Rect sweepRect) {
//         List<Rect> result;
//         if (rects == null || rects.Count == 0) {
//             result = new List<Rect>();
//         } else {
//             List<Rect> list = new List<Rect>();
//             foreach (Rect current in rects) {
//                 if (current.Overlaps(sweepRect)) {
//                     list.Add(current);
//                 }
//             }
//             foreach (Rect current2 in list) {
//                 rects.Remove(current2);
//             }
//             list.Sort((Rect a, Rect b) => a.x.CompareTo(b.x));
//             result = list;
//         }
//         return result;
//     }
// }
 