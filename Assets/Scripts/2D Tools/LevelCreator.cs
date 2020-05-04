using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorMap
{
    public Color Color;
    public GameObject Prefab;

    public ColorMap(Color c)
    {
        this.Color = c;
        this.Prefab = null;
    }
}

public class LevelCreator : MonoBehaviour
{
    public string LevelName;
    public Texture2D LevelTexture;
    public ColorMap[] ColorMapping;

    private Transform parentTransform;

    public void ResetLevel()
    {
        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(parentTransform.GetChild(i));
        }
    }

    public void LoadLevel()
    {
        parentTransform = new GameObject(LevelName).transform;

        Color[] allPixels = LevelTexture.GetPixels();

        int width = LevelTexture.width;
        int height = LevelTexture.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnPrefab(allPixels[y * width + x], x, y);
            }
        }
    }

    private void SpawnPrefab(Color color, int x, int y)
    {
        if (color.a == 0)
        {
            return;
        }

        foreach (ColorMap map in ColorMapping)
        {
            if (color == map.Color)
            {
                GameObject newObject = Instantiate(map.Prefab, new Vector3(x, y, 0), Quaternion.identity, parentTransform);
                return;
            }
        }

        Debug.LogError("Não existe um mapeamento para esta cor: " + color);
    }
}
