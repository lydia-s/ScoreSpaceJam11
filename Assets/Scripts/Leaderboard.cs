using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI first;
    public TextMeshProUGUI second;
    public TextMeshProUGUI third;
    // Start is called before the first frame update
    void Start()
    {
        first.text = PlayerPrefs.GetInt("highscore1").ToString(); 
        second.text =PlayerPrefs.GetInt("highscore2").ToString(); 
        third.text = PlayerPrefs.GetInt("highscore3").ToString(); 
        
    }

    
}
