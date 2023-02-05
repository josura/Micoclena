using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Collider2D cd2d;

    private float movementSpeed = 0.5f;
    private float jumpForce = 3.2f;
    private bool isJumping = true;
    private bool onGround = false;
    float moveHorizontal = 0;
    float moveVertical = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical= Input.GetAxisRaw("Vertical");
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
