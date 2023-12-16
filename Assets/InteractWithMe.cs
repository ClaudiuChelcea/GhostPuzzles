using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithMe : MonoBehaviour
{
    public GameObject text;
    public Image imageComp;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Debug.Log("You are interacting with me daddy!");
            text.gameObject.active = true;
            imageComp.fillAmount = 0.0f;
        }       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("You no longer are interacting with me daddy!");
            text.gameObject.active = false;
            imageComp.fillAmount = 0.0f;
        }
    }
}
