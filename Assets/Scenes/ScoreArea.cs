using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreArea : MonoBehaviour
{
    [SerializeField]
    private Text score;
    private DropletController player;

    private void Start()
    {
        player = GameObject.Find("Circle").GetComponent<DropletController>();
    }

    private void LateUpdate()
    {
        if (player.gameInProgress)
        {
            score.text = "Score: " + player.score;
        }
    }
}
