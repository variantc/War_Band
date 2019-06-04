using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public Refs refs;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = DoRaycastAtMouse();

            if (hit.transform.gameObject == null)
            {
                Debug.Log("problem?");
                return;
            }

            if (hit.transform.tag == "Unit")
            {
                hit.transform.gameObject.GetComponent<Unit>().IsSelected = true;
                refs.unitController.SelectionMade();
                Debug.Log("unit tag");
            }
            else
            {
                if (hit.transform.tag == "Terrain")
                {
                    if (hit.transform.gameObject.GetComponent<Tile>().UnitAtTile() != null)
                    {
                        hit.transform.gameObject.GetComponent<Tile>().UnitAtTile().IsSelected = true;

                        refs.unitController.SelectionMade();
                    }
                    else
                    {
                        refs.unitController.UnSelectUnits();
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            refs.unitController.MoveUnits(DoRaycastAtMouse().transform.position);
        }
    }

    private RaycastHit2D DoRaycastAtMouse()
    {
        return Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    }
    


}
