using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    Vector3 max;
    Vector3 min;
    void Start()
    {
        max = GetComponent<Collider2D>().bounds.max;
        min = GetComponent<Collider2D>().bounds.min;
    }

    void LateUpdate()
    { 
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        if (transform.position.y - max.y > screenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, -(screenBounds.y) - max.y);
        }
        else if (transform.position.y + max.y < -screenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, screenBounds.y + max.y);
        }
        else if (transform.position.x - max.y > screenBounds.x)
        {
            transform.position = new Vector2(-(screenBounds.x) - max.y,transform.position.y);
        }
        else if (transform.position.x + max.y < -screenBounds.x)
        {
            transform.position = new Vector2((screenBounds.x) + max.y, transform.position.y);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
