using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    float x_move;
    public float speed = 40f;
    [SerializeField] private bool jump = false;
    public bool isAttacking = false;
    public bool isJumping = false;
    public bool isSliding = false;
    public bool isBlocking = false;
    public LayerMask attackMask;
    public float attackDamage = 8f;
    public float attackRange = 0.58f;
    public GameObject hitmarkCheck;
    public AudioManager audioM;
    public Rigidbody2D rb;
    public Transform bossPos;
    private Transform target;
    private Vector3 direction;
    private float delay = 0f;

    void Start()
    {
       target = PlayerManager.instance.player.transform;
       animator.SetBool("Blocking", false);
    }

    void Update()
    {
       if (!isSliding)
       {
          x_move = Input.GetAxisRaw("Horizontal") * speed;
          animator.SetFloat("Speed", Mathf.Abs(x_move));
       }
       //force direction
       direction = (target.position-bossPos.position).normalized;
       //all inputs
       if (Input.GetButtonDown("Jump") && !isAttacking && !isJumping)
       {
          jump = true;
          isJumping = true;
       }

       if (Input.GetButtonDown("Fire1") && !isAttacking && !isSliding && !isJumping)
       {
          audioM.Play("PlayerAttack");
          isAttacking = true;
          animator.SetTrigger("Attack");
       }

       if (Input.GetButtonDown("Roll") && !isAttacking && !isJumping && !isSliding && !isBlocking && Time.time>=delay)
       {
          isSliding = true;
          x_move = 0;
          animator.SetTrigger("Roll");
          delay = Time.time + 0.8f;
       }

       if (Input.GetButtonDown("Fire1") && isSliding)
       {
          animator.SetTrigger("Strike");
       }

       if (Input.GetButton("Fire2") && !isJumping && !isAttacking && !isSliding)
       {
          animator.SetBool("Blocking", true);
          GetComponentInParent<PlayerHealth>().enabled = false;
          x_move = 0;
          isAttacking = true;
       }

       if(Input.GetButtonUp("Fire2"))
       {
          animator.SetBool("Blocking", false);
          isAttacking=false;
       }
    }

    void FixedUpdate()
    {
       if (!isAttacking && !isBlocking)
       {
          controller.Move(x_move * Time.fixedDeltaTime, false, jump);
          jump = false;
       }

       if (controller.m_Grounded)
       {
          isJumping = false;
          jump = false;
       }
       else
       {
          isJumping=true;
       }

       if (isSliding)
       {
          gameObject.layer = 1;
          rb.gameObject.layer = 1;
          GetComponentInParent<PlayerHealth>().enabled = false;
          if (controller.m_FacingRight)
          {
              rb.velocity = new Vector2(6f, 0f);
          }
          else
          {
              rb.velocity = new Vector2(-6f, 0f);
          }
       }
    }

    void Attack()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Roll"); 
        Collider2D col = Physics2D.OverlapCircle(hitmarkCheck.transform.position, attackRange, attackMask);
        if(col!=null)
        {
            col.GetComponentInParent<BossHealth>().TakeDamage(attackDamage);
        }
    }

    void CancelAttack()
    {
        gameObject.layer = 3;
        rb.gameObject.layer = 3;
        isSliding = false;
        isAttacking=false;
        isBlocking =false;
        GetComponentInParent<PlayerHealth>().enabled = true;
    }

    void Dizzy()
    {
        x_move = 0f;
        isBlocking = true;
        rb.AddForce(direction.x*target.right*8f, ForceMode2D.Impulse);
    }

    void Roll()
    {
        isAttacking=true;
        isSliding = true;
    }

    void Strike()
    {
        audioM.Play("PlayerAttack");
        isAttacking = true;
        isSliding = false;
    }

    void Grabbed()
    {
        isAttacking = true;
    }

    void GrabOver()
    {
        GetComponentInParent<PlayerHealth>().GrabOver();
        isAttacking = false;
        animator.SetBool("Blocking", false);
    }

    void Death()
    {
        this.enabled = false;
    }
}
