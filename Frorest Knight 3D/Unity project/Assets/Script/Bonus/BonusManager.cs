using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{

    public  GameObject bonus;
    public  int addNum=15;
    private void Awake()
    {
        

            for (int i = 0; i < addNum; i++)
            {
                Initialarea1();
            }

    }

    public void Initialarea1()
    {
        int x = Random.Range(0, 3);
        if (x==0)
        {
            Instantiate(bonus, new Vector3(Random.Range(-36f, -14f), 1, Random.Range(9f, -2f)), Quaternion.identity);
        }
        if (x == 1)
        {
            Instantiate(bonus, new Vector3(Random.Range(-13f, 0f), 1, Random.Range(-33f, -4f)), Quaternion.identity);
        }
        if (x == 2)
        {
            Instantiate(bonus, new Vector3(Random.Range(11f, 1f), 1, Random.Range(12f, 35f)), Quaternion.identity);
        }
    }

}
