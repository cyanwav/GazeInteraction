using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Highlight : MonoBehaviour
{
    private bool isHighlighted;
    private int highlightedObjNum = 0;
    private float startTime;
    private float endTime;
    private List<string> csvData;
    private int TotalObjNum = 4;
    public bool isTarget = false;
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private Color color = Color.white;
    private List<Material> materials;
    void Start()
    {
        csvData = new List<string>();
    }
    private void Awake()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(new List<Material>(renderer.materials));
        }
        color.a = 0.12f;
        if (isHighlighted)
        {
            ToggleHighlight(true);
        }
    }
    public void ToggleHighlight(bool val)
    {
        // if a target is highlighted, it will not be highlighted again. 
        if (val && !isHighlighted)
        {
            isHighlighted = true;
            if (isTarget)
            {
                float foundTime = Time.time;
                string dataEntry = $"{foundTime}";
                csvData.Add(dataEntry);
                highlightedObjNum++;
                if (highlightedObjNum == 0)
                {
                    startTime = foundTime;
                    Debug.Log("Start Recording: " + startTime);
                }
                else Debug.Log("Highlighted Targets: " + (highlightedObjNum - 1));
            }
            foreach (var material in materials)
            {
                color.a = 0.12f;
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);
            }
            if (highlightedObjNum == TotalObjNum)
            {
                endTime = Time.time;
                string dataEntry = $"{endTime}";
                csvData.Add(dataEntry);
                Debug.Log("End Recording: " + endTime);
                //record duration
                dataEntry = $"{endTime - startTime}";
                csvData.Add(dataEntry);
                Debug.Log("Duration: " + (endTime - startTime));
                highlightedObjNum = 0;
            }
        }
        else
        {
            if (!isTarget)
            {
                isHighlighted = false;
                foreach (var material in materials)
                {
                    material.DisableKeyword("_EMISSION");
                }
            }
        }
    }
    private void WriteCsvToFile(string filePath, List<string> data)
    {
        // Ensure the directory exists
        string directory = Path.GetDirectoryName(filePath);
        Debug.Log("Directory: " + directory);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        // Write data to CSV file
        File.WriteAllLines(filePath, data);
        Debug.Log("CSV file written: " + filePath);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Write  face and gaze CSV data to file when 'Esc' key is pressed
            string fileBase = "test";
            string outFilePathGaze = Path.Combine("", fileBase + "_Gaze.csv");
            WriteCsvToFile(outFilePathGaze, csvData);
            Debug.Log("CSV Gaze file written: " + outFilePathGaze);
        }
    }
}


