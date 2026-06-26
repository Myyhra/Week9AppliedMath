using UnityEngine;

public class Obstacle : MonoBehaviour
{
    SpriteRenderer sprite;
    Scaler scaler;
    public float zPos;
    public float speed;
    Vector2 ogPos;
    public float horizonY = 3f;

    public int lane;

    [Header("Attack")]
    float damage = 5f;
    Player2D player;
    IDamageable2D damage2d;
    [SerializeField] float hitDistance = 1f;
    [SerializeField] float jumpHeight = 1f;
    bool hasHit;

    void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        scaler = FindAnyObjectByType<Scaler>();
        player = FindAnyObjectByType<Player2D>();
        damage2d = player.GetComponent<IDamageable2D>();

    }

    void Start()
    {
        ogPos = transform.position;
        sprite.color = GetRandomColor();
    }

    void Update()
    {
        Move();
        HitPlayer();
        SetObjectActive();
    }

    void Move()
    {
        if (scaler == null) return;

        float scale = scaler.focalLength / (scaler.focalLength + zPos);

        transform.localScale = Vector3.one * scale;

        // transform.position = ogPos * scale;
        float projectedX = ogPos.x * scale;
        float projectedY = Mathf.Lerp(horizonY, ogPos.y, scale);

        transform.position = new Vector2(projectedX, projectedY);

        zPos -= speed * Time.deltaTime;
    }

    void HitPlayer()
    {
        Debug.Log("Checking hit...");
        Debug.Log($"Obstacle Lane: {lane} | Player Lane: {player.CurrentLane}");
        if (hasHit)
            return;

        if (!SameLane())
            return;

        if (!CloseEnough())
            return;

        if (IsJumping())
            return;

        hasHit = true;

        damage2d.Damage(damage);

        Destroy(gameObject);
    }
    bool SameLane()
    {
        return lane == player.CurrentLane;
    }
    bool CloseEnough()
    {
        return zPos <= hitDistance;
    }
    bool IsJumping()
    {
        return player.transform.position.y >
               player.groundY + jumpHeight;
    }




    void SetObjectActive()
    {
        if (zPos <= -1f|| zPos >= scaler.focalLength)
            Destroy(gameObject);
    }
    private Color GetRandomColor()
    {
        var rRand = Random.Range(0f, 1f);
        var gRand = Random.Range(0f, 1f);
        var bRand = Random.Range(0f, 1f);
        return new Color(rRand, gRand, bRand);
    }
}

