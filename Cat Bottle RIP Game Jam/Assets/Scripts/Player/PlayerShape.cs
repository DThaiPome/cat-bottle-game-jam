using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IPlayerStateMachine))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerShape : MonoBehaviour
{
    [SerializeField]
    private PlayerQuad quad;

    private IPlayerStateMachine states;
    private BoxCollider2D collider;
    private Vector2 baseSize;
    private Vector2 baseOffset;

    void Start()
    {
        this.states = this.GetComponent<IPlayerStateMachine>();
        this.states.OnStateEnter(this.OnEnterLooking, PlayerState.Looking);
        this.states.OnStateStart(this.OnEnterLooking, PlayerState.Looking);
        this.states.OnStateEnter(this.OnEnterStanding, PlayerState.Standing);
        this.states.OnStateStart(this.OnEnterStanding, PlayerState.Standing);

        this.collider = this.GetComponent<BoxCollider2D>();
        this.baseSize = this.collider.size;
        this.baseOffset = this.collider.offset;
        this.UpdateQuad();
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
            angle = 90;
        } else if (direction.Equals(Vector2.left))
        {
            angle = 180;
        } else if (direction.Equals(Vector2.down))
        {
            angle = 270;
        } else
        {
            angle = 0;
        }
        this.transform.eulerAngles = Vector3.forward * angle;

        this.collider.size = new Vector2(this.baseSize.x * 2, this.baseSize.y);
        this.collider.offset += Vector2.right * 0.5f;

        this.UpdateQuad();
    }

    void OnEnterStanding()
    {
        this.collider.size = this.baseSize;
        this.collider.offset = this.baseOffset;
        this.UpdateQuad();
    }

    private void UpdateQuad()
    {
        this.quad.SetDimensions(this.collider.size.x, this.collider.size.y, this.collider.offset);
    }
}
