using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeConeMesh : MonoBehaviour
{
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
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
        this.maskGenerator = new ConeMeshGenerator(this.filter.mesh, 0, this.width, this.height);
        this.maskGenerator.GenerateMesh(this.transform.position, this.transform.right, this.resolution);
    }
}
