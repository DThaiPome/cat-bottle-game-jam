using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IPlayerStateMachine))]
public class PlayerShape : MonoBehaviour
{
    private IPlayerStateMachine states;

    void Start()
    {
        this.states = this.GetComponent<IPlayerStateMachine>();
        this.states.OnStateEnter(this.OnEnterLooking, PlayerState.Looking);
        this.states.OnStateStart(this.OnEnterLooking, PlayerState.Looking);
    }
    
    void OnEnterLooking()
    {
        Vector2 direction = this.states.GetLookDirection();
        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector2.Perpendicular(direction));
    }
}
