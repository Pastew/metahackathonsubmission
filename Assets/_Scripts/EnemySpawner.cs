using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemiesPrefabs;
    [SerializeField] private GameObject _spawnPoint;

    private float _cooldown;

    private void Start()
    {
        _cooldown = Random.Range(3, 10);
    }

    void Update()
    {
        _cooldown -= Time.deltaTime;

        if (_cooldown <= 0)
        {
            ResetCooldown();
            SpawnEnemy();
        }
    }

    private void ResetCooldown()
    {
        _cooldown = Random.Range(3, 10);
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemiesPrefabs.Random(), _spawnPoint.transform.position, Quaternion.identity);
    }
}
