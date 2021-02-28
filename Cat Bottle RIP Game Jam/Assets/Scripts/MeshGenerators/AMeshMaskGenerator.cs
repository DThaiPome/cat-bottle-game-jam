using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMeshMaskGenerator : IMeshMaskGenerator
{
    // The mesh to modify
    protected Mesh mesh;
    // Objects on this layer should obstruct the mesh.
    protected int obstructionLayer;

    public AMeshMaskGenerator(Mesh mesh, int obstructionLayer)
    {
        this.mesh = mesh;
        this.obstructionLayer = obstructionLayer;
    }

    // Ensure the resolution is min 3 and the direction is not the 0 vector.
    // Also ensures the direction is a unit vector.
    public Mesh GenerateMesh(Vector2 origin, Vector2 direction, int resolution)
    {
        if (resolution < 3 || direction.Equals(Vector2.zero))
        {
            Debug.LogError("Improper parameters for mesh: direction is " + direction + ", resolution is " + resolution);
            return new Mesh();
        } else
        {
            return this.GenerateValidMesh(origin, direction.normalized, resolution);
        }
    }

    // Do this once the inputs are validated.
    protected abstract Mesh GenerateValidMesh(Vector2 origin, Vector2 direction, int resolution);

    // Other util stuff
    protected static Vector2 RotateVector(Vector2 v, float degrees)
    {
        float radians = Mathf.Deg2Rad * degrees;
        float newX = (v.x * Mathf.Cos(radians)) + (v.y * -Mathf.Sin(radians));
        float newY = (v.x * Mathf.Sin(radians)) + (v.y * Mathf.Cos(radians));
        return new Vector2(newX, newY);
    }
}
