using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class powerup : MonoBehaviour
{
    public float speed;

   
    void Update()
    {
        transform.Translate(new Vector2(0f, -1f)*Time.deltaTime*speed);
        if (transform.position.y < 15f)
        {
            Destroy(gameObject);
        }
  

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Destroy(this.gameObject);
        }

       
    }

}
