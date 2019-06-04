using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Refs refs;

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

    public Tile GetTileAt(Vector3 tilePos)
    {
        Tile foundTile = null;

        foreach (Tile t in GetComponentsInChildren<Tile>())
        {
            if (t.transform.position == tilePos)
            {
                foundTile = t;
            }
        }

        Debug.LogError("MapController.GetTileAt : No tile found at : " + tilePos);
        return foundTile;
    }
}
