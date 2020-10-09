using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LevelController : Singleton<LevelController>
{
    public MeshRenderer playerPREFAB;
    public MeshRenderer enemyPREFAB;
    public MeshRenderer ground;
    public MeshRenderer wall;
    public int numberOfEnemies;
    public float rotationSpeed;
    public bool freezeYRotation;
    [Range(1.1f, 5f)]
    public int enemySocialDistance;
    //limits for spawn objects
    private float _minX;
    private float _maxX;
    private float _minZ;
    private float _maxZ;

    //key - free to spawn, Value- Vector3 position
    private Dictionary<bool, List<Vector3>> _spawnDict = new Dictionary<bool, List<Vector3>>();
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private GameObject _player;

    public UnityEvent OnLevelWin;
    public UnityEvent OnLevelLose;
    private void Start()
    {
        _minX = ground.bounds.min.x;
        _maxX = ground.bounds.max.x;
        _minZ = ground.bounds.min.z;
        _maxZ = ground.bounds.max.z;
        CreateSpawns();
        GenerateLevel();
    }
    private void Update()
    {
        var direction = AccelerometerSimulationInput.instance.GetDirection();
        var rotX = direction.x * rotationSpeed * Mathf.Deg2Rad;
        var rotZ = direction.y * rotationSpeed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.forward, -rotX);
        transform.RotateAround(Vector3.right, rotZ);

        if (freezeYRotation)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
    }
    private void GenerateLevel()
    {
        SpawnPlayer();
        //SpawnHoles
        SpawnEnemies();
    }
    private void CreateSpawns()
    {
        int ilimit = (int)(ground.bounds.size.z / (playerPREFAB.bounds.size.z + enemySocialDistance));
        int jlimit = (int)(ground.bounds.size.x / (playerPREFAB.bounds.size.x + enemySocialDistance));
        _spawnDict.Add(true, new List<Vector3>());
        _spawnDict.Add(false, new List<Vector3>());

        for (int i = 1; i <= ilimit; i++)
        {
            for (int j = 1; j <= jlimit; j++)
            {
                var key = true;
                var freeSpawns = _spawnDict[key];
                var position = transform.InverseTransformPoint(new Vector3((_minX + (enemyPREFAB.bounds.size.x + enemySocialDistance) * i), 0, (_maxZ - j * (enemyPREFAB.bounds.size.z + enemySocialDistance))));
                freeSpawns.Add(new Vector3(position.x, 1, position.z));
            }
        }
    }
    private void ResetSpawns()
    {
        foreach (var position in _spawnDict[false])
        {
            _spawnDict[true].Add(position);
        }
        _spawnDict[false].Clear();
    }
    private void SpawnPlayer()
    {
        Vector3 position;
        var havePosition = FindPosition(out position);
        if (havePosition)
        {
            var go = Pooler.instance.SpawnFromPool("Player", transform, position, Quaternion.identity);
            _player = go;
            _spawnedObjects.Add(go);
        }
        else
        {
            return;
        }
    }
    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 position;
            var havePosition = FindPosition(out position);
            if (havePosition)
            {
                var go = Pooler.instance.SpawnFromPool("Enemy", transform, position, Quaternion.identity);
                _spawnedObjects.Add(go);
            }
            else
            {
                return;
            }
        }
    }
    private bool FindPosition(out Vector3 position)
    {
        position = Vector3.zero;
        if (_spawnDict[true].Count == 0)
            return false;
        var key = true;
        var index = Random.Range(0, _spawnDict[true].Count - 1);
        position = _spawnDict[key][index];
        _spawnDict[key].RemoveAt(index);
        key = false;
        _spawnDict[key].Add(position);
        return true;
    }
    private void ResetLevel()
    {
        ResetSpawns();

        DisableSpawnedObjects();
        var playerCollider = _player.GetComponent<Collider>();
        if (playerCollider)
            IgnoreGround(playerCollider, false);
        GenerateLevel();
    }
    private void DisableSpawnedObjects()
    {
        foreach (GameObject go in _spawnedObjects)
        {
            go.SetActive(false);
        }
    }
    private void IgnoreGround(Collider collider, bool ignore)
    {
        var groundCollider = ground.GetComponent<Collider>();
        if (groundCollider)
            Physics.IgnoreCollision(collider, groundCollider, ignore);
    }
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        ResetLevel();
    }
    public void Win()
    {
        Debug.Log("Win");
        var playerCollider = _player.GetComponent<Collider>();
        if (playerCollider)
            IgnoreGround(playerCollider, true);

        OnLevelWin?.Invoke();

        StartCoroutine(WaitCoroutine());
    }
    public void Lose()
    {
        Debug.Log("Lose");
        OnLevelLose?.Invoke();
        ResetLevel();
    }
}
