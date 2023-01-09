using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Countdownn : MonoBehaviour
{
    public float timeStart = 60;
    public Text textBox;
    public GameOver GameOver;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        if (timeStart == 0)
        {
            SceneManager.LoadScene("Menu");
            GameOver.Setup(4);
            
        }

        textBox.text = Mathf.Round(timeStart).ToString();
        Debug.Log("ddd");

    }
}
