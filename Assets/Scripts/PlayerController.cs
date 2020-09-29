using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpforce;
    private float moveInput;
    public float attackRange = 0.3f;
    public float attackRate = 2f;
    private float nextAttackTime = 0;

    public int extraJump;

    public bool facingRigt = true;
    public bool isGrounded;
    public Transform attackPoint;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    public LayerMask enemyLayers;
    private SpriteRenderer p;

    private Rigidbody2D rb;
    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        p = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRigt == false && moveInput > 0)
        {
            //p.flipX = false;
            Flip();
        }
        else if(facingRigt == true && moveInput < 0)
        {
            //p.flipX = true;
            Flip();
        }
    }
    private void Update()
    {
        if(isGrounded == true)
        {
            extraJump = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log("Enemy Hit" + enemy.name);
            CameraShake.Instance.ShakeCamera(1f,0.1f);
        }

        /*if (Time.time >= nextAttackTime)
        {
            anim.SetTrigger("Attack");
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemy)
            {
                Debug.Log("Enemy Hit" + enemy.name);
            }
        }
        nextAttackTime = Time.time + 1f / attackRate;*/
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void Flip()
    {
        facingRigt = !facingRigt;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    public void Jump()
    {
        if (extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJump--;
        }
        else if (extraJump == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpforce;

        }
    }
    public void MF()
    {
        moveInput = 1;
    }
    public void MB()
    {
        moveInput = -1;
    }
    public void Stop()
    {
        moveInput = 0;
    }
}
