using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }
    public void Quit(){
        Application.Quit();
    }
    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }
}
