using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnRaycast : MonoBehaviour
{
    private GameObject lastMainParent;
    public GameObject sourceObject;
    GameObject FindPrefabParent(GameObject child)
    {
        Transform currentParent = child.transform;
        // Keep traversing up the hierarchy until the parent is null
        while (currentParent.parent != null)
        {
            currentParent = currentParent.parent;
            // Check if the current parent has the specified tag
            if (currentParent.CompareTag("SimObjPhysics"))
            {
                // Stop the search and return the current parent
                return currentParent.gameObject;
            }
        }
        // No prefab parent found
        return null;
    }

    void Update()
    {
        // Vector3 sourcePosition = sourceObject.main.transform.position;
        // Vector3 sourceForward = sourceObject.main.transform.forward;
        Vector3 sourcePosition = sourceObject.transform.position;
        Vector3 sourceForward = sourceObject.transform.forward;

        // Cast a ray from the direction the camera faces
        Ray ray = new Ray(sourcePosition, sourceForward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hit an object
            Debug.Log(hit.collider.gameObject.name);
            GameObject mainParent = FindPrefabParent(hit.collider.gameObject);
            if (mainParent == null)
            {
                // Debug.Log("Main parent not found");
            }
            else
            {
                Debug.Log("Found parent: " + mainParent.name);
                Highlight highlightComponent = mainParent.GetComponent<Highlight>();
                if (highlightComponent != null)
                {
                    // If the last hit object is not the current hit object
                    if (lastMainParent != null && lastMainParent != mainParent)
                    {
                        // Return the color of the last hit object to white
                        Highlight lastHighlightComponent = lastMainParent.GetComponent<Highlight>();
                        if (lastHighlightComponent != null)
                        {
                            Debug.Log("De-highlighting object: " + lastMainParent.name);
                            lastHighlightComponent.ToggleHighlight(false);
                        }
                    }
                    // Change the color of the hit object
                    highlightComponent.ToggleHighlight(true);
                    // Update the last hit object
                    lastMainParent = mainParent;

                }
                else
                {
                    Debug.Log("Highlight component not found on " + mainParent.name);
                }
            }
        }
        else
        {
            // If the ray does not hit any object, de-highlight the last hit object
            if (lastMainParent != null)
            {
                Highlight lastHighlightComponent = lastMainParent.GetComponent<Highlight>();
                if (lastHighlightComponent != null)
                {
                    Debug.Log("De-highlighting object: " + lastMainParent.name);
                    lastHighlightComponent.ToggleHighlight(false);
                }
                lastMainParent = null;
            }
        }
    }
}
