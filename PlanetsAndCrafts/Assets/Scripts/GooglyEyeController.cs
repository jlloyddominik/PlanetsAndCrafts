using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglyEyeController : MonoBehaviour
{
    private Vector2 lastVelocity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // save velocity for use after a collision
    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    // reflect velocity off the surface
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 surfaceNormal = collision.contacts[0].normal;
        rb.velocity = Vector2.Reflect(lastVelocity, surfaceNormal);
    }
}
