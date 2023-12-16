using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPlayerUI : MonoBehaviour
{
    [SerializeField] public Vector3 offsetSlider;
    [SerializeField] public Vector3 offsethinting;
    [SerializeField] public Rigidbody player;
    public GameObject hinting;
    public GameObject looting;

    private Slider myPlayerSlider;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.position + offsetSlider;

        if (myPlayerSlider.transform.position != pos)
        {
            myPlayerSlider.transform.position = pos;
        }

        Vector3 pos2 = player.position + offsethinting;

        if (hinting.transform.position != pos2)
        {
            hinting.transform.position = pos2;
        }

        Vector3 pos3 = player.position + offsethinting;

        if (looting.transform.position != pos3)
        {
            looting.transform.position = pos3;
        }
    }
}
