using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjects : MonoBehaviour
{
    public float gravityModifire = 1f;
    public float minGroundNormalY = .65f;

    protected bool isGrounded;
    protected Vector2 groundNormal;
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minDistanceCollision = 0.001f;
    protected const float shellRadius = 0.001f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        velocity += gravityModifire * Physics2D.gravity * Time.deltaTime;
        isGrounded = false;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaPosition;
        Movement(move, true);
        Debug.Log(hitBuffer);
    }

    public void Movement(Vector2 move, bool verticalMovement)
    {
        float distance = move.magnitude;
        if(distance > minDistanceCollision)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for(int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            for(int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    isGrounded = true;
                    if (verticalMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if(projection < 0)
                {
                    velocity = velocity - projection * currentNormal;

                }
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;

            }
        }
        rb2d.position = rb2d.position + move.normalized*distance;
    }
}
