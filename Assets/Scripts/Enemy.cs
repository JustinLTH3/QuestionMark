using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    public void Spawn(Vector3 spawnPos)
    {
        gameObject.SetActive(true);
        transform.position = spawnPos;
        rigidbody.velocity = new Vector2(0, -10);
        StartCoroutine(Despawn());
    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        rigidbody.velocity = Vector2.zero;
        Spawner.Instance.Despawn(gameObject);
    }
}