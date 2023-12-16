using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class talkableClass : MonoBehaviour
{
    public string Message = "Temporary message!";
    public TextMeshProUGUI getTextToModify;
    public TextMeshProUGUI getTextToModifyPopUp;
    public GameObject getWholeTextPlane;
    public float timeOfPopUp;
    private float initialTimeOfPopUp;
    private bool ImShown = false;

    private void Start()
    {
        getTextToModifyPopUp.enabled = false;
        initialTimeOfPopUp = timeOfPopUp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            // show pop-up with right message
            getWholeTextPlane.active = true;
            getTextToModify.text = stringifyOurText(Message);

            // add a timer to it
            timeOfPopUp = initialTimeOfPopUp;

            // start reducing from the timer in the update function
            ImShown = true;

            // show the timer text
            getTextToModifyPopUp.enabled = true;
            getTextToModifyPopUp.text = "Text expires: " + Mathf.RoundToInt(timeOfPopUp);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            // close pop-up
            getWholeTextPlane.active = false;

            // close timer in UI
            getTextToModifyPopUp.enabled = false;

            // time of pop-up
            timeOfPopUp = initialTimeOfPopUp;

            // no longer reduce from the timer
            ImShown = false;
        }
    }

    private string stringifyOurText(string text)
    {
        return text.Replace("\\n", "\n");
    }

    private void Update()
    {
        // if the timer is shown
        if(ImShown == true)
        {
            // constantly reduce until you get to 0
            float timer = timeOfPopUp - Time.deltaTime;
            timeOfPopUp = Mathf.Max(timer, 0.0f);

            getTextToModifyPopUp.text = "Text expires: " + Mathf.RoundToInt(timeOfPopUp);

            // when you get to 0, it is no longer reduced fromit
            if (timeOfPopUp == 0.0f)
            {
                ImShown = false;

                // also the message goes down and the pop-up
                getTextToModifyPopUp.enabled = false;
                getWholeTextPlane.active = false;
            }
        }
    }
}