using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeshMaskGenerator
{
    /// <summary>
    /// Generate a mesh used as a mask to apply renderer post-processing.
    /// </summary>
    /// <param name="origin">position of the mesh's origin</param>
    /// <param name="direction">where the mesh is facing</param>
    /// <param name="resolution">higher resolution means more verticies in the mesh, minimum is 3</param>
    /// <returns></returns>
    Mesh GenerateMesh(Vector2 origin, Vector2 offset, Vector2 direction, int resolution);
}
