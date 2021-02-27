using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFinalScores : MonoBehaviour
{
    public static int finalScore;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    void Start(){
        DisplayScores();
    }
    public void DisplayScores(){
        score.text = finalScore.ToString();
        finalScore=0;
        highScore.text = PlayerPrefs.GetInt("highscore1").ToString();


    }
}
