using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public Control player;
    public Text mainText;
    int count;
    public Text counter;

    public GameObject pauseMenu;
    public Camera mainCamera;

    public GameOver GameOver;

    AudioSource music;

    bool nexxt;
    Text next;
    bool enable;

    // Define the triggers for deleted, finalLevel, and sumCounter.Sets the counter to 0 and the reset buttons are disabled
    
    void Start () 
    {

        Time.timeScale = 1;

        player.finalLevel += Final;
        player.sumCounter += Count;
        player.enabled = true;
        mainText.text = "";
        count = 0;
        counter.text = "0";

        pauseMenu.SetActive(false);

        music = mainCamera.GetComponentInChildren<AudioSource>();
    }

    public void Update()
    {
        // if (Input.GetKeyUp(KeyCode.Escape))
        // {
        //     pauseMenu.SetActive(!pauseMenu.activeSelf);
        //     if (Time.timeScale == 1) {
        //         Time.timeScale = 0;
        //         music.Pause();
        //     }
        //     else if (Time.timeScale == 0)
        //     {   
        //         Time.timeScale = 1;
        //         music.Play();
        //     }
        // }
        if (Input.GetKeyUp(KeyCode.Escape)){
            pauseMenu.SetActive(true);
            
        }
        else if (Input.GetKeyUp(KeyCode.Return)){
                pauseMenu.SetActive(false);
        }
    }

    
    public void Count()
    {
        count++;
        counter.text = count.ToString();
    }



    void Final(){

        player.setVelocity();
        player.enabled = false;
        mainText.text = "You Won!!";
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            next.text = "Beginning Scene";
        }
        next.enabled = true;
    }

    public void NextScreen()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("Game"))
        {
            SceneManager.LoadScene("Menu");
        }
 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
