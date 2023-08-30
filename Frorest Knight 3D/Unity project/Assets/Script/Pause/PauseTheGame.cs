using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class PauseTheGame : MonoBehaviour
{
    Button Continue;
    Button SaveQuit;
    bool GameIsPause = false;
    public GameObject TheCanves ;
    private void Awake()
    {
        TheCanves.SetActive(false);
        Continue = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
        SaveQuit = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>();
        Continue.onClick.AddListener(ButContinue);
        SaveQuit.onClick.AddListener(ButSaveQuit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameIsPause == false)
            {
                GameIsPause = true;
            }else 
            {
                GameIsPause = false;
                Time.timeScale = 1f;
                TheCanves.SetActive(false);
            }
        }
        if (GameIsPause == true)
        {
            GamePause();
        }
    }

    void ButContinue()
    {
        GameIsPause = false;
        Time.timeScale = 1f;
        TheCanves.SetActive(false);
    }

    void ButSaveQuit()
    {
        GetEnemyInfo();
        GetPlayerInfo();
        getbaseInfo();
        getRebaseInfo();
        getBonus();
        SceneManager.LoadScene("Main");
    }
    void GamePause()
    {
        Time.timeScale = 0f;
        TheCanves.SetActive(true);
    }

    void GetPlayerInfo()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        List<float[]> Player = new List<float[]>();
        var PlayerInfo = GameObject.FindGameObjectsWithTag("Player");
        foreach (var any in PlayerInfo)
        {
            float health = (any.GetComponent<CharacterStats>().characterData.currentHealth);
            float SP = (any.GetComponent<CharacterStats>().characterData.currentSP);
            float x = (any.transform.position.x);
            float y = (any.transform.position.y);
            float z = (any.transform.position.z);
            float[] player = new float[5];
            player[0] = health;
            player[1] = SP;
            player[2] = x;
            player[3] = y;
            player[4] = z;
            Player.Add(player);
        }

        string path = Application.persistentDataPath + "/store.player";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, Player);
        stream.Close();
    }

    void GetEnemyInfo()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        List<float[]> Enemyinfo = new List<float[]>();
        var Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int index = 0;
        foreach (var any in Enemies)
        {
            index++;
            float health = (any.GetComponent<CharacterStats>().characterData.currentHealth);
            float x = (any.transform.position.x);
            float y = (any.transform.position.y);
            float z = (any.transform.position.z);
            float[] AnyEnemy = new float[5];
            AnyEnemy[0] = health;
            AnyEnemy[1] = x;
            AnyEnemy[2] = y;
            AnyEnemy[3] = z;
            AnyEnemy[4] = PlayerHealthUI.Score;
            Enemyinfo.Add(AnyEnemy);
        }
        string path = Application.persistentDataPath + "/store.enemy";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, Enemyinfo);
        stream.Close();
    }
    void getbaseInfo()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        List<float[]> BaseInfo = new List<float[]>();
        var Bases = GameObject.FindGameObjectsWithTag("Base");
        foreach (var any in Bases)
        {
            if (any.name == "Base")
            {
                float x = (any.transform.position.x);
                float y = (any.transform.position.y);
                float z = (any.transform.position.z);
                float[] AnyBase = new float[4];
                AnyBase[0] = 0;
                AnyBase[1] = x;
                AnyBase[2] = y;
                AnyBase[3] = z;
                BaseInfo.Add(AnyBase);
            }

        }
        string path = Application.persistentDataPath + "/store.base";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, BaseInfo);
        stream.Close();
    }

    void getRebaseInfo()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        List<float[]> RBaseInfo = new List<float[]>();
        var RBases = GameObject.FindGameObjectsWithTag("Base");
        foreach (var any in RBases)
        {
            if (any.name == "Basereplaced")
            {
                float x = (any.transform.position.x);
                float y = (any.transform.position.y);
                float z = (any.transform.position.z);
                float[] RAnyBase = new float[4];
                RAnyBase[0] = 0;
                RAnyBase[1] = x;
                RAnyBase[2] = y;
                RAnyBase[3] = z;
                RBaseInfo.Add(RAnyBase);
            }

        }
        string path = Application.persistentDataPath + "/store.baserep";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, RBaseInfo);
        stream.Close();
    }

    void getBonus()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        List<float[]> BonusInfo = new List<float[]>();
        var Bonus = GameObject.FindGameObjectsWithTag("Bonus");
        foreach (var any in Bonus)
        {
            float x = (any.transform.position.x);
            float y = (any.transform.position.y);
            float z = (any.transform.position.z);
            float[] AnyBonus = new float[4];
            AnyBonus[0] = 0;
            AnyBonus[1] = x;
            AnyBonus[2] = y;
            AnyBonus[3] = z;
            BonusInfo.Add(AnyBonus);


        }
        string path = Application.persistentDataPath + "/store.bonus";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, BonusInfo);
        stream.Close();
    }
}
