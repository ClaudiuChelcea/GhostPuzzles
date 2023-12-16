using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropener : MonoBehaviour
{
    public Animator doorAnim;
    public Material doorMaterial;
    public Ghost_Movement player;
    private BoxCollider collider;

    public int door_number = 1;

    private void Start()
    {
        doorMaterial.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (door_number == 1)
        {
            if (other.gameObject.layer == 8 && player.canGoThroughGate1 == true)
            {
                // player
                doorAnim.SetTrigger("open");
                Debug.Log("Colluded!");
            }
        } 
        else if (door_number == 2)
        {
            if (other.gameObject.layer == 8 && player.canGoThroughGate2 == true)
            {
                // player
                doorAnim.SetTrigger("open2");
                Debug.Log("Colluded!");
            }
        }
        else if (door_number == 3)
        {
            if (other.gameObject.layer == 8 && player.canGoThroughGate3 == true)
            {
                // player
                doorAnim.SetTrigger("open3");
                Debug.Log("Colluded!");
                collider.enabled = false;
            }
        }
        else if (door_number == 4)
        {
            if (other.gameObject.layer == 8 && player.canGoThroughGate4 == true)
            {
                // player
                doorAnim.SetTrigger("open4");
                Debug.Log("Colluded!");
                collider.enabled = false;
            }
        }
        else if (door_number == 5)
        {
            if (other.gameObject.layer == 8 && player.canGoThroughGate5 == true)
            {
                // player
                doorAnim.SetTrigger("open5");
                Debug.Log("Colluded!");
                collider.enabled = false;
            }
        }
        else if (door_number == 6)
        {
            if (other.gameObject.layer == 8 && player.canGoThroughGate6 == true)
            {
                // player
                doorAnim.SetTrigger("open6");
                Debug.Log("Colluded!");
                collider.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (door_number == 1)
        {
            if(player.canGoThroughGate1 == false)
            {
                doorMaterial.color = Color.red;
            } else
            {
                doorMaterial.color = Color.green;
            }
        } else if (door_number == 2)
        {
            if (player.canGoThroughGate2 == false)
            {
                doorMaterial.color = Color.red;
            }
            else
            {
                doorMaterial.color = Color.green;
            }
        }
        if (door_number == 3)
        {
            if (player.canGoThroughGate3 == false)
            {
                doorMaterial.color = Color.red;
            }
            else
            {
                doorMaterial.color = Color.green;
            }
        }
        if (door_number == 4)
        {
            if (player.canGoThroughGate4 == false)
            {
                doorMaterial.color = Color.red;
            }
            else
            {
                doorMaterial.color = Color.green;
            }
        }
        if (door_number == 5)
        {
            if (player.canGoThroughGate5 == false)
            {
                doorMaterial.color = Color.red;
            }
            else
            {
                doorMaterial.color = Color.green;
            }
        }
        if (door_number == 6)
        {
            if (player.canGoThroughGate6 == false)
            {
                doorMaterial.color = Color.red;
            }
            else
            {
                doorMaterial.color = Color.green;
            }
        }
    }
}
