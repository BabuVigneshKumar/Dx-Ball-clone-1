using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BallController : MonoBehaviour
{
    // Speed at which the ball moves
    public float ballSpeed = 5.0f;

    // Direction in which the ball is moving (initialized to bottom-left)
    private Vector2 ballDirection = new Vector2(-1, -1);

    // Rigidbody component of the ball
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the ball in the specified direction
        rb.velocity = ballDirection * ballSpeed;
    }

    // OnCollisionEnter2D is called when the ball collides with a collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the ball hits the paddle, reverse the Y direction
        if (collision.gameObject.CompareTag("Paddle"))
        {
            ballDirection.y *= -1;
        }
        // If the ball hits the top wall, reverse the X direction
        else if (collision.gameObject.CompareTag("Top Wall"))
        {
            ballDirection.x *= -1;
        }
        if (collision.collider.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }
    }

}


   

