using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpScript : MonoBehaviour
{
    public GameObject wholeComponent;
    private RectTransform rectComponent;
    public Image imageComp;
    public Ghost_Movement player;
    public GameObject keyObjectToDespawn;
    public float speed = 200f;
    public Text text;
    public Text textNormal;
    private Vector3 init_size;
    public GameObject disableMerlin;
    float a = 0;

    public Image imageOfKey;

    int hintCount = 0;

    // Use this for initialization
    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
        float a = 0;
        text.text = a + "%";
        init_size = wholeComponent.transform.localScale;

        keyObjectToDespawn.active = true;
        disableMerlin.active = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.amLooting == false)
        {
            imageComp.fillAmount = 0.0f;
            Debug.Log("no looting");
        }
        else
        {
            Debug.Log("i am looting");
        }

        if (Input.GetKey(KeyCode.G))
        {
            if (player.mustLootRefresh == true)
            {
                player.mustLootRefresh = false;
                imageComp.fillAmount = 0;
            }

            imageComp.fillAmount = imageComp.fillAmount + Time.deltaTime * speed;
            a = (int)(imageComp.fillAmount * 100);
            if (a > 0 && a <= 33)
            {
                textNormal.text = "Picking up...";
            }
            else if (a > 33 && a <= 67)
            {
                textNormal.text = "Loading...";
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
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            int a = 0;
            text.text = a + "%";
            imageComp.fillAmount = 0.0f;
        }

        if (imageComp.fillAmount == 1)
        {
            // reset
            imageComp.fillAmount = 0.0f;

            // open player doors
            player.canGoThroughGate1 = true;
            player.canGoThroughGate2 = true;

            // despawn key object
            keyObjectToDespawn.gameObject.SetActive(false);

            // disable force zone
            disableMerlin.gameObject.SetActive(false);

            // activate portals
            player.portal1.gameObject.SetActive(true);
            player.portal2.gameObject.SetActive(true);
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
