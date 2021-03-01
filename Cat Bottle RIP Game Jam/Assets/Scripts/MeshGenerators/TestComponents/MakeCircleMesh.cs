using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MakeCircleMesh : MonoBehaviour
{
    [SerializeField]
    private int radius = 4;
    [SerializeField]
    private int resolution = 50;

    private MeshFilter filter; 
    private IMeshMaskGenerator maskGenerator;

    void Start()
    {
        this.filter = this.GetComponent<MeshFilter>();
        this.filter.mesh = new Mesh();
        this.GetMask();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetMask();
        }
    }

    private void GetMask()
    {
        this.maskGenerator = new CircleMeshGenerator(this.filter.mesh, 0, this.radius);
        this.maskGenerator.GenerateMesh(this.transform.position, Vector2.zero, this.transform.right, this.resolution);
    }
}
