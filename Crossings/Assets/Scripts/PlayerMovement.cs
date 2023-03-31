using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public Vector2 movement;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw ("Horizontal");
        movement.y = Input.GetAxisRaw ("Vertical");
        
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
}