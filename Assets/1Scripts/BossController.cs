using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform head;
    public Animator animator; 
    public bool move = true;
    public bool nierAttack = false;
    private Vector3 direction; 
    public float speed = 2;
    private float distance;
    private float grabCount=0f;
    private float count=0f;
    private bool lookingRight = false;
    private bool leap = false;
    private Transform target;
    public Rigidbody2D rb;
    [SerializeField] LayerMask mask;
    
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        //UppercutTrigger
        Collider2D col = Physics2D.OverlapCircle(head.position + new Vector3(0f, 1.2f, 0f), 0.3f,mask);

        direction = target.transform.position - gameObject.transform.position;
        distance = direction.x;
        Vector2 movePos = new Vector2(target.position.x,rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, movePos, speed*Time.fixedDeltaTime);
        if (Mathf.Sign(direction.x) > 0 && !lookingRight && !nierAttack)
        {
            Flip();
        }
        else if(Mathf.Sign(direction.x) < 0 && lookingRight && !nierAttack)
        {
            Flip();
        }
        if (Mathf.Abs(distance) >= 7f)
        {
            animator.ResetTrigger("Attack");
            animator.SetBool("Lightning",true);//long range lightning
        }

        if (Mathf.Abs(distance) >= 2.5f && Mathf.Abs(distance) < 7f)
        {
            leap = false;
            count = 0f;
            animator.ResetTrigger("Attack");
            animator.SetBool("Lightning", false);
            if (move)
            { 
            rb.MovePosition(newPos);
            animator.SetBool("Walking", true);//mid range chase
            }
        }
        if (Mathf.Abs(distance) < 2.5f)
        {
            if (leap==false)
            {
                grabCount++;
                leap=true;
                //rb.AddForce(direction.normalized*2500f); leap towards player
            }
            if (col == null)
            {
                animator.ResetTrigger("Uppercut");
                animator.SetBool("Walking", false);
                if (grabCount == 3)
                {
                    animator.SetTrigger("Grab");//grab attack
                    grabCount = 0;
                }
                else
                {
                    animator.SetTrigger("Attack");//normal attack
                }
            }
            else if (col != null)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Uppercut");//Uppetcut if on top of head
            }
        }
    }
    void Flip()
    {//flipping
        lookingRight = !lookingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FixedUpdate()
    {
        if (count <4f && leap)
        {
            count++;
            Vector2 lmovePos = new Vector2(target.position.x, rb.position.y);
            Vector2 leapPos = Vector2.MoveTowards(rb.position, lmovePos, 5f * Time.fixedDeltaTime);
            rb.MovePosition(leapPos);
        }
    }
}
