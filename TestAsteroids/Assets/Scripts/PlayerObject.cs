using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        if (transform.position.y > screenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, -screenBounds.y);
        }
    }
}
