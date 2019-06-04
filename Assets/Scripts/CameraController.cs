using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    float zoomSpeed = 20f;
    float panSpeed = 5f;

	// Use this for initialization
	void Start () {
        // Set camera to centre of map tiles
        this.transform.position = new Vector3(MapController.width / 2 + 0.5f,
                                                MapController.height / 2 - 0.5f,
                                                -10f);
	}

    private void Update()
    {
        // Panning:
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += Vector3.up * Time.deltaTime * panSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += Vector3.left * Time.deltaTime * panSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += Vector3.down * Time.deltaTime * panSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += Vector3.right * Time.deltaTime * panSpeed;
        }

        // Zooming
        if (Input.mouseScrollDelta.y != 0)
        {
            Camera.main.orthographicSize += -Input.mouseScrollDelta.normalized.y * Time.deltaTime * zoomSpeed;
        }
    }
}
