using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthBar : MonoBehaviour
{

    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image backHealth;
    public Image frontHealth;
    public GameObject other;

    public GameOver GameOver;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }



    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        
    }


    public void UpdateHealthUI()
    {
        
        float frontF = frontHealth.fillAmount;
        float backF = backHealth.fillAmount;
        float hFraction = health / maxHealth;
        if (backF > hFraction){
            frontHealth.fillAmount = hFraction;
            backHealth.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealth.fillAmount = Mathf.Lerp(backF, hFraction, percentComplete);
        }
        if (frontF < hFraction){
            backHealth.color = Color.green;
            backHealth.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealth.fillAmount = Mathf.Lerp(frontF, backHealth.fillAmount, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {

        //Debug.Log("before");
        health -= damage;
        lerpTimer = 0f;
        if (health <= 0){
            GameOver.Setup(4);
        }

    }
    public void Recovery(float healthAmount){
       // Debug.Log("gain");
        health += healthAmount;
        lerpTimer = 0f;
    }
}
