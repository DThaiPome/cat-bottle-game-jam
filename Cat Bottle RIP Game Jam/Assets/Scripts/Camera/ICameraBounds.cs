using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraBounds
{
    Vector2 getCenter();
    Vector2 getMinBound();
    Vector2 getMaxBound();
}
