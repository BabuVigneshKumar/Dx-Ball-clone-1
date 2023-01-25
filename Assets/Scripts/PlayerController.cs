using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float[] angles = { -60, -45, 0, 45, 60 };
    private Vector3 originalScale;
    Ball ball;
    Rigidbody2D rb;
    public bool isMagnet = false;
    private void Start()
    {

        //ball = GameObject.Find("Ball").GetComponent<Ball>();
        ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();
        originalScale = transform.localScale;
    }
    private void Update()
    {
        //Mouse Controls
        float mousex = Input.GetAxis("Mouse X");
        //Float value - local pos,Min - 1 , Max - 21  
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + mousex, 1, 21), 0.64f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Used To Add Extra Life Value
        if (collision.gameObject.CompareTag("ExtraLife"))
        {
            Debug.Log("Lifeee.......");
            GameManager.instance.UpdateLife(1);

        }
        //USed to Slow the Ball 
        if (collision.gameObject.CompareTag("SlowBall"))
        {
           
             Debug.Log("Slowwwww.......");
            //Time.timeScale = 0.5f;
            //ball.GetComponent<Rigidbody2D>();
           
            ball.Slowball();
        }
        if (collision.gameObject.CompareTag("Magnet"))
        {
            Debug.Log("Magnet.......");
            isMagnet= true;

        }
        if (collision.gameObject.CompareTag("Danger"))
        {
            
            Debug.Log("Danger.......");
            GameManager.instance.UpdateLife(-1);
        }
    
        if (collision.gameObject.CompareTag("BigPaddle"))
        {
            Debug.Log("Bigger.......");
            transform.localScale = originalScale * 2;
   
        }
        if (collision.gameObject.CompareTag("SmallerPaddle"))
        {
            Debug.Log("Smaller.......");
            transform.localScale = originalScale / 2;

        }
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Debug.Log("Fireball");

            

        }




    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball Dir change");

            int randomAngle = Random.Range(0, angles.Length);
            Vector3 bar = collision.transform.eulerAngles;
            bar.z = angles[randomAngle];
            collision.transform.eulerAngles = bar;
          



        }
    }
}






//Vector2 tempVect = new Vector2(collision.rigidbody.velocity.x * -1, collision.rigidbody.velocity.y * -1);
//collision.rigidbody.velocity = tempVect;

