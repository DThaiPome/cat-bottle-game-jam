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
        this.states.OnStateUpdate(this.OnRollingUpdate, PlayerState.Rolling);
        this.states.OnStateExit(this.OnRollingExit, PlayerState.Rolling);
    }

    private void OnRollingUpdate()
    {
        Vector2 direction = this.states.GetRollDirection();
        Vector3 offset = direction * TileCoordinates.TilesToUnits(this.rollSpeed);
        this.transform.position += offset * Time.deltaTime;
    }

    private void OnRollingExit()
    {
        // Go to the nearest whole tile.
        Vector2 tilePos = TileCoordinates.UnitPosToTilePos(this.transform.position);
        Vector2 roundedTiles = new Vector2(Mathf.Round(tilePos.x), Mathf.Round(tilePos.y));
        this.transform.position = TileCoordinates.TilePosToUnitPos(roundedTiles);
    }
}
