using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBCharacterController : MonoBehaviour {

    public enum CharacterState
    {
        Idle, Crouch, Block, Hit, Down, DownRecovery, Walk,
        Dash, Jump, JumpHit, JumpDown,
        JumpLight, JumpHeavy,
        Light, LightRecovery, LightConsecutive, LightConsecutiveRecovery,
        HeavyCharge,Heavy,HeavyRecovery
        
    };


    public float walkSpeedForward;
    public float walkSpeedBackward;
    public float dashSpeedForward;
    public float dashSpeedBackward;
    public int dashLeeway;
    public float maxJumpHeight;
    public float maxJumpTime;
    public float fallTime;


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
            //if (inputQueue[inputQueue.Count - 1].ultimateAbility.wasPressed == true)
            //{
            //    anim.SetTrigger("Block");
            //}
            InputState currentInputState = inputQueue[inputQueue.Count - 1];
            switch (currentCharacterState)
            {
                case CharacterState.Idle:
                    sendCrouch(currentInputState.moveY);
                    sendWalk(currentInputState.moveX);
                    sendDash(currentInputState.moveX);
                    break;
                case CharacterState.Crouch:
                    sendCrouch(currentInputState.moveY);
                    sendWalk(currentInputState.moveX);
                    break;
                case CharacterState.Walk:
                    sendCrouch(currentInputState.moveY);
                    sendWalk(currentInputState.moveX);
                    sendDash(currentInputState.moveX);
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
}
