using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum CharacterState
{
    None = 0,
    Idle = 1 << 0,
    Crouch = 1 << 1,
    Block = 1 << 2,
    Hit = 1 << 3,
    Down = 1 << 4,
    DownRecovery = 1 << 5,
    Walk = 1 << 6,
    Dash = 1 << 7,
    Jump = 1 << 8,
    JumpHit = 1 << 9,
    JumpDown = 1 << 10,
    JumpLight = 1 << 11,
    JumpHeavy = 1 << 12,
    Light = 1 << 13,
    LightRecovery = 1 << 14,
    LightConsecutive = 1 << 15,
    LightConsecutiveRecovery = 1 << 16,
    HeavyCharge = 1 << 17,
    Heavy = 1 << 18,
    HeavyRecovery = 1 << 19,
    AbilityOne = 1 << 20,
    AbilityOneRecovery = 1 << 21,
    AbilityTwo = 1 << 22,
    AbilityTwoRecovery = 1 << 23,
    AbilityThree = 1 << 24,
    AbilityThreeRecovery = 1 << 25
};

public class NBCharacterController : MonoBehaviour {



    public float walkSpeedForward;
    public float walkSpeedBackward;
    public float dashSpeedForward;
    public float dashSpeedBackward;
    public int dashLeeway;
    public float maxJumpHeight;
    public float maxJumpTime;
    public float fallTime;
    public bool buttonBlock = false;
    [Range(0.0f,1.0f)]
    public float idleStopRatio = 0.0f;

    public bool grounded = false;
    public int playerNum;

    public CharacterState currentCharacterState;

    private Rigidbody2D rb;
    private Animator anim;

    public List<CharacterState> stateQueue;
    public List<InputState> inputQueue;
    public int queueSize;
    private Vector2 vel;
    private float gravity;
    private bool walkingForward = false;
    
    // Use this for initialization
    void Start ()
    {
        stateQueue = new List<CharacterState>();
        inputQueue = new List<InputState>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravity = -2.0f * (maxJumpHeight) / (maxJumpTime * maxJumpTime);

    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void FixedUpdate()
    {
        if (inputQueue.Count > 0)
        {
            InputState currentInputState = inputQueue[inputQueue.Count - 1];
            sendCrouch(currentInputState.moveY);
            sendWalk(currentInputState.moveX);
            sendDash(currentInputState.moveX);
            sendBlock(currentInputState.moveX, currentInputState.buttonBlock.wasPressed);
            sendJump(currentInputState.moveY);
            
        }
        checkGrounded();
        sendGrounded();
        stateQueue.Add(currentCharacterState);
        if (stateQueue.Count > queueSize)
            stateQueue.RemoveAt(0);
        doFace();
        doIdle();
        doGravity();
    }

    public void takeInput(InputState state)
    {
        inputQueue.Add(state);
        if (inputQueue.Count > queueSize)
            inputQueue.RemoveAt(0);
    }

    public void setState(CharacterState state)
    {
        currentCharacterState = state;
    }

    void sendTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    void sendBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    void sendFloat(string name, float value)
    {
        anim.SetFloat(name, value);
    }

    void sendCrouch(float moveY)
    {
        CharacterState affectedStates = CharacterState.Idle | CharacterState.Crouch |
            CharacterState.Walk;
        if ((currentCharacterState & affectedStates) != 0)
        {
            sendBool("Crouch", moveY < -0.5f);
        }
    }

    void sendWalk(float moveX)
    {
        CharacterState affectedStates = CharacterState.Idle | CharacterState.Crouch |
            CharacterState.Walk;
        if ((currentCharacterState & affectedStates) != 0)
        {
            sendFloat("Walk", moveX * Mathf.Sign(transform.localScale.x));
            walkingForward = (moveX * Mathf.Sign(transform.localScale.x)) > 0.1f;
        }
    }

    void sendDash(float moveX)
    {
        CharacterState affectedStates = CharacterState.Idle | CharacterState.Walk;
        if ((currentCharacterState & affectedStates) != 0)
        {

            float abs = Mathf.Abs(moveX);
            if (abs > 0.5f)
            {
                float sign = Mathf.Sign(moveX) * Mathf.Sign(transform.localScale.x);
                bool wentCenter = false;
                bool wentOpposite = false;
                bool triggered = false;
                CharacterState dashableStates = CharacterState.Idle | CharacterState.Walk;

                for (int i = inputQueue.Count - 2; i > inputQueue.Count - 1 - dashLeeway; i--)
                {
                    float iSign = Mathf.Sign(inputQueue[i].moveX) * Mathf.Sign(transform.localScale.x);
                    float iAbs = Mathf.Abs(inputQueue[i].moveX);

                    if (iAbs > 0.5f)
                    {
                        if (iSign != sign)
                        {
                            wentOpposite = true;
                        }
                        if (iSign == sign && wentCenter)
                        {
                            triggered = true;
                        }
                    }
                    if (iAbs < 0.5f)
                    {
                        wentCenter = true;
                    }

                    if (wentOpposite || triggered || (stateQueue[i] & dashableStates) == 0)
                    {
                        break;
                    }
                }
                if (triggered)
                {
                    if (sign > 0.5f)
                        sendTrigger("Dash(Forward)");
                    else
                        sendTrigger("Dash(Backward)");
                }
            }
        }
    }

    void sendBlock(float moveX, bool wasPressed)
    {
        CharacterState affectedStates = CharacterState.Idle | CharacterState.Crouch |
            CharacterState.Walk;
        if ((currentCharacterState & affectedStates) != 0)
        {
            if (buttonBlock == false)
            {
                NBCharacterController opponent = CharacterLocator.locator.getCharacter((playerNum % 2) + 1);
                if (opponent != null)
                {
                    CharacterState attackingStates = CharacterState.Heavy | CharacterState.Light | CharacterState.LightConsecutive |
                        CharacterState.JumpLight | CharacterState.JumpHeavy | CharacterState.AbilityOne | CharacterState.AbilityTwo |
                        CharacterState.AbilityThree;
                    if ((opponent.currentCharacterState & attackingStates) != 0)
                    {
                        bool opponentLeftCheck = opponent.transform.position.x < transform.position.x && moveX > 0.1f;
                        bool opponentRightCheck = opponent.transform.position.x > transform.position.x && moveX < -0.1f;
                        if (opponentLeftCheck || opponentRightCheck)
                        {
                            sendTrigger("Block");
                        }
                    }
                }
            }
            else
            {
                if (wasPressed)
                {
                    sendTrigger("Block");
                }
            }
        }
    }

    void sendJump(float moveY)
    {
        CharacterState affectedStates = CharacterState.Idle | CharacterState.Crouch |
            CharacterState.Walk;
        if ((currentCharacterState & affectedStates) != 0)
        {
            if(moveY > 0.5f)
            {
                sendTrigger("Jump");
            }
        }
    }

    void sendGrounded()
    {
        sendBool("Grounded", grounded);
    }

    void getVelocity()
    {
        vel = rb.velocity;
    }

    void checkGrounded()
    {
        if (Mathf.Abs(rb.velocity.y) < Mathf.Epsilon)
        {
            if (vel.y < -Mathf.Epsilon || Mathf.Abs(vel.y) < Mathf.Epsilon)
            {
                grounded = true;
            }
        }
        if (Mathf.Sign(vel.y) != Mathf.Sign(rb.velocity.y))
        {
            gravity = -2.0f * maxJumpHeight / (fallTime * fallTime);
        }
        if(rb.velocity.y > 0.0f)
        {
            grounded = false;
        }
        getVelocity();

    }


    void doFace()
    {
        NBCharacterController opponent = 
            CharacterLocator.locator.getCharacter((playerNum % 2) + 1);
        if (opponent != null)
        {
            float facing =
                Mathf.Sign(opponent.transform.position.x - transform.position.x);
            transform.localScale = new Vector3(facing * Mathf.Abs(transform.localScale.x),
                transform.localScale.y, transform.localScale.z);
        }
    }

    void doJump()
    {
        float initVel = 2.0f * maxJumpHeight / maxJumpTime;
        grounded = false;
        gravity = 2.0f * (maxJumpHeight - initVel * maxJumpTime) / (maxJumpTime * maxJumpTime);
        rb.velocity = new Vector2(rb.velocity.x, initVel);
    }

    void doIdle()
    {
        CharacterState affectedStates = CharacterState.Idle | CharacterState.Crouch;
        if ((currentCharacterState & affectedStates) != 0)
            rb.velocity = new Vector2(rb.velocity.x * idleStopRatio, rb.velocity.y);
    }

    void doGravity()
    {
        rb.velocity = rb.velocity + new Vector2(0.0f, gravity * Time.deltaTime);
    }

    void doWalk()
    {
        float vel = 0.0f;
        if(walkingForward)
        {
            vel = Mathf.Sign(transform.localScale.x) * walkSpeedForward / anim.GetCurrentAnimatorStateInfo(0).length;
        }
        else
        {
            vel = Mathf.Sign(transform.localScale.x) * -walkSpeedBackward / anim.GetCurrentAnimatorStateInfo(0).length;
        }
        rb.velocity = new Vector2(vel, rb.velocity.y);
    }

}
