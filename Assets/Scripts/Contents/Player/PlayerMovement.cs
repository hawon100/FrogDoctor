using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool doesCharacterJump = false;

    [Header("Base / Root")]
    [SerializeField] private Rigidbody2D baseRB;
    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;

    [Header("'Jumping' Character")]
    [SerializeField] private Rigidbody2D charRB;
    [SerializeField] private float jumpVal = 10f;
    [SerializeField] private int possibleJumps = 1;
    [SerializeField] private int currentJumps = 0;
    [SerializeField] private bool onBase = false;
    [SerializeField] private Transform jumpDetector;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private float jumpingGravityScale;
    [SerializeField] private float fallingGravityScale;
    [SerializeField] private Animator anim;
    [SerializeField] private float curAttackDelay;
    [SerializeField] private float maxAttackDelay;
    [SerializeField] private Transform attackRange;

    [Range(0.0f, 100.0f)][SerializeField] private float detectionRadius = 5f;

    public AudioClip attackSound;

    private Vector3 targetPos;

    private bool jump;
    private bool jumping;

    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    PlayerInput input;
    Controls controls = new Controls();

    private Vector3 charDefaultRelPos;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        charDefaultRelPos = charRB.transform.localPosition;
        targetPos = transform.position;
        maxAttackDelay = 0.01f * Player_Frog.Instance.attackSpeed;
    }

    private void Update()
    {
        controls = input.GetInput();
        if (controls.JumpState && currentJumps < possibleJumps)
        {
            jump = true;
        }

        Move();
        EnemyDetect();
        AttackDelay();
    }

    private void Move()
    {
        if (!onBase && doesCharacterJump && charRB.velocity.y < 0)
        {
            detectBase();
        }

        Vector3 targetVelocity = Vector3.zero;

        if (!jumping)
        {
            targetVelocity = new Vector2(controls.HorizontalMove * Player_Frog.Instance.hSpeed, controls.VerticalMove * Player_Frog.Instance.vSpeed);
        }
        else
        {
            targetVelocity = new Vector2(controls.HorizontalMove * Player_Frog.Instance.hSpeed, 0);
        }

        Vector2 _velocity = Vector3.SmoothDamp(baseRB.velocity, targetVelocity, ref velocity, movementSmooth);
        baseRB.velocity = _velocity;

        if (controls.HorizontalMove == 0 && controls.VerticalMove == 0)
        {
            anim.SetBool("isRun", false);
        }
        else
        {
            anim.SetBool("isRun", true);
        }

        if (doesCharacterJump)
        {
            if (onBase)
            {
                charRB.velocity = Vector2.zero;

                if (charRB.transform.localPosition != charDefaultRelPos)
                {
                    var charTransform = charRB.transform;
                    charTransform.localPosition = new Vector2(charTransform.localPosition.x,
                        charDefaultRelPos.y);
                }
            }
            else
            {
                charRB.velocity = new Vector2(_velocity.x, charRB.velocity.y);
            }

            if (jump)
            {
                charRB.isKinematic = false;
                charRB.AddForce(Vector2.up * jumpVal, ForceMode2D.Impulse);
                charRB.gravityScale = jumpingGravityScale;
                anim.SetTrigger("doJump");
                jump = false;
                currentJumps++;
                onBase = false;
                jumping = true;
                anim.SetBool("isRun", false);
            }

            if (charRB.transform.localPosition != charDefaultRelPos)
            {
                var charTransform = charRB.transform;
                charTransform.localPosition = new Vector2(charDefaultRelPos.x, charTransform.localPosition.y);
            }
        }
        if (controls.HorizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        else if (controls.HorizontalMove < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void detectBase()
    {
        RaycastHit2D hit = Physics2D.Raycast(jumpDetector.position, -Vector2.up, detectionDistance, detectLayer);
        if (hit.collider != null)
        {
            onBase = true;
            charRB.isKinematic = true;
            currentJumps = 0;
            jumping = false;
        }
    }

    private void EnemyDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackRange.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (curAttackDelay < maxAttackDelay)
                    return;

                targetPos = collider.transform.position;

                if (collider.GetComponent<Enemy_Virus>() != null)
                {
                    Enemy_Virus virus = collider.GetComponent<Enemy_Virus>();
                    virus.OnDamage(Player_Frog.Instance.attack);
                    virus.isHit = true;
                    anim.SetTrigger("doAttack");
                    Managers.Sound.Play(attackSound);
                }

                curAttackDelay = 0;
            }
        }
    }

    private void AttackDelay()
    {
        curAttackDelay += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackRange.position, detectionRadius);

        if (doesCharacterJump)
        {
            Gizmos.DrawRay(jumpDetector.transform.position, -Vector3.up * detectionDistance);
        }
    }
}
