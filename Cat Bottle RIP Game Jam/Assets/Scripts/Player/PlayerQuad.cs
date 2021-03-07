using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlayerQuad : MonoBehaviour
{
    private MeshFilter filter;
    private IMeshMaskGenerator quadGen;
    private Vector2 offset;

    void Awake()
    {
        this.filter = this.GetComponent<MeshFilter>();

        this.filter.mesh = new Mesh();
        this.quadGen = null;
        this.offset = Vector2.zero;
    }

    void Update()
    {
        if (this.quadGen != null)
        {
            this.filter.mesh = this.quadGen.GenerateMesh(this.transform.position, this.offset, Vector2.right, 100);
        }
    }

    public void SetDimensions(float width, float height, Vector2 offset)
    {
        this.quadGen = new QuadMeshGenerator(this.filter.mesh, 0, width, height);
        this.offset = offset;
    }
}
