using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ghost_ManageUI : MonoBehaviour
{
    [SerializeField] public GameObject getPlayer;
    [SerializeField] public TextMeshProUGUI Speed;
    [SerializeField] public Slider GhostingSlider;
    public GameObject sliderComponent;

    // Internal extracted components
    private Ghost_Movement getPlayerStats;

    // Time completion
    public TextMeshProUGUI gameCompletion;
    private float timeCompletionTime = 0.0f;

    // Coins
    public TextMeshProUGUI coins;
    public float coinsAmount;

    // Start is called before the first frame update
    void Start()
    {
        getPlayerStats = getPlayer.GetComponent<Ghost_Movement>();
        timeCompletionTime = 0.0f;
        coinsAmount = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        // Speed
        Speed.text = "Speed: " + getPlayerStats.moveSpeed;

        // Ghostin
        GhostingSlider.value = getPlayerStats.getGhostingStarterTime() - getPlayerStats.ghostingTimer;
        if(getPlayerStats.amInUnghostableZone == true)
        {
            Debug.Log("Can't ghost here!");
            getPlayerStats.collider.enabled = true;
            sliderComponent.SetActive(false);
        }  else
        {
            Debug.Log("Can ghost here!");
            sliderComponent.SetActive(true);
        }

        timeCompletionTime += Time.deltaTime;
        int minutes = (int)(timeCompletionTime / 60.0f);
        float seconds = timeCompletionTime % 60.0f;

        gameCompletion.text = string.Format("{0:D2}:{1:D2}", minutes, (int)seconds);

        coins.text = coinsAmount.ToString();
    }
}
