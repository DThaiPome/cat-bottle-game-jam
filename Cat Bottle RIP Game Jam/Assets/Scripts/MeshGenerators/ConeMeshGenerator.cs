using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMeshGenerator : AMeshMaskGenerator
{
    private float width;
    private float height;

    public ConeMeshGenerator(Mesh mesh, int obstructionLayer, float width, float height) : base(mesh, obstructionLayer)
    {
        this.width = width;
        this.height = height;
    }

    protected override Mesh GenerateValidMesh(Vector2 origin, Vector2 offset, Vector2 direction, int resolution)
    {
        this.mesh = new Mesh();

        // Resolution is the vertex count here
        Vector3[] verticies = new Vector3[resolution];
        Vector2[] uv = new Vector2[verticies.Length];

        // One extra triangle for each extra vertex above 3
        // Also triangle array size is equal to triangleCount * 3.
        int triangleCount = resolution - 2;
        int[] triangles = new int[triangleCount * 3];

        // Verticies that always exist
        Vector2 leftEdge = this.width * RotateVector(direction, 90) + this.height * direction;
        Vector2 rightEdge = this.width * RotateVector(direction, -90) + this.height * direction;

        float edgeDistance = leftEdge.magnitude;

        leftEdge.Normalize();
        rightEdge.Normalize();

        verticies[0] = offset;
        uv[0] = verticies[0];
        verticies[1] = GetPointAtObstruction(origin, leftEdge, edgeDistance, obstructionLayer) + offset;
        uv[1] = verticies[1];
        verticies[verticies.Length - 1] = GetPointAtObstruction(origin, rightEdge, edgeDistance, obstructionLayer) + offset;
        uv[verticies.Length - 1] = verticies[verticies.Length - 1];
        int remainingVerticies = resolution - 3;

        if (remainingVerticies != 0)
        {
            float totalAngle = Vector2.Angle(leftEdge, rightEdge);
            float angleStep = totalAngle / remainingVerticies;
            for(int i = 0; i < remainingVerticies; i++)
            {
                int vertexIndex = i + 2;
                float angle = (i + 1) * angleStep;
                Vector2 iDirection = RotateVector(leftEdge, -angle);
                Vector3 vertex = GetPointAtObstruction(origin, iDirection, edgeDistance, obstructionLayer) + offset;

                verticies[vertexIndex] = vertex;
                uv[vertexIndex] = vertex;
            }
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
