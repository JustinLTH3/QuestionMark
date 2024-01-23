using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPos = new List<GameObject>();
    //Store loaded enemies, true == spawned
    [SerializeField] Dictionary<GameObject, bool> enemies = new();
    List<Enemy> enemiesList = new();
    float spawnFreq = 5;


    private void Start()
    {
        List<GameObject> enemyPrefab = new();
        enemyPrefab.AddRange(Resources.LoadAll<GameObject>("Enemies/"));
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < enemyPrefab.Count; i++)
            {
                GameObject key = Instantiate(enemyPrefab[i]);
                enemies.Add(key, false);
                enemiesList.Add(key.GetComponent<Enemy>());
            }
        }
        StartGame();
    }
    void StartGame()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(spawnFreq);
        while (true)
        {

            int rand = Random.Range(0, enemies.Count);
            while (enemies[enemiesList[rand].gameObject])
            {
                rand = Random.Range(0, enemies.Count);
            }
            enemies[enemiesList[rand].gameObject] = true;
            enemiesList[rand].Spawn(Vector3.zero);

            yield return waitForSeconds;
        }

    }
}
