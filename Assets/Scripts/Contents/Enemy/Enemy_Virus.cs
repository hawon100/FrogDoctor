using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Virus : EnemyController
{
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private int infection;
    [SerializeField] private float smoothing = 0.2f;
    [Range(0.0f, 100.0f)] [SerializeField] private float detectionRadius = 5f;

    Vector3 targetPos = Vector3.zero;

    private void Start()
    {
        hp = Managers.Data.LoadData<EnemyData>("Enemy/E_Virus").hp;
        maxHp = Managers.Data.LoadData<EnemyData>("Enemy/E_Virus").maxHp;
        infection = Managers.Data.LoadData<EnemyData>("Enemy/E_Virus").infection;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        PlayerDetect();
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

    private void PlayerDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                targetPos = collider.transform.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


    protected override void OnDamage(int value)
    {
        hp -= value;
    }
}
