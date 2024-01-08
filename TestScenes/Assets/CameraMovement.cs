using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed = 3.0f;
    public float moveSpeed = 3.0f;
    public float zoomSpeed = 5.0f;
    public float minZoomDistance = 3.0f;
    public float maxZoomDistance = 15.0f;

    void Update()
    {
        // rotate
        if (Input.GetMouseButton(1))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.up, horizontalRotation, Space.World);
            transform.Rotate(Vector3.left, verticalRotation, Space.Self);
        }

        // shift
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoom = transform.forward * scroll * zoomSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, Vector3.zero);
        if ((distance > minZoomDistance || scroll > 0) && (distance < maxZoomDistance || scroll < 0))
        {
            transform.Translate(zoom, Space.World);
        }
    }
}
