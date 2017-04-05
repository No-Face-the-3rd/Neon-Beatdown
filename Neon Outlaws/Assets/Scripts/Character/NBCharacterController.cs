using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBCharacterController : MonoBehaviour {

    [System.Flags]
    public enum CharacterState
    {
        Idle = 0,
        Crouch = 1 << 0,
        Block = 1 << 1,
        Hit = 1 << 2,
        Down = 1 << 3,
        DownRecovery = 1 << 4,
        Walk = 1 << 5,
        Dash = 1 << 6,
        Jump = 1 << 7,
        JumpHit = 1 << 8,
        JumpDown = 1 << 9,
        JumpLight = 1 << 10,
        JumpHeavy = 1 << 11,
        Light = 1 << 12,
        LightRecovery = 1 << 13,
        LightConsecutive = 1 << 14,
        LightConsecutiveRecovery = 1 << 15,
        HeavyCharge = 1 << 16,
        Heavy = 1 << 17,
        HeavyRecovery = 1 << 18,
        AbilityOne = 1 << 19,
        AbilityOneRecovery = 1 << 20,
        AbilityTwo = 1 << 21,
        AbilityTwoRecovery = 1 << 22,
        AbilityThree = 1 << 23,
        AbilityThreeRecovery = 1 << 24
    };


    public float walkSpeedForward;
    public float walkSpeedBackward;
    public float dashSpeedForward;
    public float dashSpeedBackward;
    public int dashLeeway;
    public float maxJumpHeight;
    public float maxJumpTime;
    public float fallTime;
    public bool buttonBlock = false;

    public int playerNum;

    public CharacterState currentCharacterState;

    private Rigidbody2D rb;
    private Animator anim;

    public List<InputState> inputQueue;
    public int queueSize;
    
    
    // Use this for initialization
    void Start ()
    {
        inputQueue = new List<InputState>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            switch (currentCharacterState)
            {
                case CharacterState.Idle:
                    sendCrouch(currentInputState.moveY);
                    sendWalk(currentInputState.moveX);
                    sendDash(currentInputState.moveX);
                    sendBlock(currentInputState.moveX, currentInputState.buttonBlock.wasPressed);
                    break;
                case CharacterState.Crouch:
                    sendCrouch(currentInputState.moveY);
                    sendWalk(currentInputState.moveX);
                    sendBlock(currentInputState.moveX, currentInputState.buttonBlock.wasPressed);
                    break;
                case CharacterState.Walk:
                    sendCrouch(currentInputState.moveY);
                    sendWalk(currentInputState.moveX);
                    sendDash(currentInputState.moveX);
                    sendBlock(currentInputState.moveX, currentInputState.buttonBlock.wasPressed);
                    break;
            }
            
        }
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
        sendBool("Crouch", moveY < -0.5f);
    }

    void sendWalk(float moveX)
    {
        sendFloat("Walk", moveX);
    }

    void sendDash(float moveX)
    {
        float abs = Mathf.Abs(moveX);
        if (abs > 0.5f)
        {
            float sign = Mathf.Sign(moveX);
            bool wentCenter = false;
            bool wentOpposite = false;
            bool triggered = false;

            for (int i = inputQueue.Count - 2; i > inputQueue.Count - 1 - dashLeeway; i--)
            {
                float iSign = Mathf.Sign(inputQueue[i].moveX);
                float iAbs = Mathf.Abs(inputQueue[i].moveX);
                
                if(iAbs > 0.5f)
                {
                    if(iSign != sign)
                    {
                        wentOpposite = true;
                    }
                    if(iSign == sign && wentCenter)
                    {
                        triggered = true;
                    }
                }
                if(iAbs < 0.5f)
                {
                    wentCenter = true;
                }

                if (wentOpposite || triggered)
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

    void sendBlock(float moveX, bool wasPressed)
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
