using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Highlight : MonoBehaviour
{
    private bool isHighlighted;
    // public bool isTarget = false;
    public GameObject cameraSource;


    // [SerializeField]
    // private List<Renderer> renderers;
    // [SerializeField]
    // private Color color = Color.white;
    // private List<Material> materials;

    private void Awake()
    {
        // materials = new List<Material>();
        // foreach (var renderer in renderers)
        // {
        //     materials.AddRange(new List<Material>(renderer.materials));
        // }
        // color.a = 0.12f;
        if (isHighlighted)
        {
            ToggleHighlight(true);
        }
    }
    public void ToggleHighlight(bool val)
    {
        HighlightOnRaycast rayScript = cameraSource.GetComponent<HighlightOnRaycast>();
        // if a target is highlighted, it will not be highlighted again. 
        if (val && !isHighlighted)
        {
            isHighlighted = true;
            // if (isTarget)
            // {
            float foundTime = Time.time;
            string dataEntry = $"{foundTime}";
            rayScript.csvData.Add(dataEntry);
            rayScript.highlightedObjNum++;
            if (!rayScript.isStarted)
            {
                rayScript.isStarted = true;
                rayScript.startTime = foundTime;
                Debug.Log("Start Recording: " + rayScript.startTime);
            }
            else Debug.Log("Highlighted Targets: " + (rayScript.highlightedObjNum - 1));
            // }
            // foreach (var material in materials)
            // {
            //     color.a = 0.12f;
            //     material.EnableKeyword("_EMISSION");
            //     material.SetColor("_EmissionColor", color);
            // }
            if (rayScript.highlightedObjNum == rayScript.TotalObjNum)
            {
                rayScript.endTime = Time.time;
                dataEntry = $"{rayScript.endTime}";
                rayScript.csvData.Add(dataEntry);
                Debug.Log("End Recording: " + rayScript.endTime);
                //record duration
                dataEntry = $"{rayScript.endTime - rayScript.startTime}";
                rayScript.csvData.Add(dataEntry);
                Debug.Log("Duration: " + (rayScript.endTime - rayScript.startTime));
                rayScript.highlightedObjNum = 0;
            }
        }
        // else
        // {
        //     if (!isTarget)
        //     {
        //         isHighlighted = false;
        //         // foreach (var material in materials)
        //         // {
        //         //     material.DisableKeyword("_EMISSION");
        //         // }
        //     }
        // }
    }
}


