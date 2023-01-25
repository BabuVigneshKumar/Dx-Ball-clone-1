using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed = 10; // The paddle's speed
    public static float x; // The x position of the paddle (static variable is accessible by other scripts)

  
    void Update()
    {
        // Get the horizontal input axis (-1 for left, 1 for right)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Move the paddle left or right
        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        // Clamp the paddle's position to the screen
        x = Mathf.Clamp(transform.position.x, -8, 8);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}