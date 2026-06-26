using Unity.VisualScripting;
using UnityEngine;

public class AABB_Box : MonoBehaviour
{
    [Header("Collider Settings")]
    public bool isColliding;
    public float width = 1;
    public float height = 1;
    public float depth = 1;
    public Color debugColor = Color.red;

    private AABB bounds;

    public AABB Bounds => bounds;

    public string gameObjectTag;
    void Start()
    {
        gameObjectTag = this.tag;
    }

    void Update()
    {
        bounds = new AABB(width, height, depth, transform.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, depth));
    }

    public void Collided(AABB bounds)
    {
        isColliding = true;
        Debug.Log($"Notify Collision this {gameObjectTag} is collided to");
        
    }

    public void NotCollided(AABB bounds)
    {
        isColliding = false;
        Debug.Log("Notify No Collision");
    }


    public bool Overlaps(AABB a, AABB b)
    {
        return a.min.x <= b.max.x && b.min.x <= a.max.x &&
               a.min.y <= b.max.y && b.min.y <= a.max.y &&
               a.min.z <= b.max.z && b.min.z <= a.max.z;
    }


    public Vector3 ClosestPoint(AABB b, Vector3 p)
    {
        return new Vector3(
          Mathf.Clamp(p.x, b.min.x, b.max.x),
          Mathf.Clamp(p.y, b.min.y, b.max.y),
          Mathf.Clamp(p.z, b.min.z, b.max.z));
    }

}

[System.Serializable]
public struct AABB
{
    public Vector3 min,
        max;

    public AABB(float width, float height, float depth, Vector3 pos)
    {
        min = new Vector3(pos.x - width * .5f, pos.y - height * .5f, pos.z - depth * .5f);
        max = new Vector3(pos.x + width * .5f, pos.y + height * .5f, pos.z + depth * .5f);
    }
}