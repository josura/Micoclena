using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Collider2D cd2d;
    public Animator thisAnim;
    public SpriteRenderer spriteRendcap;
    public SpriteRenderer spriteRendModel;

    private float movementSpeed = 0.5f;
    private float jumpForce = 3.2f;
    private bool isJumping = true;
    private bool onGround = false;
    float moveHorizontal = 0;
    float moveVertical = 0;
    // Start is called before the first frame update
    void Start()
    {

        thisAnim = GetComponentInChildren<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        spriteRendcap = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRendModel = transform.Find("sprite").GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        thisAnim.SetFloat("xvelocity", Math.Abs( moveHorizontal));
        moveVertical= Input.GetAxisRaw("Vertical");
        thisAnim.SetFloat("yvelocity", Math.Abs(moveVertical));
        if (moveHorizontal < 0)
        {
            spriteRendcap.flipX = true;
            spriteRendModel.flipX = true;
        } else
        {
            spriteRendcap.flipX = false;
            spriteRendModel.flipX = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
        isJumping = false;
    }

    void FixedUpdate()
    {
        if(moveHorizontal > 0.01f || moveHorizontal < -0.01f)
        {
            Vector2 vec = new Vector2(movementSpeed* moveHorizontal,0);
            rb2d.AddForce(vec,ForceMode2D.Impulse);
        }
        if ((Input.GetKey(KeyCode.Space) || moveVertical > 0.01f ) && onGround && !isJumping)
        {
            isJumping = true;
            onGround = false;
            Vector2 vec = new Vector2(0, jumpForce);
            rb2d.AddForce(vec, ForceMode2D.Impulse);
        }
    }

}
