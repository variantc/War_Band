using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool IsSelected = false;
    bool HasDestination = false;
    private Vector3 destination;
    public float moveSpeed;

    void FixedUpdate()
    {
        if (HasDestination)
        {
            if ((destination - this.transform.position).sqrMagnitude >= 0.1f)
            {
                MoveToDestination();
            }
            else
            {
                this.transform.position = destination;
                Debug.Log("Arrived!");
                HasDestination = false;
            }
        }

        if (IsSelected)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    public void SetNewDestination (Vector3 dest)
    {
        destination = dest;
        HasDestination = true;
    }

    private void MoveToDestination ()
    {
        Vector3 heading = (destination - this.transform.position).normalized;
        this.transform.position += heading * Time.deltaTime * moveSpeed;
    }
    
}
