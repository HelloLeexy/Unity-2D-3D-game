using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    Image healthSlider;
    Image spSlider;
    Text ScoreText;
    Text EnemyText;
    private float LastTime=10;
    private float LastTime1 = 60;
    public static int Score= 0;
    public static int EnemyLeft=1;
    void Awake()
    {
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        spSlider=transform.GetChild(1).GetChild(0).GetComponent<Image>();
        ScoreText = transform.GetChild(2).GetComponent<Text>();
        EnemyText = transform.GetChild(3).GetComponent<Text>();


    }
    void countScore()
    {
        if (LastTime > 0)
        {
            LastTime -= Time.deltaTime;
        }
        else
        {
            Score = Score + 1;
            LastTime = 10;
        }
        if (LastTime1 > 0)
        {
            LastTime1 -= Time.deltaTime;
        }
        else
        {
            Score = Score + 2;
            LastTime1 = 60;
        }
    }
    void Update()
    {
        ScoreText.text = "Score  "+Score;
        EnemyText.text = "Enemies  " + EnemyLeft;
        UpdataHealth();
        Updatasp();
        if ((float)GameManager.Instance.playerStats.CurrentHealth>0)
        {
            countScore();
        }

    }

    void UpdataHealth()
    {
        float sliderPercent = (float)GameManager.Instance.playerStats.CurrentHealth / GameManager.Instance.playerStats.MaxHealth;
        healthSlider.fillAmount = sliderPercent;
    }
    void Updatasp()
    {
        float sliderPercent = (float)GameManager.Instance.playerStats.characterData.currentSP / GameManager.Instance.playerStats.MaxHealth;
        spSlider.fillAmount = sliderPercent;
    }

}
