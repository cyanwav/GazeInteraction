using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighlightOnRaycast : MonoBehaviour
{
    // private GameObject lastMainParent;
    public GameObject sourceObject;
    private GameObject lastHitObject;
    public int highlightedObjNum;
    public float startTime;
    public float endTime;
    public List<string> csvData;
    public int TotalObjNum = 4;
    public bool isStarted = false;
    // GameObject FindPrefabParent(GameObject child)
    // {
    //     Transform currentParent = child.transform;
    //     // Keep traversing up the hierarchy until the parent is null
    //     while (currentParent.parent != null)
    //     {
    //         currentParent = currentParent.parent;
    //         // Check if the current parent has the specified tag
    //         if (currentParent.CompareTag("SimObjPhysics"))
    //         {
    //             // Stop the search and return the current parent
    //             return currentParent.gameObject;
    //         }
    //     }
    //     // No prefab parent found
    //     return null;
    // }
    void Start()
    {
        csvData = new List<string>();
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
            // Debug.Log(hit.collider.gameObject.name);
            // GameObject mainParent = FindPrefabParent(hit.collider.gameObject);
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            // if (mainParent == null)
            if (renderer == null)
            {
                // Debug.Log("Main parent not found");
            }
            else
            {
                // Debug.Log("Found parent: " + mainParent.name);
                // Highlight highlightComponent = mainParent.GetComponent<Highlight>();
                Highlight highlightComponent = hit.collider.gameObject.GetComponent<Highlight>();
                if (highlightComponent != null)
                {
                    // If the last hit object is not the current hit object
                    // if (lastMainParent != null && lastMainParent != mainParent)
                    if (lastHitObject != null && lastHitObject != hit.collider.gameObject)
                    {
                        // Return the color of the last hit object to white
                        // Highlight lastHighlightComponent = lastMainParent.GetComponent<Highlight>();
                        Renderer lastRenderer = lastHitObject.GetComponent<Renderer>();
                        if (lastRenderer != null)
                        {
                            lastRenderer.material.color = Color.white;
                            Highlight lastHighlightComponent = lastHitObject.GetComponent<Highlight>();
                            if (lastHighlightComponent != null)
                            {
                                // Debug.Log("De-highlighting object: " + lastMainParent.name);
                                // Debug.Log("De-highlighting object: " + lastHitObject.name);
                                lastHighlightComponent.ToggleHighlight(false);
                            }
                        }
                    }
                    // Change the color of the hit object
                    renderer.material.color = Color.black;
                    highlightComponent.ToggleHighlight(true);
                    // Update the last hit object
                    // lastMainParent = mainParent;
                    lastHitObject = hit.collider.gameObject;

                }
                // else
                // {
                //     Debug.Log("Highlight component not found on " + mainParent.name);
                // }
            }
        }
        else
        {
            // If the ray does not hit any object, de-highlight the last hit object
            // if (lastMainParent != null)
            // {
            //     Highlight lastHighlightComponent = lastMainParent.GetComponent<Highlight>();
            //     if (lastHighlightComponent != null)
            //     {
            //         Debug.Log("De-highlighting object: " + lastMainParent.name);
            //         lastHighlightComponent.ToggleHighlight(false);
            //     }
            //     lastMainParent = null;
            // }
            if (lastHitObject != null)
            {
                Renderer lastRenderer = lastHitObject.GetComponent<Renderer>();
                if (lastRenderer != null)
                {
                    lastRenderer.material.color = Color.white;
                    Highlight lastHighlightComponent = lastHitObject.GetComponent<Highlight>();
                    if (lastHighlightComponent != null)
                    {
                        // Debug.Log("De-highlighting object: " + lastHitObject.name);
                        lastHighlightComponent.ToggleHighlight(false);
                    }
                    lastHitObject = null;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Write  face and gaze CSV data to file when 'Esc' key is pressed
            string fileBase = "test";
            string currentDirectory = Directory.GetCurrentDirectory();
            string outFilePathGaze = Path.Combine(currentDirectory, fileBase + "_Gaze.csv");
            WriteCsvToFile(outFilePathGaze, csvData);
            Debug.Log("CSV Gaze file written: " + outFilePathGaze);
        }
    }
    private void WriteCsvToFile(string filePath, List<string> data)
    {
        // Ensure the directory exists
        string directory = Path.GetDirectoryName(filePath);
        Debug.Log("Directory: " + directory);
        // if (!Directory.Exists(directory))
        // {
        //     Directory.CreateDirectory(directory);
        // }
        // Write data to CSV file
        File.WriteAllLines(filePath, data);
        Debug.Log("CSV file written: " + filePath);
    }
}
