using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;

public class UnitController : MonoBehaviour {

    public List<Unit> units;
    public List<Unit> selectedUnits;
    
    void Start ()
    {
        // find all starting units
        foreach (Unit u in FindObjectsOfType<Unit>())
        {
            units.Add(u);
            Debug.Log("Adding " + u.name);
        }
	}

    public void SelectionMade()
    {
        selectedUnits.Clear();

        foreach (Unit u in units)
        {
            if (u.IsSelected == true)
            {
                selectedUnits.Add(u);
            }
        }
    }

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
        foreach (Unit u in selectedUnits)
        {
            u.IsSelected = false;
        }
        selectedUnits.Clear();
    }

    public void MoveUnits(Vector3 destination)
    {
        foreach (Unit u in selectedUnits)
        {
            u.SetNewDestination(destination);
        }
    }
}
