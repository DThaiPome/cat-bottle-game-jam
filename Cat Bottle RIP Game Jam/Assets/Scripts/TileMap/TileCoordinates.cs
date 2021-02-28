using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinates
{
    // Square size of tiles in units
    private static float tileSize = 1;

    public static void SetTileSize(float size)
    {
        tileSize = size;
    }

    public static float UnitsToTiles(float units)
    {
        return units / tileSize;
    }

    public static float TilesToUnits(float tiles)
    {
        return tiles * tileSize;
    }

    public static Vector2 UnitPosToTilePos(Vector2 units)
    {
        return units / tileSize;
    }

    public static Vector2 TilePosToUnitPos(Vector2 tiles)
    {
        return tiles * tileSize;
    }
}
