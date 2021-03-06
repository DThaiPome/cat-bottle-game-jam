using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraBounds : MonoBehaviour, ICameraBounds
{
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Awake()
    {
        this.boxCollider = this.GetComponent<BoxCollider2D>();
    }

    public Vector2 getCenter()
    {
        return this.boxCollider.offset - (Vector2)this.transform.localPosition;
    }

    private Vector2 getBound(bool max)
    {
        int mul = max ? 1 : -1;
        Vector2 center = this.getCenter();
        float x = center.x + (this.boxCollider.size.x / (2 * mul));
        float y = center.y + (this.boxCollider.size.y / (2 * mul));
        return new Vector2(x, y);
    }

    public Vector2 getMaxBound()
    {
        return this.getBound(true);
    }

    public Vector2 getMinBound()
    {
        return this.getBound(false);
    }
}
