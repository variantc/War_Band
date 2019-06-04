using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;

public class UnitController : MonoBehaviour {

    public Refs refs;

    public List<Unit> units;
    public List<Unit> selectedUnits;

    public Unit unitPrefab;
    
    void Start ()
    {
        for (int i = 0; i < 10; i++)
        {
            Unit unit = Instantiate(unitPrefab, this.transform);
            unit.transform.position = new Vector3(Random.Range(5, 15), Random.Range(5, 15), 0f);
        }
        // find all starting units and add to 'units' list
        foreach (Unit u in FindObjectsOfType<Unit>())
        {
            units.Add(u);
            //Debug.Log("Adding " + u.name);
        }
	}

    public void SelectionMade()
    {
        // clear selectedUnits list
        selectedUnits.Clear();

        // add all units which have been designated as selected
        foreach (Unit u in units)
        {
            if (u.IsSelected == true)
            {
                selectedUnits.Add(u);
            }
        }
    }

    // TODO - employ for dragging selection
    public void SelectUnits (Vector3 corner1, Vector3 corner2)
    {
        float x1 = corner1.x;
        float x2 = corner2.x;
        float y1 = corner1.y;
        float y2 = corner2.y;

        if (x1 > x2)
        {
            float temp = x2;
            x2 = x1;
            x1 = temp;
        }
        if (y1 > y2)
        {
            float temp = y2;
            y2 = y1;
            y1 = temp;
        }

        foreach (Unit u in units)
        {
            float xPos = u.transform.position.x;
            float yPos = u.transform.position.y;

            if (xPos > x1 && xPos < x2 && yPos > y1 && yPos < y2)
                u.IsSelected = true;
        }
    }

    public void UnSelectUnits()
    {
        // go through selectedUnits and set selected flag to false and then clear
        // list
        foreach (Unit u in selectedUnits)
        {
            u.IsSelected = false;
        }
        selectedUnits.Clear();
    }

    public void MoveUnits(Vector3 destination)
    {
        // we need to distribute the positions
        // issue the unit's move command for each currently selected unit
        int i = 0;
        int j = 0;

        int loopCount = 0;

        // store the used positions so units can't overlay
        List<Vector2> usedPos = new List<Vector2>();

        foreach (Unit u in selectedUnits)
        {
            Vector3 unitDest = new Vector3(destination.x + i, destination.y + j, 0f);
            u.SetNewDestination(unitDest);

            //// Improve! - looping through all tiles and seeing if they're under the destination
            //foreach (Tile t in FindObjectsOfType<Tile>())
            //{
            //    if (t.transform.position == destination)
            //    {
            //        t.UnitToTile(u);
            //    }
            //}

            //refs.mapController.GetTileAt(destination).UnitToTile(u);
            
            usedPos.Add(new Vector2(i, j));

            while (usedPos.Contains(new Vector2(i, j)))
            {
                i += Random.Range(-1, 2);
                j += Random.Range(-1, 2);

                if (loopCount > 1000)
                {
                    Debug.LogError("UnitController.MoveUnits : Can't find space for all units");
                    break;
                }
                loopCount++;
            }
        }
    }
}
