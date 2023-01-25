using UnityEngine;

public class Magnet : MonoBehaviour
{
  
 
    public float duration;
    public float timer;
    public GameObject ball;
    public float strength;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Magnet"))
        {
            timer = duration;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            // Calculate the direction and distance between the ball and the paddle
            Vector3 direction = (transform.position - ball.transform.position).normalized;
            float distance = Vector3.Distance(transform.position, ball.transform.position);

            // Apply a force to the ball to pull it towards the paddle
            ball.GetComponent<Rigidbody2D>().AddForce(direction * strength / distance);
            timer -= Time.deltaTime;
        }
    }
}
