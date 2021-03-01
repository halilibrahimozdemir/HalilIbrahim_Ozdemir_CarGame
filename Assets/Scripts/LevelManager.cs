using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Texture2D map;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            // The pixel is transparent. Then ignore it.
            return;
        }


    }
}
