using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    [Range(0,250)]
    public float asteroidSpeed;
    public GameObject player;
    private void Start()
    {
        StartLevel();
    }
    public void StartLevel()
    {
      
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        var random = new System.Random();
        for (int i = 0; i < 3; i++)
        {
            var position = new Vector2(random.Next((int)-screenBounds.x, (int)screenBounds.x), random.Next((int)-screenBounds.y , (int)screenBounds.y ));
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles=new Vector3(0, 0, random.Next(0, 360));
            var aster=ObjectPooler.instance.SpawnFromPool("big_asteroid",position, rotation);
            Vector2 force = position * asteroidSpeed;
            aster.GetComponent<Rigidbody2D>().AddForce(force);
        }

        for (int i = 0; i < 7; i++)
        {
            var position = new Vector2(random.Next((int)-screenBounds.x , (int)screenBounds.x ), random.Next((int)-screenBounds.y , (int)screenBounds.y ));
          
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, random.Next(0, 360));
            var aster = ObjectPooler.instance.SpawnFromPool("small_asteroid", position, rotation);
            Vector2 force = position * asteroidSpeed;
            aster.GetComponent<Rigidbody2D>().AddForce(force);
        }
        for (int i = 0; i < 4; i++)
        {
            var position = new Vector2(random.Next((int)-screenBounds.x, (int)screenBounds.x ), random.Next((int)-screenBounds.y, (int)screenBounds.y ));
            ObjectPooler.instance.SpawnFromPool("meduim_asteroid", position, default(Quaternion));
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, random.Next(0, 360));
            var aster = ObjectPooler.instance.SpawnFromPool("medium_asteroid", position, rotation);
            Vector2 force = position * asteroidSpeed;
            aster.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
    public void AwakePlayer()
    {
        player.SetActive(true);
    }
    public void SleepPlayer()
    {
        player.SetActive(false);
    }
}
