using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public Refs refs;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // TODO, not selecting now
            Debug.Log("Click");
            RaycastHit2D hit = DoRaycastAtMouse();
            Debug.Log("hit " + hit.transform.gameObject.name);

            if (hit.transform.tag == "Unit")
            {
                Debug.Log("Unit");
                hit.transform.gameObject.GetComponent<Unit>().IsSelected = true;
                refs.unitController.SelectionMade();
            }
            else
            {
                refs.unitController.UnSelectUnits();
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
