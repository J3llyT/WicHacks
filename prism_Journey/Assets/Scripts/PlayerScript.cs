using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool isCurrentlyColliding = false;

    public int lives = 5;

    public float invinsibilityTimer;

    public float aliveTimer = 0;
    public int score = 0;

    //---------------------------------------MOVEMENT-----------------------------
    [SerializeField]
    float speed = 1f;

    public Vector3 playerPosition = Vector3.zero;

    public Vector3 direction = Vector3.right;

    [SerializeField]
    Vector3 velocity = Vector3.zero;

    private Vector3 movementInput;

    private float totalCamHeight;
    private float totalCamWidth;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = transform.position;
        invinsibilityTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTimer += Time.deltaTime;

        if (aliveTimer >= 1f)
        {
            ++score;
            aliveTimer = 0;
        }

        Movement();

        if (invinsibilityTimer == 0)
        {
            //if the object is colliding...
            if (isCurrentlyColliding)
            {
                //change to red
                GetComponent<SpriteRenderer>().color = Color.red;
                invinsibilityTimer += Time.deltaTime;
                --lives;
            }
            //if the object isn't colliding...
            else
            {
                //leave it as its original color
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            invinsibilityTimer += Time.deltaTime;

            if (invinsibilityTimer > 3f)
            {
                invinsibilityTimer = 0;
            }
        }

    }


    public void Movement()
    {
        Camera cameraObject = Camera.main;
        float totalCamHeight = cameraObject.orthographicSize;
        float totalCamWidth = totalCamHeight * cameraObject.aspect;

        direction = movementInput;
        //direction = Quaternion.Euler(0, 0, turnAmount * Time.deltaTime) * direction;

        // velocity is direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        // add velocity to the position
        playerPosition += velocity;


        if (playerPosition.x < -(totalCamWidth))
        {
            playerPosition = new Vector3(-(totalCamWidth), playerPosition.y, playerPosition.z);
        }
        if (playerPosition.x > totalCamWidth)
        {
            playerPosition = new Vector3(totalCamWidth, playerPosition.y, playerPosition.z);
        }
        if (playerPosition.y < -(totalCamHeight))
        {
            playerPosition = new Vector3(playerPosition.x, totalCamHeight, playerPosition.z);
        }
        if (playerPosition.y > totalCamHeight)
        {
            playerPosition = new Vector3(playerPosition.x, -(totalCamHeight), playerPosition.z);
        }

        // "draw" this player at this position
        transform.position = playerPosition;



    }


    public bool AABBCollision(GameObject otherObject)
    {
        //gets the bounds of two objects
        Bounds thisObjectBounds = this.GetComponent<SpriteRenderer>().bounds;
        Bounds otherObjectBounds = otherObject.GetComponent<SpriteRenderer>().bounds;

        //if their bounds are overlapping...
        if ((thisObjectBounds.min.x < otherObjectBounds.max.x) && (thisObjectBounds.max.x > otherObjectBounds.min.x)
            && (thisObjectBounds.min.y < otherObjectBounds.max.y) && (thisObjectBounds.max.y > otherObjectBounds.min.y))
        {
            //they are colliding
            return true;
        }

        return false;

    }


    // public void OnMove(InputAction.CallbackContext content)
    // {
    //    movementInput = content.ReadValue<Vector2>();
    //}

}
