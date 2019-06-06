using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool IsSelected = false;     // flag for if unit selected
    bool HasDestination = false;        // flag for when unit has new destination
    private Vector3 destination;        // a destination location
    public float moveSpeed;             // how fast the unit moves
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        // Do we have a destination?
        if (HasDestination)
        {
            // if we're not at (close to) destination, MoveToDestination
            if ((destination - this.transform.position).sqrMagnitude >= 0.1f)
            {
                MoveToDestination();
            }
            // otherwise, we're close enough to move to destination exactly
            else
            {
                this.transform.position = destination;
                // set flag to false so won't try to move further
                HasDestination = false;
                WhereAmI();
            }
        }

        // change colour to indicate selected
        // TODO: - improve, only want to change colour when required
        if (IsSelected)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    // assigning new destination
    public void SetNewDestination(Vector3 dest)
    {
        destination = dest;
        HasDestination = true;
    }

    // move unit towards destination
    private void MoveToDestination()
    {
        Vector3 heading = (destination - this.transform.position).normalized;
        this.transform.position += heading * Time.deltaTime * moveSpeed;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("dsf");
    //}

    public Tile WhereAmI ()
    {
        Collider2D[] colliders = new Collider2D[1];
        //ContactFilter2D col = new ContactFilter2D();
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Terrain");

        int num = col.OverlapCollider(filter, colliders);

        Debug.Log(colliders[num-1]);
        return colliders[num-1].gameObject.GetComponent<Tile>();
    }
}
