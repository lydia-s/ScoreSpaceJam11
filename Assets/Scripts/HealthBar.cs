using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthBar : MonoBehaviour
{
    public float startHealth;
    public static float health;
    public Slider healthBar;
    void Start(){
        startHealth=healthBar.value;
        health = healthBar.value;

    }
    public void SetHealth(){
        healthBar.value = health;
    }
    public void IncreaseHealth(){
        if(!(health>=startHealth)){
            health+=0.1f;

        }
    }
    public void CheckHealth(){
        if(health<=0){
            SceneManager.LoadScene("GameOver");
            int highScore1 =PlayerPrefs.GetInt("highscore1"); 
            int highScore2 =PlayerPrefs.GetInt("highscore2"); 
            int highScore3 =PlayerPrefs.GetInt("highscore3"); 
            if(highScore1<UpdateScore.score){
                PlayerPrefs.SetInt("highscore1", UpdateScore.score);

            }else if(highScore2<UpdateScore.score){
                PlayerPrefs.SetInt("highscore2", UpdateScore.score);

            }
            else if(highScore3<UpdateScore.score){
                PlayerPrefs.SetInt("highscore3", UpdateScore.score);

            }
            DisplayFinalScores.finalScore = UpdateScore.score;
            UpdateScore.score=0;

        }
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
        CheckHealth();
    }
}
