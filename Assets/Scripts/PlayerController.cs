using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float speed = 20;
    private float mf;
    private Vector2 Y;
    private bool Grounded;
    private float jumpHeight = 2;
    private float gravity = -9.6f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Grounded = _controller.isGrounded;
        if(Grounded && Y.y < 0)
        {
            Y.y = 0f;
        }

        Vector2 moveF = new Vector2(mf, 0);
        _controller.Move(moveF * Time.deltaTime * speed);

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            Y.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        Y.y += gravity * Time.deltaTime;
        _controller.Move(Y * Time.deltaTime);
    }



    public void moveF()
    {
        mf = 1; 
    }
    public void moveB()
    {
        mf = -1;
    }
    public void stop()
    {
        mf = 0;
    }
}
