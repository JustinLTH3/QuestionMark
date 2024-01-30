using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    public void Spawn(Vector3 spawnPos)
    {
        gameObject.SetActive(true);
        transform.position = spawnPos;
        rb.velocity = new Vector2(0, -10);
        StartCoroutine(Despawn());
    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        rb.velocity = Vector2.zero;
        Spawner.Instance.Despawn(gameObject);
    }
}