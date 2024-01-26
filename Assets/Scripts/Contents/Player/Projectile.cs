using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    private float time = 2.0f;

    private void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            time = 2.0f;
            Enemy_Virus enemy = collision.GetComponent<Enemy_Virus>();
            enemy.OnDamage(10000);
            EnemySpeedDown();
        }
    }

    private IEnumerator EnemySpeedDown()
    {
        while(time > 0)
        {
            Enemy_Virus.Instance.smoothing -= 2000.0f / 100.0f;
            time--;
        }

        Enemy_Virus.Instance.smoothing = 1f;

        yield break;
    }
}