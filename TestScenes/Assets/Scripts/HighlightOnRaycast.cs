using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnRaycast : MonoBehaviour
{
    private GameObject lastHitObject;

    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;

        // Cast a ray from the direction the camera faces
        Ray ray = new Ray(cameraPosition, cameraForward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hit an object
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                // If the last hit object is not the current hit object
                if (lastHitObject != null && lastHitObject != hit.collider.gameObject)
                {
                    // Return the color of the last hit object to white
                    Renderer lastRenderer = lastHitObject.GetComponent<Renderer>();
                    if (lastRenderer != null)
                    {
                        lastRenderer.material.color = Color.white;
                    }
                }

                // Change the color of the hit object
                renderer.material.color = Color.black;

                // Update the last hit object 
                lastHitObject = hit.collider.gameObject;
            }
        }
        else
        {
            // If the ray does not hit any object, return the color of the last hit object to white
            if (lastHitObject != null)
            {
                Renderer lastRenderer = lastHitObject.GetComponent<Renderer>();
                if (lastRenderer != null)
                {
                    lastRenderer.material.color = Color.white;
                    lastHitObject = null;
                }
            }
        }
    }
}
