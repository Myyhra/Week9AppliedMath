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

/* [Header("Movement")]
    public float progress;
    public float zPos = 20f;
    public float speed = 5f;

    public float damage = 5f;

    public int lane;
    public float laneWidth;

    public float horizonY;
    public float groundY;

    public float minScale = 0.1f;
    public float maxScale = 1f;

    public float spawnZ;


    [SerializeField] float hitDistance = 0.5f;
    [SerializeField] float jumpHeight = 1.5f;

    Player2D player;
    IDamageable2D damagePlayer;

    Scaler scaler;
    SpriteRenderer spriteRenderer;

    Vector3 originalScale;

    void Start()
    {
        scaler = FindAnyObjectByType<Scaler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindAnyObjectByType<Player2D>();
        damagePlayer = player.GetComponent<IDamageable2D>();

        originalScale = transform.localScale;
        spriteRenderer.color = GetRandomColor();
        progress = 0f;
    }
    void Update()
    {
        progress += speed * Time.deltaTime;

        UpdatePerspective();
        CheckCollision();
        SetObjectActive();
    }
    void UpdatePerspective()
    { */
/* float perspective = scaler.focalLength / (scaler.focalLength + zPos);
float x = (lane - 1) * laneWidth;
float t = 1f - (zPos / spawnZ);

Vector3 pos;

pos.x = x * perspective;
pos.y = Mathf.Lerp(horizonY, groundY, t);
pos.z = 1;

transform.position = pos;

transform.localScale = originalScale * perspective; */
/* float scale = Mathf.Lerp(minScale, maxScale, progress);

float x = (lane - 1) * laneWidth * scale;

float y = Mathf.Lerp(horizonY, groundY, progress);

transform.position = new Vector3(x, y, 0);
transform.localScale = originalScale * scale; */
/*     }
    void CheckCollision()
    {
        bool sameLane = lane == player.CurrentLane;

        bool closeEnough =
            Mathf.Abs(zPos - player.playerZ) <= hitDistance;

        bool jumping =
            player.transform.position.y > jumpHeight;

        if (sameLane && closeEnough && !jumping)
        {
            damagePlayer.Damage(damage);

            Destroy(gameObject);
        }
    } */
