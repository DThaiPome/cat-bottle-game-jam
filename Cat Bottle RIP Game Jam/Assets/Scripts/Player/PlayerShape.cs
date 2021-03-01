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
        float angle;
        if (direction.Equals(Vector2.right))
        {
            angle = 0;
        } else if (direction.Equals(Vector2.up))
        {
            angle = 45;
        } else if (direction.Equals(Vector2.left))
        {
            angle = 90;
        } else if (direction.Equals(Vector2.down))
        {
            angle = 135;
        } else
        {
            angle = 0;
        }
        this.transform.eulerAngles = Vector3.forward * angle;
    }
}
