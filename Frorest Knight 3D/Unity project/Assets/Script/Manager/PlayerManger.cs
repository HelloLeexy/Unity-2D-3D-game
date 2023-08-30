using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EventMouse : UnityEvent<Vector3>
{

}

public class PlayerManger : MonoBehaviour
{
    public static float superTime=0;
    public GameObject powerL;
    public BonusManager bouns1;
    GameObject bouns;
    public static float Score=0;
    float time1 = 0;
    float time = 0;
    private int times=0;
    private bool isDeath=false;
    private float lastAttacktime;
    private float lastRuntime=0;
    private CharacterStats characterStats;
    public Texture2D point;
    private Animator anim;
    public float speed1 = 7f;
    public float speed2 = 14f;
    public float speed;
    bool isRun;
    bool canRun;
    private bool isTurn=true;
    Vector3 movement;
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    public static GameObject Enemy_target;
    Rigidbody enemybody;
    // Start is called before the first frame update

    void Start()
    {
        GameManager.Instance.RigisterPlayer(characterStats);
       
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        playerRigidbody = GetComponent<Rigidbody>();
        powerL.SetActive(false);

    }
    private void SwitchAnimation()
    {
        anim.SetFloat("Speed",speed);
    }
    // Update is called once per frame

 
    void Update()
    {
        InDanger();
        OutRange();
        // Store the input axes.			
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // Move the player around the scene.
        if (!isDeath)
        {
            Move(h, v);
        }

        MouseControl();
        SetCursorTexture();
        SwitchAnimation();
        Run();
        Attack();
        lastAttacktime -= Time.deltaTime;
        FoundPlayer();
        if (characterStats.characterData.currentHealth <= 0)
        {
            anim.SetBool("Death", true);
            isDeath = true;
            GameManager.Instance.NotifyObservers();
        }
        superPower();
    }

    void superPower()
    {
        if (superTime > 0)
        {
            //FindObjectOfType<AudioManager>().play("superPower");
            superTime = superTime - Time.deltaTime;
            powerL.SetActive(true);
            powerL.transform.Rotate(Vector3.up * Time.deltaTime * 80);
            characterStats.characterData.currentHealth = 100;
            characterStats.characterData.currentDefence = 100;
            characterStats.characterData.currentSP = 100;
        }
        else
        {
            powerL.SetActive(false);
            characterStats.characterData.currentDefence = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bonus"))
        {
            FindObjectOfType<AudioManager>().play("Getbouns");
            bouns = collision.collider.gameObject;
            Destroy(bouns);
            PlayerHealthUI.Score = PlayerHealthUI.Score + 10;
            Invoke("creatNewBonus", 60);
        }
        if (collision.collider.gameObject.name=="Base(Clone)")
        {
            FindObjectOfType<AudioManager>().play("PlayerTouchBase");
            superTime = superTime + 3;
            PlayerHealthUI.Score = PlayerHealthUI.Score + 5;
        } 
    }

     void  creatNewBonus()
    {
        bouns1.Initialarea1();
    }

    void OutRange()
    {
        if (Enemy_target != null)
        {
            if (Vector3.Distance(transform.position, Enemy_target.transform.position) > 3)
            {
                Enemy_target = null;
            }
        }
    }
    void FoundPlayer()
    {
        var colloders = Physics.OverlapSphere(transform.position, 3);

        foreach (var target in colloders)
        {   
            if (target.gameObject.tag=="Enemy" && Enemy_target == null)
            {
                Enemy_target = target.gameObject;
                //print(Enemy_target);
            }
            else
            {
               //Enemy_target = null;
            }
            
        }
        
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&lastAttacktime<0&&!isDeath)
        {

            if (Enemy_target!=null)
            {

                anim.Play("attack");
            }
            else
            {
                anim.Play("attack");
            }

            lastAttacktime = characterStats.attackData.coolDown;
            
        }
    }
    void Run()
    {


        if (Input.GetKey(KeyCode.LeftShift) && characterStats.characterData.currentSP>0)
        {
            speed = speed2;
            time += Time.deltaTime;
            if (time >= 1f)//“ª√Î
            {
                characterStats.characterData.currentSP = characterStats.characterData.currentSP - 10f;
                time = 0;
            }
        }
        else if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.D) == false)
        {
            speed = 0;

        } else
        {
            speed = speed1;
            if (characterStats.characterData.currentSP <= 100)
            {
                time += Time.deltaTime;
                if (time >= 1f)//“ª√Î
                {
                    characterStats.characterData.currentSP = characterStats.characterData.currentSP + 20f;
                    time = 0;
                }
                if (characterStats.characterData.currentSP > 100)
                {
                    characterStats.characterData.currentSP = 100;
                }

            }

        }

    }

    void InDanger()
    {
        float x = transform.position.x;
        float z = transform.position.z;
        if (x > -9 && x < -2)
        {
            if (z < 6 && z > 0.5)
            {
                time1 += Time.deltaTime;
                if (time1 >= 2f)//“ª√Î
                {
                    characterStats.characterData.currentHealth = characterStats.characterData.currentHealth - 2;
                    time1 = 0;
                }
            }
        }
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }
    RaycastHit hitinfo;
    public EventMouse OnMouseClicked;

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hitinfo))
        {
            Cursor.SetCursor(point, new Vector2(-16, 16), CursorMode.Auto);
        }
    }

    void MouseControl()
    {
        if (hitinfo.collider!= null)
        {
            if (hitinfo.collider.gameObject.CompareTag("Ground")&&!isDeath)
            {
                if ((hitinfo.point.x>playerRigidbody.transform.position.x+1|| hitinfo.point.x < playerRigidbody.transform.position.x - 1))
                {
                    OnMouseClicked?.Invoke(hitinfo.point);
                }


            }
        }
    }

    //Animation Event
    void Hit()
    {
        FindObjectOfType<AudioManager>().play("playattack");
        if (Enemy_target != null&& Vector3.Distance(transform.position, Enemy_target.transform.position) < characterStats.attackData.attackRange)
        {
            transform.LookAt(Enemy_target.transform.position);
            var targetStats = Enemy_target.GetComponent<CharacterStats>();
            targetStats.TakeDamage(characterStats, targetStats);

        }

    }
    void destorySelf()
    {
        UnityEngine.Object.Destroy(gameObject);
    }
}
