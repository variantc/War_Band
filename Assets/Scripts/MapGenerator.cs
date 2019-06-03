using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public Sprite[] grassSprites;
    public static int height = 20;
    public static int width = 20;
    public Tile tilePrefab;

    private void Start()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile t = Instantiate(tilePrefab, this.transform);
                t.SetupTile(i, j, grassSprites[Random.Range(0, grassSprites.Length)]);
            }
        }
    }
}
