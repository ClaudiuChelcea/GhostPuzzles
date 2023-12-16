using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishGame : MonoBehaviour
{
    public Ghost_Movement player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            // Meet player
            player.GameOver = true;
        }
    }
}
