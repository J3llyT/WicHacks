using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public bool isCurrentlyColliding = false;

    [SerializeField]
    float speed = 1f;

    public Vector3 playerPosition = Vector3.zero;

    public Vector3 direction = Vector3.right;

    [SerializeField]
    Vector3 velocity = Vector3.zero;

    private Vector3 movementInput;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = transform.position;
    }

    void Update()
    {

        Movement();

        //if the object is colliding...
        if (isCurrentlyColliding)
        {
            //change to red
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        //if the object isn't colliding...
        else
        {
            //leave it as its original color
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }


    public void Movement()
    {
        Camera cameraObject = Camera.main;
        float totalCamHeight = cameraObject.orthographicSize;
        float totalCamWidth = totalCamHeight * cameraObject.aspect;

        direction = movementInput;
        velocity = direction * speed * Time.deltaTime;
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


    public void OnMove(InputAction.CallbackContext content)
    {
        movementInput = content.ReadValue<Vector2>();
    }

}
