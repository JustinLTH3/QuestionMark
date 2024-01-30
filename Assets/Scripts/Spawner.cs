using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    List<GameObject> spawnPos = new();

    //Store loaded enemies, true == spawned
    Dictionary<GameObject, bool> enemies = new();
    List<Enemy> enemiesList = new();
    float spawnFreq = 2;
    public static Spawner Instance;
    [SerializeField] private int TrackNum;
    [SerializeField] private Transform[] lines = new Transform[2];

    Coroutine GameLoop;
    Coroutine UpdateScoreCor;

    [SerializeField] Animator killscreen;
    float timer;
    [SerializeField] TMP_Text scoreDisplayInGame;
    [SerializeField] TMP_Text FinalScore;
    public Button PauseButton;

    [SerializeField] TMP_Text HighScore;

    [SerializeField] TMP_Text CountDown;

    private void Start()
    {
        instance = this;
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
            float x = Camera.main.orthographicSize / Screen.height * Screen.width * 2f;
            x /= 3f;

            GameObject temp = new("SpawnPos" + i)
            {
                transform =
                {
                    position = new Vector3(x * i - (TrackNum - TrackNum % 2)*x/2, Camera.main.orthographicSize+1, 0)
                }
            };
            spawnPos.Add(temp);
            temp = new("PlayerPos" + i)
            {
                transform = { position = new Vector3(x * i - (TrackNum - TrackNum % 2) * x / 2, -Camera.main.orthographicSize + 4, 0) }
            };
            PlayerMovement.instance.AddPlayerPos(temp);
        }
        PlayerMovement.instance.ResetPos();
        lines[0].position = new((spawnPos[0].transform.position.x + spawnPos[1].transform.position.x) / 2, 0, 0);
        lines[1].position = new((spawnPos[2].transform.position.x + spawnPos[1].transform.position.x) / 2, 0, 0);
        StartGame();
    }

    public void StartGame()
    {
        GameLoop = StartCoroutine(SpawnEnemy());
        HighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        scoreDisplayInGame.text = "0";
        PlayerMovement.instance.enabled = true;
        spawnFreq = 2;
    }
    IEnumerator UpdateScore()
    {
        WaitForSeconds one = new(1);
        while (true)
        {
            yield return one;
            scoreDisplayInGame.text = ((int)(Time.time - timer)).ToString();
        }

    }
    IEnumerator SpawnEnemy()
    {

        WaitForSeconds one = new(1);
        CountDown.enabled = true;
        for (int i = 3; i > 0; i--)
        {
            CountDown.text = i.ToString();
            yield return one;
        }
        CountDown.enabled = false;
        timer = Time.time;
        UpdateScoreCor = StartCoroutine(UpdateScore());
        PlayerMovement.instance.ResetPos();
        float spawn = 0;
        while (true)
        {
            if ((int)(Time.time - timer) % 5 == 0)
            {
                spawnFreq -= .3f;
                Debug.Log(spawnFreq);
            }
            if (spawn > 1)
            {
                yield return one;
                spawn -= 1;
                continue;
            }
            else if (spawn > 0)
            {
                yield return null;
                spawn -= Time.deltaTime;
                continue;
            }

            Debug.Log("hi");
            int rand = Random.Range(0, enemies.Count);
            while (enemies[enemiesList[rand].gameObject])
            {
                rand = Random.Range(0, enemies.Count);
            }

            enemies[enemiesList[rand].gameObject] = true;
            int rand2 = Random.Range(0, spawnPos.Count);
            enemiesList[rand].Spawn(spawnPos[rand2].transform.position);
            yield return null;
            spawn = spawnFreq;
        }
    }

    public void Despawn(GameObject enemy)
    {
        enemies[enemy] = false;
    }
    public void EndGame()
    {
        PauseButton.gameObject.SetActive(false);
        killscreen.SetTrigger("Enable");
        int score = (int)(Time.time - timer);
        FinalScore.text = score.ToString();
        if (PlayerPrefs.GetInt("HighScore", 0) < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        StopCoroutine(GameLoop);
        StopCoroutine(UpdateScoreCor);
    }
}