using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float spawnTime;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public GameObject spawnPoint;
    public bool isVisible = false;

    void Start()
    {
        if (isVisible)
        {
            StartCoroutine(SpawnSlimes());
        }
    }
    private void Update()
    {
        if (!isVisible)
        {
            RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(transform.position, new Vector2(spawnPoint.GetComponent<SpriteRenderer>().flipX == false ? 1 : -1, 0));
            if (Array.Exists(raycastHit2D, x => x.collider.gameObject.tag.Equals("Player")))
            {
                StartCoroutine (SpawnSlimes());
                isVisible = true;
            }
        }
    }

    private IEnumerator SpawnSlimes()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed,0);
        yield return new WaitForSeconds(spawnTime);
        yield return SpawnSlimes();
    }
}
