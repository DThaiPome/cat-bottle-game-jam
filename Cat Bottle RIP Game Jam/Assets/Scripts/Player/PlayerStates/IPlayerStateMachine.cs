using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum PlayerState
{
    Standing,
    Looking,
    Rolling
}

public interface IPlayerStateMachine
{
    PlayerState GetPlayerState();
    Vector2 GetLookDirection();
    Vector2 GetRollDirection();
    void ChangeDirection(Vector2 direction);
    void ChangeState(PlayerState newState);

    void OnStateEnter(Action func, PlayerState state);

    void OnStateUpdate(Action func, PlayerState state);

    void OnStateStart(Action func, PlayerState state);

    void OnStateExit(Action func, PlayerState state);
}
