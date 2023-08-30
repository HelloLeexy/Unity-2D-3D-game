using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    Button Quit60;
    Button Continue60;
    Button Continue40;
    Button Quit40;
    Text text40;
    Text text60;
    public GameObject Panel40;
    public GameObject Panel60;
    void Awake()
    {

        text40 = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        text60 = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();
        Panel40.SetActive(false);
        Panel60.SetActive(false);
        Continue40 = transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>();
        Quit40 = transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Button>();
        Continue60 = transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
        Quit60 = transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>();

        Continue40.onClick.AddListener(OnContinue40);
        Quit40.onClick.AddListener(OnQuit40);
        Continue60.onClick.AddListener(OnContinue60);
        Quit60.onClick.AddListener(OnQuit60);


    }

    private void Update()
    {
        if ((float)GameManager.Instance.playerStats.CurrentHealth <= 0)
        {
            if (PlayerHealthUI.Score < 50)
            {
                Score40show();
            }
            else
            {
                Score60show();
            }
        }
    }
    void Score40show()
    {
        Panel40.SetActive(true);
        text40.text = "You get " + PlayerHealthUI.Score + " this time";
    }
    void Score60show()
    {
        Panel60.SetActive(true);
        text60.text = "You get " + PlayerHealthUI.Score + " this time";
    }

    void OnContinue40()
    {
        SceneManager.LoadScene("SampleScene");
        EnemyInitializer.isreload = false;
        PlayerHealthUI.EnemyLeft = 1;
        PlayerHealthUI.Score = 0;
    }
    void OnQuit40()
    {
        SceneManager.LoadScene("Main");
        EnemyInitializer.isreload = false;
        PlayerHealthUI.EnemyLeft = 1;
        PlayerHealthUI.Score = 0;

    }
    void OnContinue60()
    {
        SceneManager.LoadScene("SampleScene");
        EnemyInitializer.isreload = false;
        PlayerHealthUI.EnemyLeft = 1;
        PlayerHealthUI.Score = 0;
    }
    void OnQuit60()
    {
        SceneManager.LoadScene("Main");
        EnemyInitializer.isreload = false;
        PlayerHealthUI.EnemyLeft = 1;
        PlayerHealthUI.Score = 0;
    }
}
