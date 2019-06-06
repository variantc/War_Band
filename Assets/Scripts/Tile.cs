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

    private void FixedUpdate()
    {
        if (unitAtTile != null)
            GetComponent<SpriteRenderer>().color = Color.blue;
        else
            GetComponent<SpriteRenderer>().color = Color.white;
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

    // add the unit u to this tile object
    public void UnitToTile(Unit u)
    {
        if (unitAtTile == null)
        {
            unitAtTile = u;
        }
        else
        {
            Debug.LogError("Tile.UnitToTile() : Unit already at tile: " + this.transform.name);
        }
    }

    public bool CheckForUnit()
    {
        // get the collider2D on this tile
        Collider2D col = GetComponent<Collider2D>();
        // make a temporary collider2D array to store any overlapping units
        Collider2D[] colliders = new Collider2D[1];
        // make a filter and give it the mask to look for "Unit"
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Unit");

        // get any overlapping colliders (caught by filter -- looking for "Unit" layer)
        int num = col.OverlapCollider(filter, colliders);

        // Check there is only one collider here
        if (num == 1)
        {
            unitAtTile = colliders[num - 1].gameObject.GetComponent<Unit>();
            return true;
        }
        if (num > 1)
        {
            unitAtTile = null;
            Debug.LogError("Multiple Units at tile");
            return false;
        }
        else
        {
            unitAtTile = null;
            return false;
        }
    }

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
    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // only update unitAtTile if don't already have unit here
    //    if (unitAtTile == null)
    //        unitAtTile = collision.gameObject.GetComponent<Unit>();
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<Unit>() == null)
    //        return;
    //    // check that the Unit leaving the Tile is the current unit - to prevent errors of a
    //    // passing Unit 'nullifying' the unitAtTile variable
    //    if (collision.gameObject.GetComponent<Unit>() != unitAtTile)
    //        return;
    //    else
    //        unitAtTile = null;
    //}
}
