using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject chloe;

    public bool Setup(int hello){
        gameObject.SetActive(true);
        Destroy(chloe);
        return true;
    }

    public void TryAgain()
   {
       SceneManager.LoadScene("Game");
   }

   public void Menu()
   {
       SceneManager.LoadScene("Menu");
   }
}
