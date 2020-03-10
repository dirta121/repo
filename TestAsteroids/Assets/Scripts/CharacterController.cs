using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float rotationSpeed;
    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource audoiSource;
    public AudioClip thrust;
    public AudioClip shoot;

    public Transform gun;
    


    public GameObject bulletPREFAB; 

    public float maxVelocity;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        audoiSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(50);
        }

        
        if (Input.GetAxis("Vertical") > 0)
        {
          
            anim.SetFloat("Speed", 2.0f);
            AudioManager.instance.Play("thrust");

            ThrustForward(y* speed);
        }
        else
        {
            anim.SetFloat("Speed", 0);
          
        }
     
        Rotate(transform, -x * rotationSpeed);
        ClampVelocity();
    }
    void ClampVelocity()
    {
        var x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        var y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = new Vector2(x, y);
    }
    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
    }
    private void Rotate(Transform transform,float amount)
    {
        transform.Rotate(0, 0, amount);
    }

    public void Shoot(float amount)
    {
        var bullet = ObjectPooler.instance.SpawnFromPool("bullet", gun.position, default(Quaternion));
        Vector2 force = transform.up * amount;
        bullet.GetComponent<Rigidbody2D>().AddForce(force);
    }
    
}
