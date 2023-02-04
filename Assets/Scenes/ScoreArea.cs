using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreArea : MonoBehaviour
{
    [SerializeField]
    private Text score;
    private int _score = 0;

    void LateUpdate()
    {
        _score += 1;
        score.text = "Score: " + _score; 
    }
}
