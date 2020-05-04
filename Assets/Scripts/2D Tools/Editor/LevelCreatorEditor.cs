using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
    private LevelCreator lc;
    private Texture2D prevTexture;

    private void OnEnable()
    {
        lc = (LevelCreator)target;
        prevTexture = null;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Level Creator Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space(10f);

        lc.LevelName = EditorGUILayout.TextField("Level Name", lc.LevelName);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level Texture", GUILayout.Width(120f));
        lc.LevelTexture = (Texture2D)EditorGUILayout.ObjectField(lc.LevelTexture, typeof(Texture2D), false);
        EditorGUILayout.EndHorizontal();

        EditorGUI.DrawTextureTransparent(new Rect(30, 100, 64*4, 32*4), lc.LevelTexture);
        EditorGUILayout.Space(140f);

        if (lc.LevelTexture != prevTexture)
        {
            string texturePath = AssetDatabase.GetAssetPath(lc.LevelTexture);
            TextureImporter textImp = (TextureImporter)TextureImporter.GetAtPath(texturePath);
            textImp.filterMode = FilterMode.Point;
            textImp.textureCompression = TextureImporterCompression.Uncompressed;
            textImp.isReadable = true;
            textImp.SaveAndReimport();

            List<ColorMap> tempList = new List<ColorMap>();
            Color[] allPixels = lc.LevelTexture.GetPixels();

            int width = lc.LevelTexture.width;
            int height = lc.LevelTexture.height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //para todos os pixeis que tenham alpha != 0 e ainda não existam na nossa lista.
                    //adicionamos à lista
                    Color tempColor = allPixels[y * width + x];
                    if (tempColor.a != 0 && !tempList.Any(cm => cm.Color == tempColor))
                    {
                        tempList.Add(new ColorMap(tempColor));
                    }
                }
            }

            lc.LevelName = lc.LevelTexture.name;
            lc.ColorMapping = tempList.ToArray();
            prevTexture = lc.LevelTexture;
        }

        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Color to Prefab Mapping", EditorStyles.boldLabel);
        EditorGUILayout.Space(5f);

        for (int i = lc.ColorMapping.Length - 1; i >= 0; i--)
        {
            EditorGUILayout.BeginHorizontal();

            lc.ColorMapping[i].Color = EditorGUILayout.ColorField(lc.ColorMapping[i].Color, GUILayout.Width(40f));
            lc.ColorMapping[i].Prefab = (GameObject)EditorGUILayout.ObjectField(lc.ColorMapping[i].Prefab, typeof(GameObject), false);

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(10f);

        if (GUILayout.Button("Delete Level"))
        {
            lc.ResetLevel();
        }

        if (GUILayout.Button("Create Level"))
        {
            lc.LoadLevel();
        }

        EditorUtility.SetDirty(lc);
    }
}
