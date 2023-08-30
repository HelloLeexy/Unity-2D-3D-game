using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class MainCanves : MonoBehaviour
{
    Button NewGame;
    Button RePlay;
    Button Setting;
    Button Manul;
    Button Quit;
    bool show = false;
    public GameObject player;
    public GameObject volumeManager;
    Dropdown reso;

    void Awake()
    {
        NewGame = transform.GetChild(1).GetComponent<Button>();
        RePlay = transform.GetChild(2).GetComponent<Button>();
        Setting = transform.GetChild(3).GetComponent<Button>();
        Manul = transform.GetChild(4).GetComponent<Button>();
        Quit = transform.GetChild(5).GetComponent<Button>();
        reso = transform.GetChild(6).GetChild(2).GetComponent<Dropdown>();
        RePlay.onClick.AddListener(Continue);
        NewGame.onClick.AddListener(PlayNewGame);
        Setting.onClick.AddListener(Set);
        Manul.onClick.AddListener(Manu);
        Quit.onClick.AddListener(QuitGame);
        player.SetActive(true);
        volumeManager.SetActive(false);
    }
     void Update()
    {
        if (reso.value == 1)
        {
            Screen.SetResolution(1920,1080,true);
        }
        if (reso.value == 2)
        {
            Screen.SetResolution(1920, 1200, false);
        }
        if (reso.value == 3)
        {
            Screen.SetResolution(1680, 1050, false);
        }
        if (reso.value == 4)
        {
            Screen.SetResolution(1440, 900, false);
        }
        if (reso.value == 5)
        {
            Screen.SetResolution(1280, 800, false);
        }
    }
    void PlayNewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
        EnemyInitializer.isreload = false;
        PlayerHealthUI.EnemyLeft = 1;
        PlayerHealthUI.Score = 0;
    }

    void Continue()
    {
        SceneManager.LoadScene("SampleScene");
        EnemyInitializer.isreload = true;
        Time.timeScale = 1f;
        PlayerHealthUI.EnemyLeft = 1;
        PlayerHealthUI.Score = 0;
    }

    void Manu()
    {
        if (show==false)
        {
            player.SetActive(false);
            volumeManager.SetActive(true);
            show = true;
        }
        else
        {
            player.SetActive(true);
            volumeManager.SetActive(false);
            show = false;
        }

    }
    void Set()
    {
        SceneManager.LoadScene("Manuak");
    }
    void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

}
