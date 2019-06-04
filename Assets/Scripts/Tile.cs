using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Refs refs;
    private SpriteRenderer sr;
    int xPos;
    int yPos;

    // store a unit which arrives at this tile - TODO: make private
    [SerializeField]
    Unit unitAtTile;

    private void Start()
    {
        refs = FindObjectOfType<Refs>();
        GetUnit();
    }

    public void SetupTile(int x, int y, Sprite spr)
    {
        sr = GetComponent<SpriteRenderer>();
        xPos = x;
        yPos = y;
        sr.sprite = spr;
        this.transform.position = new Vector3(xPos, yPos, 0f);
        this.transform.name = "Tile" + xPos + "," + yPos;
    }

    void GetUnit()
    {
        foreach (Unit u in refs.unitController.units)
        {
            if (u.transform.position == this.transform.position)
            {
                unitAtTile = u;
            }
        }
    }

    public Vector2 GetTileCoords()
    {
        return new Vector2(xPos, yPos);
    }

    //// add the unit u to this tile object
    //public void UnitToTile (Unit u)
    //{
    //    if (unitAtTile == null)
    //    {
    //        unitAtTile = u;
    //    }
    //    else
    //    {
    //        Debug.LogError("Tile.UnitToTile() : Unit already at tile: " + this.transform.name);
    //    }
    //}

    //// remove the unit from this tile
    //public Unit UnitFromTile ()
    //{
    //    if (unitAtTile == null)
    //    {
    //        Debug.LogError("Tile.UnitFromTile() : No unit to remove from tile: " + this.transform.name);
    //        return null;
    //    }
    //    else
    //    {
    //        Unit u = unitAtTile;
    //        unitAtTile = null;
    //        return u;
    //    }
    //}

    // query (get) the unit at this tile
    public Unit UnitAtTile()
    {
        if (unitAtTile == null)
        {
            //Debug.LogError("Tile.UnitFromTile() : No unit at tile: " + this.transform.name);
            return null;
        }
        else
        {
            return unitAtTile;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        unitAtTile = collision.gameObject.GetComponent<Unit>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        unitAtTile = null;
    }
}
