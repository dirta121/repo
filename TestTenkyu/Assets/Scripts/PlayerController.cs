using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hole") && gameObject.CompareTag("Player")) //green ball collides with hole
        {
            LevelController.instance.Win();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _rb.velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player")) //touch red ball=lose
        {
            LevelController.instance.Lose();
        }      
    }
}
