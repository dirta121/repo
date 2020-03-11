using System.Collections;
using UnityEngine;
public class BulletObject : MonoBehaviour, IPooledObject
{

    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        
    }
    public void OnObjectSpawn()
    {
        source.Play();
        StartCoroutine(LifeTimeCoroutine());
    }

    private void LateUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
   

    IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

}
