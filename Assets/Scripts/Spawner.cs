using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    List<GameObject> spawnPos = new();

    //Store loaded enemies, true == spawned
    Dictionary<GameObject, bool> enemies = new();
    List<Enemy> enemiesList = new();
    float spawnFreq = 2;
    public static Spawner Instance;
    [SerializeField] private int TrackNum;

    private void Start()
    {
        List<GameObject> enemyPrefab = new();
        Instance = this;
        enemyPrefab.AddRange(Resources.LoadAll<GameObject>("Enemies/"));
        for (int j = 0; j < 3; j++)
        {
            foreach (var key in enemyPrefab.Select(Instantiate))
            {
                enemies.Add(key, false);
                enemiesList.Add(key.GetComponent<Enemy>());
            }
        }

        for (int i = 0; i < TrackNum; i++)
        {
            GameObject temp = new("SpawnPos" + i)
            {
                transform =
                {
                    position = new Vector3(2 * i - (TrackNum - TrackNum % 2), Camera.main.orthographicSize, 0)
                }
            };
            spawnPos.Add(temp);
        }

        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds waitForSeconds = new(spawnFreq);
        while (true)
        {
            int rand = Random.Range(0, enemies.Count);
            while (enemies[enemiesList[rand].gameObject])
            {
                rand = Random.Range(0, enemies.Count);
            }

            enemies[enemiesList[rand].gameObject] = true;
            int rand2 = Random.Range(0, spawnPos.Count);
            enemiesList[rand].Spawn(spawnPos[rand2].transform.position);

            yield return waitForSeconds;
        }
    }

    public void Despawn(GameObject enemy)
    {
        enemies[enemy] = false;
    }
}