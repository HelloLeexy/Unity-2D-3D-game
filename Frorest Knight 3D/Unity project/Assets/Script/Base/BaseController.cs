using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseController : MonoBehaviour
{
    public BaseManager CreatBasee;
    float speed;
    public GameObject enemy;
    public GameObject ReplaceBase;
    GameObject NewEnemy;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().play("enemyAttackbase");
            speed = other.gameObject.GetComponent<NavMeshAgent>().speed;
            other.gameObject.GetComponent<NavMeshAgent>().speed = other.gameObject.GetComponent<NavMeshAgent>().speed * 0.9f;
            NewEnemy=Instantiate(enemy, transform.position, Quaternion.identity);
            NewEnemy.gameObject.GetComponent<NavMeshAgent>().speed = speed/2;
            PlayerHealthUI.EnemyLeft = PlayerHealthUI.EnemyLeft + 1;
            transform.Translate(0, -300, 0);
            Invoke("creatNewBases", 120);

        }
        else if (other.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Instantiate(ReplaceBase, transform.position, Quaternion.identity);
        }
    }
    void creatNewBases()
    {
        CreatBasee.Initialarea1();
        Destroy(this.gameObject);
    }
}
