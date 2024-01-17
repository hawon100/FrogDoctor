using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Virus : EnemyController
{
    public static Enemy_Virus Instance;

    public int score;

    public float hp;
    public float infection;
    public float smoothing = 0.2f;
    [Range(0.0f, 100.0f)][SerializeField] private float detectionRadius = 5f;
    [SerializeField] private Vector2 boxRange;
    private Vector2 targetPos;
    private Rigidbody2D rigid;
    private Animator anim;
    public bool isHit;

    [SerializeField] private float curAttackDelay;
    [SerializeField] private float maxAttackDelay;

    [SerializeField] private float curInfectionDelay;
    [SerializeField] private float maxInfectionDelay;

    public AudioClip deathSound;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Instance = this;
    }

    private void Start()
    {
        targetPos = transform.position;
        hp = GameManager.instance.gameData.enemyHp;
        infection = GameManager.instance.gameData.enemyInfection;
        smoothing = GameManager.instance.gameData.enemySpeed;
        maxAttackDelay = 0.01f * GameManager.instance.gameData.enemyInfectionSpeed;
        maxInfectionDelay = 0.01f * GameManager.instance.gameData.enemyInfectionSpeed;
    }

    private void Update()
    {
        Hit();
        PlayerDetect();
        InfectionDelay();
        AttakcDelay();
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothing); ;
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVecR = targetPos - rigid.position;
        Vector2 nextVecR = dirVecR.normalized * 3 * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVecR);
        rigid.velocity = Vector2.zero;
    }
    bool temp = true;

    private void Hit()
    {
        if (hp <= 0)
        {
            Managers.Sound.Play(deathSound);

            if (temp)
            {
                anim.SetTrigger("Death");

                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    Death();
                }
            }
            temp = false;
        }
        else
        {
            temp = true;
        }
        KnockBack();
    }

    private void Death()
    {
        int randValue = Random.Range(0, 10);

        if(randValue < 9)
        {
            Debug.Log("None");
        }
        else if(randValue < 10)
        {
            GameManager.instance.vaccineCount = 1;
        }

        GameManager.instance.score += score;

        Destroy(gameObject);
    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForFixedUpdate();
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
    }

    private void PlayerDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        Collider2D[] colliders2 = Physics2D.OverlapBoxAll(transform.position, boxRange, 0);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                targetPos = collider.transform.position;
            }
        }

        foreach (Collider2D collider in colliders2)
        {
            if (collider.CompareTag("Player"))
            {
                if (!isHit)
                {
                    if (curInfectionDelay < maxInfectionDelay) return;

                    Player_Frog.Instance.OnDamage((int)infection * 2);

                    curInfectionDelay = 0;
                }

                if (isHit)
                {
                    if (curAttackDelay < maxAttackDelay) return;
                    Player_Frog.Instance.OnDamage((int)infection);
                    curAttackDelay = 0;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxRange);
    }

    public override void OnDamage(int value)
    {
        anim.SetTrigger("Hit");
        hp -= value;
        isHit = false;
        StartCoroutine(KnockBack());
    }

    private void InfectionDelay()
    {
        curInfectionDelay += Time.deltaTime;
    }

    private void AttakcDelay()
    {
        curAttackDelay += Time.deltaTime;
    }
}
