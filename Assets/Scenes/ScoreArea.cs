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
        if (Time.timeScale == 1)
        {
            score.text = "Score: " + player.score;
        }
    }
}
