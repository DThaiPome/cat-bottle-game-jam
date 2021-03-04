using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, IPlayerStateMachine
{
    [SerializeField]
    private PlayerState initialState;
    [SerializeField]
    private Vector2 initialLookDirection = Vector2.up;
    [SerializeField]
    private Vector2 initialRollDirection = Vector2.right;
    [SerializeField]
    private string[] collisionLayerNames;

    private PlayerState state;
    private Vector2 lookDirection;
    private Vector2 rollDirection;

    private event Action standingUpdate;
    private event Action lookingUpdate;
    private event Action rollingUpdate;

    private event Action standingEnter;
    private event Action lookingEnter;
    private event Action rollingEnter;

    private event Action standingExit;
    private event Action lookingExit;
    private event Action rollingExit;

    private event Action standingStart;
    private event Action lookingStart;
    private event Action rollingStart;

    private LayerMask collisionLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        this.EnterState(this.initialState);
        this.lookDirection = this.initialLookDirection;
        this.rollDirection = this.initialRollDirection;

        this.StartState();

        this.collisionLayerMask = LayerMask.GetMask(this.collisionLayerNames);
    }

    private void StartState()
    {
        switch (this.state)
        {
            case PlayerState.Standing:
                if (this.standingStart != null)
                {
                    this.standingStart();
                }
                break;
            case PlayerState.Looking:
                if (this.lookingStart != null)
                {
                    this.lookingStart();
                }
                break;
            case PlayerState.Rolling:
                if (this.rollingStart != null)
                {
                    this.rollingStart();
                }
                break;
            default:
                return;
        }
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
        // See what is on the neighboring tiles.
        float tileSize = TileCoordinates.UnitsToTiles(1) * 0.45f;
        RaycastHit2D rightHit = Physics2D.CircleCast(this.transform.position, tileSize, Vector2.right, tileSize, this.collisionLayerMask);
        RaycastHit2D upHit = Physics2D.CircleCast(this.transform.position, tileSize, Vector2.up, tileSize, this.collisionLayerMask);
        RaycastHit2D leftHit = Physics2D.CircleCast(this.transform.position, tileSize, Vector2.left, tileSize, this.collisionLayerMask);
        RaycastHit2D downHit = Physics2D.CircleCast(this.transform.position, tileSize, Vector2.down, tileSize, this.collisionLayerMask);

        bool rightHitExists = rightHit.collider != null;
        bool upHitExists = upHit.collider != null;
        bool leftHitExists = leftHit.collider != null;
        bool downHitExists = downHit.collider != null;

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !rightHitExists)
        {
            this.lookDirection = Vector2.right;
            this.EnterState(PlayerState.Looking);
        } else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !upHitExists)
        {
            this.lookDirection = Vector2.up;
            this.EnterState(PlayerState.Looking);
        } else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !leftHitExists)
        {
            this.lookDirection = Vector2.left;
            this.EnterState(PlayerState.Looking);
        } else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !downHitExists)
        {
            this.lookDirection = Vector2.down;
            this.EnterState(PlayerState.Looking);
        }
    }

    private void TransitionFromLooking()
    {
        if (this.lookDirection.Equals(Vector2.right)) 
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                this.EnterState(PlayerState.Standing);
            } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                this.rollDirection = Vector2.up;
                this.EnterState(PlayerState.Rolling);
            } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                this.rollDirection = Vector2.down;
                this.EnterState(PlayerState.Rolling);
            }
        } else if (this.lookDirection.Equals(Vector2.up))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                this.EnterState(PlayerState.Standing);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                this.rollDirection = Vector2.left;
                this.EnterState(PlayerState.Rolling);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                this.rollDirection = Vector2.right;
                this.EnterState(PlayerState.Rolling);
            }
        } else if (this.lookDirection.Equals(Vector2.left))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                this.EnterState(PlayerState.Standing);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                this.rollDirection = Vector2.up;
                this.EnterState(PlayerState.Rolling);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                this.rollDirection = Vector2.down;
                this.EnterState(PlayerState.Rolling);
            }
        } else if (this.lookDirection.Equals(Vector2.down))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                this.EnterState(PlayerState.Standing);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                this.rollDirection = Vector2.right;
                this.EnterState(PlayerState.Rolling);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                this.rollDirection = Vector2.left;
                this.EnterState(PlayerState.Rolling);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.state == PlayerState.Rolling && other.gameObject.CompareTag("Wall"))
        {
            this.EnterState(PlayerState.Standing);
        }
    }

    private void TransitionFromRolling()
    {
        // the on trigger transitions state instead
    }

    private void EnterState(PlayerState state)
    {
        switch (this.state)
        {
            case PlayerState.Standing:
                if (this.standingExit != null)
                {
                    this.standingExit();
                }
                break;
            case PlayerState.Looking:
                if (this.lookingExit != null)
                {
                    this.lookingExit();
                }
                break;
            case PlayerState.Rolling:
                if (this.rollingExit != null)
                {
                    this.rollingExit();
                }
                break;
            default:
                return;
        }
        this.state = state;
        switch(state)
        {
            case PlayerState.Standing:
                if (this.standingEnter != null)
                {
                    this.standingEnter();
                }
                break;
            case PlayerState.Looking:
                if (this.lookingEnter != null)
                {
                    this.lookingEnter();
                }
                break;
            case PlayerState.Rolling:
                if (this.rollingEnter != null)
                {
                    this.rollingEnter();
                }
                break;
            default:
                return;
        }
    }

    public PlayerState GetPlayerState()
    {
        return this.state;
    }

    public Vector2 GetLookDirection()
    {
        return this.lookDirection;
    }

    public void ChangeDirection(Vector2 direction)
    {
        this.rollDirection = direction;
    }

    public Vector2 GetRollDirection()
    {
        return this.rollDirection;
    }

    public void OnStateEnter(Action func, PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Standing:
                this.standingEnter += func;
                break;
            case PlayerState.Looking:
                this.lookingEnter += func;
                break;
            case PlayerState.Rolling:
                this.rollingEnter += func;
                break;
            default:
                return;
        }
    }

    public void OnStateUpdate(Action func, PlayerState state)
    {
        switch(state)
        {
            case PlayerState.Standing:
                this.OnStandingUpdate(func);
                break;
            case PlayerState.Looking:
                this.OnLookingUpdate(func);
                break;
            case PlayerState.Rolling:
                this.OnRollingUpdate(func);
                break;
            default:
                return;
        }
    }

    public void OnStateExit(Action func, PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Standing:
                this.standingExit += func;
                break;
            case PlayerState.Looking:
                this.lookingExit += func;
                break;
            case PlayerState.Rolling:
                this.rollingExit += func;
                break;
            default:
                return;
        }
    }

    public void OnStateStart(Action func, PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Standing:
                this.standingStart += func;
                break;
            case PlayerState.Looking:
                this.lookingStart += func;
                break;
            case PlayerState.Rolling:
                this.rollingStart += func;
                break;
            default:
                return;
        }
    }

    private void OnStandingUpdate(Action func)
    {
        this.standingUpdate += func;
    }

    private void OnLookingUpdate(Action func)
    {
        this.lookingUpdate += func;
    }

    private void OnRollingUpdate(Action func)
    {
        this.rollingUpdate += func;
    }
}
