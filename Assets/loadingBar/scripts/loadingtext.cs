using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class loadingtext : MonoBehaviour {
    public GameObject wholeComponent;
    private RectTransform rectComponent;
    public Image imageComp;
    public Ghost_Movement player;
    public GameObject hint;
    public TextMeshProUGUI texthint;
    public TextMeshProUGUI hintDurationTime; 
    public float hintDuration = 5.0f;
    private float initialHintDuration;

    public float speed = 200f;
    public Text text;
    public Text textNormal;
    private Vector3 init_size;
    float a = 0;

    public Ghost_ManageUI manager;
    public Vector3[] hintPrice;

    int hintCount = 0;

    // Use this for initialization
    void Start () {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
        float a = 0;
        text.text = a + "%";
        init_size = wholeComponent.transform.localScale;
        initialHintDuration = hintDuration;
    }
	
	// Update is called once per frame
	void Update () {

        if(player.amGettingHinted == false)
        {
            imageComp.fillAmount = 0.0f;
        }

        if(Input.GetKey(KeyCode.G))
        {
            if(player.mustRefresh == true)
            {
                player.mustRefresh = false;
                imageComp.fillAmount = 0;
            }

            imageComp.fillAmount = imageComp.fillAmount + Time.deltaTime * speed;
            a = (int)(imageComp.fillAmount * 100);
            if (a > 0 && a <= 33)
            {
                textNormal.text = "Loading...";
            }
            else if (a > 33 && a <= 67)
            {
                textNormal.text = "Getting hint...";
            }
            else if (a > 67 && a <= 100)
            {
                textNormal.text = "Please wait...";
            }
            else
            {
                Debug.Log("max");
            }
            text.text = a + "%";
        } else if(Input.GetKeyUp(KeyCode.G))
        {
            int a = 0;
            text.text = a + "%";
            imageComp.fillAmount = 0.0f;
        }

        if(imageComp.fillAmount == 1)
        {
            // remove currency

            // reset
            imageComp.fillAmount = 0.0f;

            hint.SetActive(true);

            hintDuration = initialHintDuration;

            // give hint
            hintCount++;
            if(hintCount == 1)
            {
                texthint.text = "You unlock doors with it! This costed you: " + hintPrice[0].x;
                manager.coinsAmount -= hintPrice[0].x;
            } else if (hintCount == 2)
            {
                texthint.text = "It's one with the nature! This costed you: " + hintPrice[0].y;
                manager.coinsAmount -= hintPrice[0].y;
            } else if (hintCount == 3)
            {
                texthint.text = "It's under a fucking plant! This costed you: " + hintPrice[0].z;
                manager.coinsAmount -= hintPrice[0].z;
            }
            else
            {
                texthint.text = "No more hints for you!";
            }
        }

        if (hintDuration > 0.0f)
        {
            Debug.Log("Hint active!");
            float timer = hintDuration - Time.deltaTime;

            hintDuration = Mathf.Max(timer, 0.0f);

            hintDurationTime.text = hintDuration.ToString("#.##");

            if (hintDuration == 0)
            {
                hint.SetActive(false);
            }
        }
    }

    private void Hide(GameObject gm)
    {
        // Hide button
        gm.transform.localScale = new Vector3(0, 0, 0);
        imageComp.fillAmount = 0.0f;
    }

    private void Show(GameObject gm)
    {
        // Hide button
        gm.transform.localScale = init_size;
    }
}
