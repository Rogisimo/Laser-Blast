using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endScreen;
    public void PlayAgain(){
        Time.timeScale = 1;
        SceneManager.LoadScene("MainLevel");
    }

    public void GameOver(){
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }
}
