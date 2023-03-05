using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Rigidbody2D playerRB2D;

    private float speed;
    private float jumpForce;
    private bool isJumping;
    private float moveX;
    private float moveY;

    // Start is called before the first frame update
    void Start()
    {
        playerRB2D = gameObject.GetComponent<Rigidbody2D>();

        speed = 2f;
        jumpForce = 50f;
        isJumping = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // detect movement
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // movement
        if(moveX > 0 || moveX < 0)
        {
            playerRB2D.AddForce(new Vector2(moveX * speed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveY > 0)
        {
            playerRB2D.AddForce(new Vector2(0f, moveY * jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // makes sure the player can only jump when standing on the platform
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // makes sure the player can only jump when standing on a platform
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }
}
