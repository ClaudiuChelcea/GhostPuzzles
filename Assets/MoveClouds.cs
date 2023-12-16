using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour
{

    public float couldsSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            float newZ;
            // Needs reset
            if (child.transform.position.z < -300)
            {
                newZ = 556.3f;
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, newZ);
            } else
            {
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z - Time.deltaTime * couldsSpeed);
            }
            
        }
    }
}
