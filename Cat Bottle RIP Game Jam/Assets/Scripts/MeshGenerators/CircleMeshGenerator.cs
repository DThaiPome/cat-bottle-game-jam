using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMeshGenerator : AMeshMaskGenerator
{
    private float radius;

    public CircleMeshGenerator(Mesh mesh, int obstructionLayer, float radius) : base(mesh, obstructionLayer)
    {
        this.radius = radius;
    }

    protected override Mesh GenerateValidMesh(Vector2 origin, Vector2 direction, int resolution)
    {
        // Resolution is the vertex count here
        Vector3[] verticies = new Vector3[resolution];
        Vector2[] uv = new Vector2[verticies.Length];

        // One extra triangle for each extra vertex above 3
        // Also triangle array size is equal to triangleCount * 3.
        int triangleCount = resolution - 2;
        int[] triangles = new int[triangleCount * 3];

        float angleStep = 360f / resolution;
        for(int i = 0; i < resolution; i++)
        {
            float angle = angleStep * i;
            Vector2 offset = RotateVector(direction, angle);
            Vector2 vertex = offset + origin;
            uv[i] = vertex;
            verticies[i] = vertex;
        }

        for(int i = 0; i < triangleCount; i++)
        {
            int rootIndex = i * 3;

            triangles[rootIndex] = 0;
            triangles[rootIndex + 1] = i + 1;
            triangles[rootIndex + 2] = i + 2;
        }

        this.mesh.vertices = verticies;
        this.mesh.uv = uv;
        this.mesh.triangles = triangles;

        return this.mesh;
    }
}
