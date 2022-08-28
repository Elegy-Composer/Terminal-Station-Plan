using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

[ExecuteAlways]
public class CharacterCollider : MonoBehaviour
{
    public CustomCollider2D customCollider;
    public Grid grid;
    public float scale = 0.3f;

    private List<Vector2> LRCollider;
    private List<Vector2> UDCollider;

    void Start()
    {
        if (!Application.IsPlaying(gameObject)) return;
        createColliders();
        useColliders(LRCollider, UDCollider);
    }

    void Update()
    {
        if (Application.IsPlaying(gameObject)) return;
        createColliders();
        useColliders(LRCollider, UDCollider);
    }

    private void createColliders()
    {
        float x = grid.cellSize.x * scale;
        float y = grid.cellSize.y * scale;

        LRCollider = createPolygon(x, y);
        UDCollider = createPolygon(-x, y); //flipped on x axis
    }

    private List<Vector2> createPolygon(float x, float y)
    {
        Vector2 offset = new Vector2(x / 2, y / 2);
        float dy = 0.001f;
        return new List<Vector2>
        {
            new Vector2(x, 0) - offset,
            new Vector2(0, y) - offset,
            new Vector2(x*dy/y, y+dy) - offset,
            new Vector2(x+x*dy/y, dy) - offset
        };
    }

    public void UseLRCollider()
    {
        useColliders(LRCollider);
    }

    public void UseUDCollider()
    {
        useColliders(UDCollider);
    }

    public void UseAllColliders()
    {
        useColliders(LRCollider, UDCollider);
    }

    private void useColliders(params List<Vector2>[] colliders)
    {
        PhysicsShapeGroup2D shapeGroup = new PhysicsShapeGroup2D();
        foreach (List<Vector2> vertices in colliders)
        {
            shapeGroup.AddPolygon(vertices);
        }
        customCollider.SetCustomShapes(shapeGroup);
    }
}
