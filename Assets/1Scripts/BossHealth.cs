using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    private float full_Health = 100f;
    public float current_Health;
    public Animator animator;
    public GameManager gameManager;
    public BossHealthBar healthBar;
    private bool isDead = false;
    private bool phase = false;
    public Rigidbody2D rb;
    private Transform target;

    void Start()
    {
        GetComponent<BossController>().enabled = true;
        target = PlayerManager.instance.player.transform;
        current_Health = full_Health;
        healthBar.MaxHealth(full_Health);
    }

    public void TakeDamage(float damage)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.AddForce(new Vector2(-direction.x * 40f, 10f), ForceMode2D.Impulse);
        current_Health -= damage;
        healthBar.Health(current_Health);
        //phase2
        if (current_Health <= full_Health / 2f && !phase)
        {
            animator.SetTrigger("Phase2"); 
            phase = true;
            GetComponent<BossController>().speed = 3f;
            GetComponent<BossController>().move = false;
        }

        if (current_Health <= 0 && isDead == false)
        {
            isDead = true;
            Death();
        }
    }

    void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
        GetComponent<BossController>().enabled=false;
        gameManager.Victory();
    }
}
