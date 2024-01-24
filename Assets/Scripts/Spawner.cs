using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    List<GameObject> spawnPos = new List<GameObject>();
    //Store loaded enemies, true == spawned
    Dictionary<GameObject, bool> enemies = new();
    List<Enemy> enemiesList = new();
    float spawnFreq = 5;
    public static Spawner Instance;

    private void Start()
    {
        List<GameObject> enemyPrefab = new();
        Instance = this;
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
        Camera.main.orthographicSize = 8 * Screen.height / Screen.width * 0.5f;
        for (int i = 0; i < 3; i++)
        {
            GameObject temp = new GameObject("spawnpos" + i);
            temp.transform.position = new(2 * i - 2 * (3 - 3 % 2) / 2, 0, 0);
            spawnPos.Add(temp);
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
