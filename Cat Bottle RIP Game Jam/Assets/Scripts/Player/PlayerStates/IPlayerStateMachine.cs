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

    void OnStandingUpdate(Action func);
    void OnLookingUpdate(Action func);
    void OnRollingUpdate(Action func);
}
