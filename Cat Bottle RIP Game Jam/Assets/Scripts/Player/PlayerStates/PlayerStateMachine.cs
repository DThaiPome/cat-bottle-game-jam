using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IPlayerStateMachine
{
    [SerializeField]
    private PlayerState initialState;

    private PlayerState state;
    private Vector2 lookDirection;
    private Vector2 rollDirection;

    private event Action standingUpdate;
    private event Action lookingUpdate;
    private event Action rollingUpdate;

    // Start is called before the first frame update
    void Start()
    {
        this.state = initialState;
        this.lookDirection = Vector2.right;
        this.rollDirection = Vector2.up;
}

    // Update is called once per frame
    void Update()
    {
        switch(this.state)
        {
            case PlayerState.Standing:
                if (this.standingUpdate != null)
                {
                    this.standingUpdate();
                }
                this.TransitionFromStanding();
                break;
            case PlayerState.Looking:
                if (this.lookingUpdate != null)
                {
                    this.lookingUpdate();
                }
                this.TransitionFromLooking();
                break;
            case PlayerState.Rolling:
                if (this.rollingUpdate != null)
                {
                    this.rollingUpdate();
                }
                this.TransitionFromRolling();
                break;
            default:
                return;
        }
    }

    private void TransitionFromStanding()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            this.lookDirection = Vector2.right;
            this.state = PlayerState.Looking;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            this.lookDirection = Vector2.up;
            this.state = PlayerState.Looking;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            this.lookDirection = Vector2.left;
            this.state = PlayerState.Looking;
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            this.lookDirection = Vector2.down;
            this.state = PlayerState.Looking;
        }
    }

    private void TransitionFromLooking()
    {
        if (this.lookDirection.Equals(Vector2.right)) 
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                this.state = PlayerState.Standing;
            } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                this.rollDirection = Vector2.up;
                this.state = PlayerState.Rolling;
            } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                this.rollDirection = Vector2.down;
                this.state = PlayerState.Rolling;
            }
        } else if (this.lookDirection.Equals(Vector2.up))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                this.state = PlayerState.Standing;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                this.rollDirection = Vector2.left;
                this.state = PlayerState.Rolling;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                this.rollDirection = Vector2.right;
                this.state = PlayerState.Rolling;
            }
        } else if (this.lookDirection.Equals(Vector2.left))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                this.state = PlayerState.Standing;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                this.rollDirection = Vector2.up;
                this.state = PlayerState.Rolling;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                this.rollDirection = Vector2.down;
                this.state = PlayerState.Rolling;
            }
        } else if (this.lookDirection.Equals(Vector2.down))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                this.state = PlayerState.Standing;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                this.rollDirection = Vector2.right;
                this.state = PlayerState.Rolling;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                this.rollDirection = Vector2.left;
                this.state = PlayerState.Rolling;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.state == PlayerState.Rolling)
        {
            this.state = PlayerState.Standing;
        }
    }

    private void TransitionFromRolling()
    {
        // the on trigger transitions state instead
    }

    public PlayerState GetPlayerState()
    {
        return this.state;
    }

    public Vector2 GetLookDirection()
    {
        return this.lookDirection;
    }

    public Vector2 GetRollDirection()
    {
        return this.rollDirection;
    }

    public void OnStandingUpdate(Action func)
    {
        this.standingUpdate += func;
    }

    public void OnLookingUpdate(Action func)
    {
        this.lookingUpdate += func;
    }

    public void OnRollingUpdate(Action func)
    {
        this.rollingUpdate += func;
    }
}
