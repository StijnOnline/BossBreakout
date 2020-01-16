using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    private bool playing = false;
    public GameObject canvas;
    public GameObject ball;


    private float timer;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public void Update() {

        if (playing)
        {
            timer += Time.deltaTime;
        }

        scoreText.SetText("Time: " + (int)(timer) / 60 + ":" + ((int)(timer) % 60).ToString("00"));

        if (!playing && Input.anyKey) {
            Play();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
           
        }
    }


    void LoadHighscore()
    {
        int Time = PlayerPrefs.GetInt("Time");
        int bestTime = PlayerPrefs.GetInt("BestTime");
        if (Time < bestTime || bestTime == 0)
        {
            bestTime = Time;
            PlayerPrefs.SetInt("BestTime", bestTime);
        }

        scoreText.SetText("Time: " + Time / 60 + ":" + Time % 60);
        highScoreText.SetText("Best time: " + bestTime / 60 + ":" + (bestTime % 60).ToString("00"));
    }

    public void Play() {
        timer = 0;
        LoadHighscore();
        playing = true;
        canvas.SetActive(false);
        ball.SetActive(true);

    }



    public void Victory()
    {
        playing = false;
        PlayerPrefs.SetInt("Time", (int)(timer));
        LoadHighscore();

    }


    public void GameOver(GameObject g, Collider2D c) {
        playing = false;
        canvas.SetActive(true);
        SceneManager.LoadScene(0);
    }





   


        










}
