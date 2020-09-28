using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Ideal,
        Running,
        Attack
    }
    public PlayerState _playerState;

    public float speed;
    public float jumpforce;
    private float moveInput;
    public int extraJump;

    public bool facingRigt = true;
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    private SpriteRenderer p;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerState = PlayerState.Ideal;
        p = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);

        //moveInput = Input.GetAxis("Horizontal");
        if(_playerState == PlayerState.Running)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            Debug.Log(rb.velocity);
        }
        else
        {
            rb.velocity = new Vector2(moveInput * 0, rb.velocity.y);
        }
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

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
        _playerState = PlayerState.Running;
        moveInput = 1;
    }
    public void MB()
    {
        _playerState = PlayerState.Running;
        moveInput = -1;
    }
    public void Stop()
    {
        moveInput = 0;
        _playerState = PlayerState.Ideal;
    }
}
