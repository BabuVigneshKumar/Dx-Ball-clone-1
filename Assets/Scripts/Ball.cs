using UnityEngine;
using System.Collections;


public class Ball : MonoBehaviour
{

    //public static Ball instance;
    private PlayerController paddle;
    public bool hasStarted = false;
    private Vector2 paddleToBall;
    [SerializeField] float pushX =-6f;
    [SerializeField] float pushY = 10f;
    public float speed = 0.5f;
    Rigidbody2D rb;
    public Transform[] Powerbrick;
    public float radius = 0.47f;




    // Use this for initialization
    void Start()
    {
        
        paddle = GameObject.FindObjectOfType<PlayerController>();
        if (paddle == null)
        {
            Debug.LogError("No player controller found in the scene");
            return;
        }

        paddleToBall= this.transform.position - paddle.transform.position;

        rb = GetComponent<Rigidbody2D>();

            
     
    }

    // Update is called once per frame

    void Update()
    {
        if (!hasStarted)
        {
            LanchBallState();
            LockBallstate();
         }
    }

    public void LanchBallState()
    {
        
        // Wait for a mouse press to launch.
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(pushX, pushY);
       
        }
        //else
        //{
        //    LockBallstate();
        //}
    }

    public void LockBallstate()
    {
        if (!hasStarted)
        {
            Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBall;
            }
    }

    public void Slowball()
    {
        rb.velocity = (rb.velocity - rb.velocity / 2);
        //StartCoroutine(RestoreVelocityAfter(5f));
    }
    public void Fireball(Collision2D collision)
    {
        Physics2D.OverlapCircleAll(collision.contacts[0].point, radius);
    }

    //IEnumerator RestoreVelocityAfter(float time)
    //{
    //    yield return new WaitForSeconds(2f);
    //    //rb.velocity = (rb.velocity + rb.velocity * 2);
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))
        {
            if (hasStarted)
            {
                GameManager.instance.AddScore(collision.gameObject.GetComponent<Brick>().points);
                Debug.Log("<<<<<<<<" + collision.gameObject.GetComponent<Brick>().points);
                Destroy(collision.gameObject);
            }
            ///////// Random Generate PowerUps //////////
            int Randombrick = Random.Range(0, 20);
            if (Randombrick < 50)
            {
                int randomIndex = Random.Range(0, Powerbrick.Length);
                Transform selectedPowerUp = Powerbrick[randomIndex];
                Instantiate(selectedPowerUp, collision.transform.position, Quaternion.identity);
            }

        }

        //IT Shows GameOver Panel & Decrement life value
        if (collision.gameObject.CompareTag("GO"))
        {
            Debug.Log("Gameover");
            GameManager.instance.UpdateLife(-1);

            //GameManager.instance.SaveScore();
        }



        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (collision.collider.GetComponent<PlayerController>().isMagnet)
            {
                hasStarted = false;
                LockBallstate();
                return;
            }
            // Get the normal vector of the collision point
            Vector2 normal = collision.contacts[0].normal;
            // Invert the y-component of the normal vector
            normal.y = -normal.y;
            // Get the reflection vector of the ball's velocity
            Vector2 reflectedVelocity = Vector2.Reflect(rb.velocity, normal);
            reflectedVelocity.y = Mathf.Abs(reflectedVelocity.y);
            Debug.Log("#####" + reflectedVelocity);
            // Set the ball's new velocity
            rb.velocity = reflectedVelocity;

        }
    }
}
    



