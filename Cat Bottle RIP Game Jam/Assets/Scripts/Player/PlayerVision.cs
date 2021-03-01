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
    [SerializeField]
    private string[] obstructionLayerNames;

    private LayerMask obstructionLayers;
    private MeshFilter filter;
    private IMeshMaskGenerator maskGenerator;

    void Start()
    {
        this.obstructionLayers = LayerMask.GetMask(this.obstructionLayerNames);
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
        this.maskGenerator = new CircleMeshGenerator(this.filter.mesh, obstructionLayers, this.circleVisionRadius);
        this.filter.mesh = this.maskGenerator.GenerateMesh(this.transform.localPosition, this.transform.right, this.visionResolution);
    }

    private void OnEnterLooking()
    {
        this.transform.rotation = Quaternion.identity;
        this.maskGenerator = new ConeMeshGenerator(this.filter.mesh, obstructionLayers, this.coneVisionWidth, this.coneVisionHeight);
        this.filter.mesh = this.maskGenerator.GenerateMesh(this.transform.localPosition, this.states.GetLookDirection(), this.visionResolution);
    }
}
