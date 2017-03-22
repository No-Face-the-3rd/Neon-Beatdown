using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MyCharacterController : MonoBehaviour {
    public int playerNum;
    public KeyCode moveLeft, moveRight;
    public KeyCode highAttack, lowAttack, block, abilityOne, abilityTwo, abilityThree;
    
    public float speed;
    public float jumpStrength;
    public float slowRate;
    public Timer oneTimer, twoTimer, threeTimer;

    public GameObject abilityOneObj, abilityTwoObj, abilityThreeObj;
    public Vector3 attackSpawn;
    public float dashSpeed;

    public Timer dashTimer;

    private bool grounded = false;
    private float horiz = 0.0f, vert = 0.0f;
    private Vector3 origScale;
    private Rigidbody2D rb;
    private GameObject enemy;
    private MyCharacterController enemyController;
    private SpriteRenderer sr;
    private bool enemyLeft = false;
    private int movePress = 0;
    private int dashFrames = 0;
    

    // Use this for initialization
    void Start () {
        origScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for(int i =0;i < players.Length;i++)
            if(players[i] != gameObject)
            {
                enemy = players[i];
                break;
            }
        if (enemy.transform.position.x > transform.position.x)
            enemyLeft = false;
        else
            enemyLeft = true;
        enemyController = enemy.GetComponent<MyCharacterController>();
        sr = GetComponent<SpriteRenderer>();
        oneTimer.init();
        twoTimer.init();
        threeTimer.init();
        oneTimer.setActive(true);
        twoTimer.setActive(true);
        threeTimer.setActive(true);
        dashTimer.init();
	}

    public void setGrounded(bool groundedIn)
    {
        grounded = groundedIn;
    }

    // Update is called once per frame
    void Update()
    {
        horiz = Input.GetAxisRaw("P" + playerNum + "Horiz");
        vert = Input.GetAxisRaw("P" + playerNum + "Vert");
        if (enemy.transform.position.x > transform.position.x)
        {
            if (enemyLeft == true)
                sr.flipX = !sr.flipX;
            enemyLeft = false;
            attackSpawn = new Vector3(0.5f, attackSpawn.y, attackSpawn.z);
        }
        else
        {
            if (enemyLeft == false)
                sr.flipX = !sr.flipX;
            enemyLeft = true;
            attackSpawn = new Vector3(-0.5f, attackSpawn.y, attackSpawn.z);
        }

        if (Input.GetKeyDown(abilityOne))
            doAbilityOne();
        if (Input.GetKeyDown(abilityTwo))
            doAbilityTwo();
        if (Input.GetKeyDown(abilityThree))
            doAbilityThree();

        if (dashTimer.isPassed())
        {

            if (Input.GetKeyDown(moveLeft))
            {
                if(movePress == -2)
                {
                    doDash(true);
                }
                else if (movePress != -1)
                {
                    movePress = -1;
                    dashFrames = 0;
                }
            }
            if(Input.GetKeyDown(moveRight))
            {
                if(movePress == 2)
                {
                    doDash(false);
                }
                else if(movePress != 1)
                {
                    movePress = 1;
                    dashFrames = 0;
                }
            }

            if (Input.GetKeyUp(moveLeft))
                if (movePress == -1)
                    movePress = -2;

            if (Input.GetKeyUp(moveRight))
                if (movePress == 1)
                    movePress = 2;
        } 

        if(movePress != 0)
        {
            dashFrames++;
        }
        if(dashFrames > 20)
        {
            movePress = 0;
        }


        oneTimer.update();
        twoTimer.update();
        threeTimer.update();
        dashTimer.update();

      
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            if (vert < 0.0f)
                transform.localScale = new Vector3(origScale.x, origScale.y * 0.5f, origScale.z);
            else
                transform.localScale = origScale;

            if (vert > 0.0f)
            {
                rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
                grounded = false;
            }

            rb.velocity =Vector2.Lerp(rb.velocity, new Vector2(horiz * speed, rb.velocity.y), 0.60f);
            if (horiz == 0.0f)
                rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        else
            if (horiz == 0.0f)
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(Mathf.Abs(rb.velocity.x) > slowRate ? (Mathf.Abs(rb.velocity.x) - slowRate) * Mathf.Sign(rb.velocity.x) : 0.0f, rb.velocity.y), 0.60f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
            grounded = true;        
    }

    void doAbilityOne()
    {
        if(oneTimer.isPassed())
        {
            GameObject attack = (GameObject)Instantiate(abilityOneObj, transform.position + attackSpawn, Quaternion.identity, null);
            attack.tag = "Player " + playerNum + " Attack";
            AttackController att = attack.GetComponent<AttackController>();
            attack.transform.position += new Vector3(Mathf.Sign(attackSpawn.x) * att.attackLength, 0.0f, 0.0f);
            att.owner = playerNum;
            att.origin = transform.position + attackSpawn;
            att.setLeft(enemyLeft);
            oneTimer.resetTimer();
            oneTimer.setActive(true);
        }
    }

    void doAbilityTwo()
    {
        if (twoTimer.isPassed())
        {
            GameObject attack = (GameObject)Instantiate(abilityTwoObj, transform.position + attackSpawn, Quaternion.identity, null);
            attack.tag = "Player " + playerNum + " Attack";
            AttackController att = attack.GetComponent<AttackController>();
            attack.transform.position += new Vector3(Mathf.Sign(attackSpawn.x) * att.attackLength, 0.0f, 0.0f);
            att.owner = playerNum;
            att.origin = transform.position + attackSpawn;
            att.setLeft(enemyLeft);
            twoTimer.resetTimer();
            twoTimer.setActive(true);
        }
    }

    void doAbilityThree()
    {
        if (threeTimer.isPassed())
        {
            //GameObject attack = (GameObject)Instantiate(abilityThreeObj,transform.position + attackSpawn, Quaternion.identity, null);
            //attack.tag = "Player " + playerNum + " Attack";
            //AttackController att = attack.GetComponent<AttackController>();
            //attack.transform.position += new Vector3(Mathf.Sign(attackSpawn.x) * att.attackLength, 0.0f, 0.0f);
            //att.owner = playerNum;
            //att.origin = transform.position + attackSpawn;
            //att.setLeft(enemyLeft);
            threeTimer.resetTimer();
            threeTimer.setActive(true);
        }
    }

    void doDash(bool left)
    {
        transform.Translate(Vector3.up * 0.1f);
        rb.AddForce((left ? Vector2.left : Vector2.right) * dashSpeed, ForceMode2D.Impulse);
        dashTimer.resetTimer();
        dashTimer.setActive(true);
    }

    public float abilityOneCooldown()
    {
        return oneTimer.percentPassed();
    }
    public float abilityTwoCooldown()
    {
        return twoTimer.percentPassed();
    }
    public float abilityThreeCooldown()
    {
        return threeTimer.percentPassed();
    }
}
