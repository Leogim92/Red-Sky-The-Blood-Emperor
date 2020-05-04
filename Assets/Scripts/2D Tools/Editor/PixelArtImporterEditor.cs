using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PixelArtImporterEditor : EditorWindow {

    private int pixelsPerUnit = 100;
    string folderPath = "";

    [MenuItem ("Tools/Pixel Art Importer")]
    private static void ShowWindow () {
        var window = GetWindow<PixelArtImporterEditor> ();
        window.titleContent = new GUIContent ("Pixel Art Importer");
        window.Show ();
    }

    private void OnGUI () {
        GUILayout.Label ("Pixels Per Unit", EditorStyles.boldLabel);
        pixelsPerUnit = EditorGUILayout.IntField (pixelsPerUnit);

        EditorGUILayout.BeginHorizontal ();
        
        EditorGUILayout.TextField (folderPath.Replace (Application.dataPath, "")); //Apagar o caminho até à pasta "Assets".
        
        if (GUILayout.Button ("Browse")) {
            folderPath = EditorUtility.OpenFolderPanel ("Select Pixel Art Folder", "Assets/", ""); //Para facilitar a vida do utilizador, vamos criar um butao de seleccao da pasta
        }

        EditorGUILayout.EndHorizontal ();

        if (GUILayout.Button ("Apply Pixel Art Settings")) {
            SetPixelArtSettings ();
        }
    }
    
    private void SetPixelArtSettings () {
        string[] files = Directory.GetFiles (folderPath, "*", SearchOption.AllDirectories);
        foreach (string file in files) {
            if (file.EndsWith (".png")) {

                string unityTexturePath = file.Substring (file.IndexOf ("Assets")).Replace ("\\", "/");
                TextureImporter ti = AssetImporter.GetAtPath (unityTexturePath) as TextureImporter;
                ti.spritePixelsPerUnit = pixelsPerUnit;
                ti.mipmapEnabled = false;
                ti.wrapMode = TextureWrapMode.Clamp;
                ti.filterMode = FilterMode.Point;
                ti.maxTextureSize = 4096;
                ti.textureCompression = TextureImporterCompression.Uncompressed;
                EditorUtility.SetDirty (ti);
                ti.SaveAndReimport ();
            }
        }
    }
}