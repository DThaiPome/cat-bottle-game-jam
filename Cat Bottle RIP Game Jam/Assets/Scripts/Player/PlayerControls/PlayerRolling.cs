using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IPlayerStateMachine))]
public class PlayerRolling : MonoBehaviour
{
    [SerializeField]
    // In tiles/sec
    private float rollSpeed = 4;

    private IPlayerStateMachine states;

    void Start()
    {
        this.states = this.GetComponent<IPlayerStateMachine>();
        this.states.OnRollingUpdate(this.OnRollingUpdate);
    }

    private void OnRollingUpdate()
    {
        Vector2 direction = this.states.GetRollDirection();
        Vector3 offset = direction * TileCoordinates.TilesToUnits(this.rollSpeed);
        this.transform.position += offset * Time.deltaTime;
    }
}
