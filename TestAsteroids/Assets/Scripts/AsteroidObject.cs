using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidObject : MonoBehaviour, IPooledObject
{
    float r;
    AudioSource audioSource;
    void Start()
    {
        r = GetComponent<CircleCollider2D>().radius;
        audioSource = GetComponent<AudioSource>();
    }
    void LateUpdate()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (transform.position.y - r > screenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, -(screenBounds.y) - r);
        }
        else if (transform.position.y + r < -screenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, screenBounds.y + r);
        }
        else if (transform.position.x - r > screenBounds.x)
        {
            transform.position = new Vector2(-(screenBounds.x) - r, transform.position.y);
        }
        else if (transform.position.x + r < -screenBounds.x)
        {
            transform.position = new Vector2((screenBounds.x) + r, transform.position.y);
        }
    }

    public void OnObjectSpawn()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            audioSource.Play();
        }
    }

}

