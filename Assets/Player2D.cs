using UnityEngine;

public class Player2D : MonoBehaviour,IDamageable2D
{
    [Header("Player")]
    public float playerMaxHP = 100f;
    HP hp;
    IDamageable HPbar;
    [Header("Lane")]
    public float laneOffset = 2f;
    [SerializeField] float laneSpeed = 10f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float gravity = 20f;

    int currentLane = 1;

    float targetX;
    public float groundY;
    float velocityY;
    


    void Start()
    {
        hp = FindAnyObjectByType<HP>();
        HPbar = hp.GetComponent<IDamageable>();
        groundY = transform.position.y;
        targetX = transform.position.x;
    }

    void Update()
    {
        HandleLaneInput();
        MoveToLane();

        HandleJumpInput();
        ApplyGravity();
    }

    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            currentLane--;

        if (Input.GetKeyDown(KeyCode.D))
            currentLane++;

        currentLane = Mathf.Clamp(currentLane, 0, 2);

        targetX = (currentLane - 1) * laneOffset;
    }

    void MoveToLane()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.MoveTowards(
            pos.x,
            targetX,
            laneSpeed * Time.deltaTime);

        transform.position = pos;
    }

    void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            transform.position.y <= groundY + 0.01f)
        {
            velocityY = jumpForce;
        }
    }

    void ApplyGravity()
    {
        Vector3 pos = transform.position;

        velocityY -= gravity * Time.deltaTime;
        pos.y += velocityY * Time.deltaTime;

        if (pos.y < groundY)
        {
            pos.y = groundY;
            velocityY = 0;
        }

        transform.position = pos;
    }



    public void Damage(float damage)
    {
        HPbar.TakeDamage(damage);
    }

    public int CurrentLane
    {
        get { return currentLane; }
    }
}

public interface IDamageable2D
{
    void Damage(float damage);
}
