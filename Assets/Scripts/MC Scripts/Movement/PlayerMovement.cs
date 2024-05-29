using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform jumpPos;
    [SerializeField] private LayerMask layerDetection;
    [SerializeField] private Rigidbody2D playerRB;
    private Animator playerAnimation;
    [SerializeField] private SpriteRenderer playerRenderer;


    [Header("Values")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxDistance;
    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;
    private float xMovement;
    private bool isJumping;
    private bool onGround;
    private bool canLand;


    void Start()
    {
        isJumping = false;
        playerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if(playerRB.velocity.y > 0)
        {
            playerAnimation.SetBool("velocityUp", true);
        }
        else
        {
            playerAnimation.SetBool("velocityUp", false);
        }

        SideMovement();
        Jump();

        /* Sprite related codes */
        if (playerRB.velocity.x > 0)
        {
            playerRenderer.flipX = false;
        }
        if (playerRB.velocity.x < 0)
        {
            playerRenderer.flipX = true;
        }
    }


    void SideMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal");
        float horizontalMovement = xMovement * movementSpeed;
        playerRB.velocity = new Vector2(horizontalMovement, playerRB.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            playerAnimation.SetBool("isMoving", false);
        }
        else
        {
            playerAnimation.SetBool("isMoving", true);
        }
        //playerAnimation.SetFloat("moveSpeed", Mathf.Abs(playerRB.velocity.x));

    }

    void Jump()
    {
        onGround = Physics2D.OverlapCircle(jumpPos.transform.position, maxDistance, layerDetection);

        if (onGround && Input.GetKeyDown(KeyCode.Space)) 
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; 
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce); 
        }

        if (isJumping && (Input.GetKey(KeyCode.Space))) 
        {
            if (jumpTimeCounter > 0)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce); 
                jumpTimeCounter -= Time.deltaTime; 
            }
            else
            {
                isJumping = false; 
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            isJumping = false; 
        }

        if (playerRB.velocity.y < 2 && !canLand)
        {
            canLand = true;
        }

        playerAnimation.SetBool("isGrounded", onGround);
        //playerAnimation.SetFloat("verticalSpeed", playerRB.velocity.y);
        Debug.Log(onGround);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(jumpPos.position, maxDistance);
    }

}

