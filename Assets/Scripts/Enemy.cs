using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }
    public void Spawn(Vector3 spawnpos)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = spawnpos;
        StartCoroutine(Despawn());
    }
    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        Spawner.Instance.Despawn(gameObject);
    }
}
