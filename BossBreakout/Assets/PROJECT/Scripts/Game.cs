using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private bool playing = false;
    public GameObject canvas;
    public GameObject ball;

    public void Update() {
        if(!playing && Input.anyKey) {
            Play();
        }
    }

    public void Play() {
        playing = true;
        canvas.SetActive(false);
        ball.SetActive(true);
    }

    public void GameOver(GameObject g, Collider2D c) {
        Debug.Log("GAMEOVER");
        playing = false;
        canvas.SetActive(true);
        SceneManager.LoadScene(0);
    }
}
