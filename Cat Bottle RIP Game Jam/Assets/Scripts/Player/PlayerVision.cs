using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlayerVision : MonoBehaviour
{
    [SerializeField]
    private PlayerStateMachine states;
    [SerializeField]
    private float circleVisionRadius = 4;
    [SerializeField]
    private float coneVisionWidth = 3;
    [SerializeField]
    private float coneVisionHeight = 10;
    [SerializeField]
    private int visionResolution = 100;

    private MeshFilter filter;
    private IMeshMaskGenerator maskGenerator;

    void Start()
    {
        this.filter = this.GetComponent<MeshFilter>();
        this.filter.mesh = new Mesh();
        if (this.states != null)
        {
            this.states.OnStateStart(this.OnEnterStanding, PlayerState.Standing);
            this.states.OnStateEnter(this.OnEnterStanding, PlayerState.Standing);
            this.states.OnStateStart(this.OnEnterLooking, PlayerState.Looking);
            this.states.OnStateEnter(this.OnEnterLooking, PlayerState.Looking);
        } else
        {
            Debug.LogError("No state machine found");
        }
    }

    private void OnEnterStanding()
    {
        this.maskGenerator = new CircleMeshGenerator(this.filter.mesh, 0, this.circleVisionRadius);
        this.maskGenerator.GenerateMesh(this.transform.position, this.transform.right, this.visionResolution);
    }

    private void OnEnterLooking()
    {
        this.maskGenerator = new ConeMeshGenerator(this.filter.mesh, 0, this.coneVisionWidth, this.coneVisionHeight);
        this.maskGenerator.GenerateMesh(this.transform.position, this.transform.right, this.visionResolution);
    }
}
