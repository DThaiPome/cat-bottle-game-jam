using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private CameraBounds cameraBounds;

    private ICameraBounds bounds;
    private Camera thisCamera;
    private GameObject cat;

    void Start()
    {
        this.bounds = this.cameraBounds;
        this.thisCamera = this.GetComponent<Camera>();
        this.cat = GameObject.FindGameObjectWithTag("Player");
    }

    private Vector2 GetMaxBounds()
    {
        if (this.bounds == null)
        {
            return Vector2.zero;
        } else
        {
            Vector2 bound = this.bounds.getMaxBound();
            Vector2 boundWithDim = bound - this.GetHalfDimVector();
            Vector2 center = this.bounds.getCenter();
            float boundedX = Mathf.Clamp(boundWithDim.x, center.x, bound.x);
            float boundedY = Mathf.Clamp(boundWithDim.y, center.y, bound.y);
            return new Vector2(boundedX, boundedY);
        }
    }

    private Vector2 GetMinBounds()
    {
        if (this.bounds == null)
        {
            return Vector2.zero;
        }
        else
        {
            Vector2 bound = this.bounds.getMinBound();
            Vector2 boundWithDim = bound + this.GetHalfDimVector();
            Vector2 center = this.bounds.getCenter();
            float boundedX = Mathf.Clamp(boundWithDim.x, bound.x, center.x);
            float boundedY = Mathf.Clamp(boundWithDim.y, bound.y, center.y);
            return new Vector2(boundedX, boundedY);
        }
    }

    private Vector2 GetHalfDimVector()
    {
        float height = this.thisCamera.orthographicSize;
        float width = this.thisCamera.aspect * height;
        return new Vector2(width, height);
    }

    private Vector3 GetCatPosition()
    {
        return this.cat == null ? Vector3.zero : new Vector3(this.cat.transform.position.x, this.cat.transform.position.y, this.transform.position.z);
    }

    void Update()
    {
        this.transform.position = this.GetCatPosition();
        this.transform.position = this.ClampPosition();
    }

    private Vector3 ClampPosition()
    {
        Vector2 maxBounds = this.GetMaxBounds();
        Vector2 minBounds = this.GetMinBounds();

        float maxX = maxBounds.x;
        float minX = minBounds.x;
        float maxY = maxBounds.y;
        float minY = minBounds.y;

        float currentX = this.transform.position.x;
        float currentY = this.transform.position.y;

        float newX = Mathf.Clamp(currentX, minX, maxX);
        float newY = Mathf.Clamp(currentY, minY, maxY);

        return new Vector3(newX, newY, this.transform.position.z);
    }
}
