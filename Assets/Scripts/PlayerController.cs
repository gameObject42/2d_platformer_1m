using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float speed = 20;
    private float mf;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Vector2 moveF = new Vector2(mf, 0);
        _controller.Move(moveF * Time.deltaTime * speed);
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
