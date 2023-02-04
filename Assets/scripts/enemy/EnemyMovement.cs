using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour,IMoveable
{

    GameObject target;
    [SerializeField] Transform targetTransform;
    public Animator anim;

    private Rigidbody2D rb2d;
    private Collider2D cd2d;

    private float movementSpeed = 0.5f;
    private float jumpForce = 2f;
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
        //moveHorizontal = Input.GetAxisRaw("Horizontal");
        //moveVertical= Input.GetAxisRaw("Vertical");

        if (target != null)
        {
            //transform.LookAt(target.transform.position);
        }
        if (PlayerManager.Instance.getHealth() <= 0)
        {
            target = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
        isJumping = false;
    }

    public void setTargetTransform(Transform _target)
    {
        targetTransform = _target;
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        //throw new System.NotImplementedException();
    }
}
