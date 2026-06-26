using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AABB_Box footCollider;
    [SerializeField] AABB_Box posXCollider;
    [SerializeField] AABB_Box posZCollider;
    [SerializeField] AABB_Box negaXCollider;
    [SerializeField] AABB_Box negaZCollider;

    public List<AABB_Box> floors;
    public List<AABB_Box> walls;



    //Might do 5 Colliders hardcoding instead if things get confusing
    //if so must have an array of all floors and walls and just use isoverlapping instead
    //because right now the collision manager is killing everything




    public float speed;
    public float gravity;
    public float gravityMultiplier;
    public float jumpforce;

    public bool isGrounded;
    [SerializeField] float verticalVelocity;
    [SerializeField] float XVelocity;
    [SerializeField] float ZVelocity;


    void OnEnable()
    {
        floors = Object.FindObjectsByType<AABB_Box>(FindObjectsSortMode.None).Where(f => f.CompareTag("Floor")).ToList();
        walls = Object.FindObjectsByType<AABB_Box>(FindObjectsSortMode.None).Where(f => f.CompareTag("Wall")).ToList();
    }

    void Update()
    {
        GroundCheck();
        Gravity();
        Movement();
        Vector3 move = new Vector3(XVelocity, 0f, ZVelocity);

        // Movement();

        Jump();
        transform.position += Vector3.up * verticalVelocity * Time.deltaTime;


        transform.position += move * speed * Time.deltaTime;

    }

    void GroundCheck()
    {
        // isGrounded = footCollider.Overlaps(footCollider.Bounds,posXCollider.Bounds);
        isGrounded = false;

        foreach (AABB_Box floor in floors)
        {
            if (footCollider.Overlaps(footCollider.Bounds, floor.Bounds))
            {
                isGrounded = true;
                break;
            }
        }
    }


    void Gravity()
    {
        if (!isGrounded)
        {
            verticalVelocity -= (gravity * gravityMultiplier) * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0f;
        }

    }

    void Jump()
    {
        if (isGrounded && Input.GetButton("Jump"))
        {
            verticalVelocity = jumpforce;
        }

    }

    void Movement()
    {
        XVelocity = Input.GetAxis("Horizontal");
        ZVelocity = Input.GetAxis("Vertical");

        foreach (AABB_Box wall in walls)
        {
            if (posXCollider.Overlaps(posXCollider.Bounds, wall.Bounds) && XVelocity > 0)
                XVelocity = 0;

            if (negaXCollider.Overlaps(negaXCollider.Bounds, wall.Bounds) && XVelocity < 0)
                XVelocity = 0;

            if (posZCollider.Overlaps(posZCollider.Bounds, wall.Bounds) && ZVelocity > 0)
                ZVelocity = 0;

            if (negaZCollider.Overlaps(negaZCollider.Bounds, wall.Bounds) && ZVelocity < 0)
                ZVelocity = 0;
        }
    }


}
