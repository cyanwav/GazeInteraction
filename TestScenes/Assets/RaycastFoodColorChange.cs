using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFoodColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    public float raycastDistance = 10f;
    void Start()
    {
       
}

    // Update is called once per frame
    void Update()
    {
        // check if there is a mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // cast a ray from the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // visualize the ray
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 0.5f);

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                // check if the object hit has MeshRenderer
                MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    // Change the color of the object hit to white
                    renderer.material.color = Color.white;
                }
            }
        }
    }
}
