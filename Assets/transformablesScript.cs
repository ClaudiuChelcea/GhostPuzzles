using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformablesScript : MonoBehaviour
{
    public loadingtext text;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            // player
            text.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            // player
            text.enabled = false;
            text.imageComp.fillAmount = 0.0f;
        }
    }
}
