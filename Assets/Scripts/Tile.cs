using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private SpriteRenderer sr;
    int xPos;
    int yPos;
	
    public void SetupTile(int x, int y, Sprite spr)
    {
        sr = GetComponent<SpriteRenderer>();
        xPos = x;
        yPos = y;
        sr.sprite = spr;
        this.transform.position = new Vector3(xPos, yPos, 0f);
        this.transform.name = "Tile" + xPos + "," + yPos;
    }

    public Vector2 GetTileCoords()
    {
        return new Vector2(xPos, yPos);
    }
}
