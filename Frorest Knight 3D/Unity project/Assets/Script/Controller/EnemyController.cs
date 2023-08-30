using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates
{
    GUARD,CHASE,DEAD
}
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyController : MonoBehaviour, IEndGameObserver
{   
    private bool playerDie=false;
    private CharacterStats characterStats;
    public EnemyStates enemyStates;
    private NavMeshAgent agent;
    private Animator anim;
    [Header("Basic Settings")]
    public float speed;
    private float lastAttackTime=0;
    public float sightRadius;
    public float attackRadius;
    private GameObject attackTarget;
    bool isGuard;
    bool isChase;
    bool isFollow;
    bool isWalk;
    bool isAttack;

    void OnDisable()
    {
        if (!GameManager.IsInitialized) 
        { return; 
        }
        GameManager.Instance.RemoveObserver(this);
      }

    private void Start()
    {
        GameManager.Instance.AddObserver(this);
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
      

    }
    void Update()
    {

        if (!playerDie)
        {

            SwitchAnimation();
            lastAttackTime -= Time.deltaTime;
            if (characterStats.characterData.currentHealth <= 0)
            {
                anim.SetBool("Death", true);
                agent.destination = agent.transform.position;
            }
            //print(characterStats.characterData.maxHealth);

        }
        switchStates();
    }

    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Attack", isAttack);
    }
    void switchStates()
    {   
        if (FoundPlayer())
        {
            isWalk = true;
            isChase = false;
            isFollow = false;
            if (agent.speed < 14)
            {
                agent.speed = agent.speed + 0.005f;
            }
            if (attackTarget != null)
            {

                try
                {
                    agent.destination = attackTarget.transform.position;
                }catch(System.Exception e)
                {

                }

             }
            else
            {
                isWalk = false;
                isFollow = false;
                isChase = false;
                isAttack = false;
                return;
            }
            if (AttackPlayer())
            {
                agent.transform.LookAt(attackTarget.transform.position);
                isAttack = true;
            }
            else
            {
                isAttack = false;
            }
            return;
        }
        else
        {
            isWalk = false;
            isFollow = false;
            isChase = false;
            return;
        }
        return;
    }
    bool AttackPlayer()
    {
        var colloders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (var target in colloders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }
    bool FoundPlayer()
    {
        var colloders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var target in colloders)
        {
            if (target.CompareTag("Player"))
            {
                foreach (var targetBase in colloders)
                {
                    if (targetBase.CompareTag("Base"))
                    {

                            if (Vector3.Distance(transform.position, target.transform.position) > Vector3.Distance(transform.position, targetBase.transform.position))
                            {
                                if (attackTarget == null)
                                {
                                    attackTarget = targetBase.gameObject;
                                }
                            }

                            else
                            {
                                if (attackTarget == null)
                                {
                                    attackTarget = target.gameObject;
                                }

                            }
                        

                    }
                }
                int baseNum = 0;
                foreach (var targetBase in colloders)
                {
                    if (targetBase.CompareTag("Base"))
                    {
                        baseNum++;
                    }
                }
                if (baseNum == 0)
                {
                    attackTarget = target.gameObject;
                }
                return true;
            }
            else
            {
                attackTarget = null;
            }
        }
        return false;
    }

    // Animation Event

    void hit()
    {
        if (attackTarget != null)
        {
            FindObjectOfType<AudioManager>().play("ememyAttack");
            // print("hit player");
            var targetStats = attackTarget.GetComponent<CharacterStats>();
            targetStats.TakeDamage(characterStats, targetStats);
        }
    }

    void destorySelf()
    {
        PlayerHealthUI.EnemyLeft = PlayerHealthUI.EnemyLeft - 1;
        FindObjectOfType<AudioManager>().play("EnemyDie");
        Destroy(gameObject);
    }

    public void EndNotify()
    {
        //ªÒ §∂Øª≠
        //Õ£÷π“∆∂Ø
        isWalk = false;
        isAttack = false;
        attackTarget = null;
        playerDie = true;
        anim.SetBool("Win", true);
    }
}
