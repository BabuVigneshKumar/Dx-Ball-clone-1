using UnityEngine;

public class BAllControllertt : MonoBehaviour
{
    public float speed = 30; // The ball's speed
    public Rigidbody2D rb; // The ball's Rigidbody component
    private bool started; // Whether or not the game has started
    private Vector2 direction; // The direction the ball is moving in
    private bool gameOver;

    void Start()
    {
        // Initialize the ball's direction to be upward
        direction = Vector2.up;

        // initialize gameOver state to false
        gameOver = false;
    }

    void Update()
    {
        if (!started)
        {
            // If the game has not started, the player can move the paddle to set the launch point of the ball
            transform.position = new Vector3(PaddleScript.x, transform.position.y, transform.position.z);

            // wait for the space bar to be pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // launch the ball
                rb.velocity = new Vector2(speed, speed);
                started = true;
            }
        }

        if (gameOver)
        {
            // code to show game over screen
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check for collision with paddle
        if (collision.gameObject.CompareTag("Paddle"))
        {
            //Calculate hit Factor
            float x = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            //Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(x, 1).normalized;
            // Set Velocity with dir * speed
            rb.velocity = dir * speed;
        }
        // check for collision with brick
        if (collision.gameObject.CompareTag("Brick"))
        {
            // update score and remaining bricks
            //ScoreScript.score += 100;
            //ScoreScript.remainingBricks--;
            //// check if all the bricks are destroyed
            //if (ScoreScript.remainingBricks <= 0)
            //{
            //    //code for showing win screen
            //}
            // destroy the brick
            Destroy(collision.gameObject);
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleWidth)
    {
        // ascii art:
        // ||  1 <- at the top of the paddle
        // ||
        // ||  0 <- at the middle of the paddle
        // ||
        // || -1 <- at the bottom of the paddle
        return (ballPos.x - paddlePos.x) / paddleWidth;
    }
}
