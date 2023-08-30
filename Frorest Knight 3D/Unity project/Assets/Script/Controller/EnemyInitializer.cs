using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class EnemyInitializer : MonoBehaviour
{
    public static int leftEnemy = -1;
    public static int score;
    public GameObject enemy;
    public GameObject bases;
    public GameObject replacedBase;
    public GameObject bouns;
    public static bool isreload;

    private void Start()
    {
        if (isreload == true)
        {
            reload();
        }
        else
        {
            newgame();
        }
       //newgame();
      //reload();
        
    }

    void reload()
    {
        isreload = true;
        LoadEnemyData();
        loadPlayerData();
        for (int i = 0; i < leftEnemy; i++)
        {
            PlayerHealthUI.EnemyLeft++;
        }
        PlayerHealthUI.Score = score;
        //loadBase();
        //loadBouns();
        //loadReBase();
    }
    void newgame()
    {
        isreload = false;
        Instantiate(enemy, new Vector3(0, 0, 16), Quaternion.identity);
    }


    void LoadEnemyData()
    {

        string pathenemy = Application.persistentDataPath + "/store.enemy";
        if (File.Exists(pathenemy))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathenemy, FileMode.Open);
            var data = formatter.Deserialize(stream) as List<float[]>;
            stream.Close();
            score = (int)data[0][4];
            try
            {

                for (int i = 1; i < data.Capacity; i++)
                {
                    float health = data[i][0];
                    float x = data[i][1];
                    float y = data[i][2];
                    float z = data[i][3];
                    GameObject ene = Instantiate(enemy, new Vector3(x, y, z), Quaternion.identity);
                    ene.GetComponent<CharacterStats>().characterData.currentHealth = (int)health;
                    leftEnemy++;
                }

            }
            catch(System.Exception e)
            {
            }

        }
    }

    void loadPlayerData()
    {
        string pathplayer = Application.persistentDataPath + "/store.player";
        if (File.Exists(pathplayer))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathplayer, FileMode.Open);
            var data = formatter.Deserialize(stream) as List<float[]>;
            stream.Close();
                float health = data[0][0];
                float SP = data[0][1];
                float x = data[0][2];
                float y = data[0][3];
                float z = data[0][4];
                GameObject player = GameObject.Find("Player");
                player.transform.Translate(x, y, z);
                player.GetComponent<CharacterStats>().characterData.currentHealth = (int)health;
                player.GetComponent<CharacterStats>().characterData.currentSP = (int)SP;

        }
        else
        {
        }
    }

    void loadBase()
    {
        string pathenemy = Application.persistentDataPath + "/store.base";
        if (File.Exists(pathenemy))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathenemy, FileMode.Open);
            var data = formatter.Deserialize(stream) as List<float[]>;
            stream.Close();
            try
            {
                for (int i = 1; i < data.Capacity; i++)
                {
                    float x = data[i][1];
                    float y = data[i][2];
                    float z = data[i][3];
                    Instantiate(bases, new Vector3(x, y, z), Quaternion.identity);
                }

            }
            catch (System.Exception e)
            {
            }

        }
    }

    void loadReBase()
    {
        string pathenemy = Application.persistentDataPath + "/store.baserep";
        if (File.Exists(pathenemy))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathenemy, FileMode.Open);
            var data = formatter.Deserialize(stream) as List<float[]>;
            stream.Close();
            try
            {
                for (int i = 1; i < data.Capacity; i++)
                {
                    float x = data[i][1];
                    float y = data[i][2];
                    float z = data[i][3];
                    Instantiate(replacedBase, new Vector3(x, y, z), Quaternion.identity);
                }

            }
            catch (System.Exception e)
            {
            }

        }
    }

    void loadBouns()
    {
        string pathenemy = Application.persistentDataPath + "/store.bonus";
        if (File.Exists(pathenemy))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(pathenemy, FileMode.Open);
            var data = formatter.Deserialize(stream) as List<float[]>;
            stream.Close();
            try
            {
                for (int i = 1; i < data.Capacity; i++)
                {
                    float x = data[i][1];
                    float y = data[i][2];
                    float z = data[i][3];
                    Instantiate(bouns, new Vector3(x, y, z), Quaternion.identity);
                }

            }
            catch (System.Exception e)
            {
            }

        }

    }
}

