using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float full_Health = 100f;
    public float current_Health;
    public Animator animator;
    public GameManager gameManager;
    public AudioManager audioM;
    public Transform grabPos;
    private bool isDead = false;
    public bool grabbed = false;
    private Transform target;
    private Rigidbody2D rb;
    private float grabAngle = 90f;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        current_Health = full_Health; 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (current_Health <= 0 && isDead == false)
        {
            isDead = true;
            Death();
        }
        if (grabbed)
        {
            gameObject.transform.position = grabPos.position;
        }
        if (isDead)
        {
            animator.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (current_Health >= 1 && target.position.y<0f)
        {
            audioM.Play("Hit");
            animator.SetTrigger("Hurt");
            current_Health -= damage;
        }
        else if (current_Health >= 1 && target.position.y > 0f )
        {
            audioM.Play("Hit");
            animator.SetTrigger("Dizzy");
            //GetComponent<PlayerHealth>().enabled = false;
            current_Health -= damage;
        }
    }

    public void HitOff(Vector3 pos) 
    {
        audioM.Play("Hit");
        Vector3 direction = target.position - pos;
        rb.AddForce(new Vector2(direction.x,0f).normalized * 400f);
    }

    public void ExplosionBlast(Vector3 pos)
    {
        audioM.Play("Hit");
        TakeDamage(20f);
        Vector3 direction = target.position - pos;
        rb.AddForce(direction.normalized * 700f);
        animator.SetTrigger("Dizzy");
        GetComponent<PlayerHealth>().enabled = false;
    }

    public void GrabAnimation()
    {
        this.enabled = true;
        animator.SetTrigger("Grabbed");
        grabbed = true;
        Vector3 direction = target.position-grabPos.position;
        if (direction.x > 0&& !GetComponent<CharacterController2D>().m_FacingRight) 
        {
           grabAngle = -90f;
        }
        gameObject.transform.Rotate(new Vector3(0f, 0f, 1f), grabAngle);
        animator.GetComponent<PlayerMovement>().enabled = false;
    }

    public void GrabOver()
    {
        grabbed = false;
        Vector3 direction = target.position - grabPos.position;
        if (direction.x > 0&& !GetComponent<CharacterController2D>().m_FacingRight)
        {
            grabAngle = -90f;
        }
        gameObject.transform.Rotate(new Vector3(0f, 0f, 1f), -grabAngle);
        animator.GetComponent<PlayerMovement>().enabled = true;
        animator.SetBool("Blocking", false);
    }

    public void Death()
    {
       this.enabled = true;
       animator.SetBool("Death",true);
       gameManager.GameOver();
    }
}

