using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackController : MonoBehaviour
{
    public Transform boss;
    public Transform lightningPos;
    public AudioManager audioM;
    public GameObject sunlightSpear;
    public GameObject lightningRain;
    public GameObject fireSword;
    public LayerMask attackMask;
    public Transform hitBox;
    private Transform target;
    private bool lightningSpawn = false;
    private bool explosion = false;
    private bool hit = false;
    private bool grab = false;
    private float attackRange = 1.5f;
    private float attackDamage = 10f;
    private float upperCut = 15f;
    private Collider2D colGrab;

    void RangeAttack()
    {
        Vector3 spawn = lightningPos.position;
        GameObject sun = Instantiate(sunlightSpear, spawn, sunlightSpear.transform.rotation);
        Destroy(sun, 3f);
    } 

    void MeleeAttack()
    {
        audioM.Play("BossAttack");
        Collider2D col = Physics2D.OverlapCircle(hitBox.position, attackRange, attackMask);
        if (col != null)
        {
            if (col.GetComponent<PlayerHealth>().enabled == false)
            {
                return;
            }
            col.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    void Uppercut()
    {
        Collider2D col = Physics2D.OverlapCircle(hitBox.position,1f, attackMask);
        audioM.Play("Uppercut"); 
        if (col != null)
        {
            col.GetComponent<PlayerHealth>().TakeDamage(upperCut);
            col.GetComponent<PlayerHealth>().HitOff(boss.position);
        }
    }

    void LightningRain()
    {
        target = PlayerManager.instance.player.transform;
        lightningSpawn = true;
        audioM.Play("Thunder");
        audioM.Pause("Lightning");
    }

    void Update()
    {
        if (explosion)
        {
           attackDamage = 12f;
           upperCut = 18f;
           Collider2D col = Physics2D.OverlapCircle(boss.position, 3f);
           if (col != null)
           {
              if (col.tag != "Player") { return; }
              col.GetComponent<PlayerHealth>().ExplosionBlast(boss.position);
              explosion = false;
           }
        }
        if (hit)
        {
            Collider2D col = Physics2D.OverlapCircle(hitBox.position, 0.4f, attackMask);
            if (col != null)
            {
                col.GetComponent<PlayerHealth>().ExplosionBlast(gameObject.transform.position);
                hit = false;
            }
        }
        if (grab)
        {
            Collider2D col = Physics2D.OverlapCircle(gameObject.transform.position, 1f,attackMask);
            if (col == null)
                return;
            if (col.gameObject.tag== "Player")
            {
                colGrab = col;
                col.GetComponent<PlayerHealth>().GrabAnimation();
                grab = false;
            }
            else
            {
                return;
            }
        }
        if (!lightningSpawn)
        {
            return;
        }
        var min = target.position.x - 2.5f;
        float angle = 50f;
        for (int i = 0; i < 5; i++)
        {
            GameObject Lightning = Instantiate(lightningRain, new Vector3(min, Random.Range(9f,11f), 0f),Quaternion.identity);
            Vector3 eulerAngles = Lightning.transform.rotation.eulerAngles + new Vector3(0f,0f,angle);
            Lightning.transform.rotation = Quaternion.Euler(eulerAngles);
            Lightning.GetComponent<Rigidbody2D>().velocity = (target.position - Lightning.transform.position).normalized * 10f;
            angle = angle - 15f;
            min++;
        }
        lightningSpawn = false;
    }

    void GrabAttack()
    {
        grab = true;
        GetComponentInParent<BossController>().move = false;
        GetComponentInParent<BossController>().enabled = false;
    }

    void GrabOver()
    {
        grab = false;
    }

    void GrabDamage()
    {
        if (colGrab != null)
        {
            if (colGrab.GetComponent<PlayerHealth>().grabbed)
            {
                audioM.Play("BossAttack");
                colGrab.GetComponent<PlayerHealth>().TakeDamage(20f);
            }
        }
    }

    void ResetGrab()
    {
        GetComponentInParent<BossController>().move = true;
        GetComponentInParent<BossController>().enabled = true;
        if (colGrab != null && colGrab.GetComponent<PlayerHealth>().grabbed)
        {
            colGrab.GetComponent<PlayerHealth>().GrabOver();
        }
    }

    void StopWalking()
    {
        GetComponentInParent<BossController>().move = false;
    }

    void ResumeWalking()
    {
        GetComponentInParent<BossController>().move = true;
        explosion = false;
        hit = false;
    }

    void PhaseChange()
    {
        audioM.Play("Phase2");
        Vector2 theScale = boss.localScale;
        theScale *= 1.2f;
        boss.localScale = theScale;
        boss.position = new Vector3(boss.position.x, -0.29f, 0f);
        fireSword.SetActive(true);
    }

    void NierAttack()
    {

        hit = true;
        boss.gameObject.layer = 1;
        explosion = false;
        GetComponentInParent<BossController>().nierAttack = true;
        //GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<CircleCollider2D>().enabled = false;
    }

    void NierReturn()
    {
        boss.gameObject.layer = 6;
        GetComponentInParent<BossController>().nierAttack = false;
        //GetComponent<BoxCollider2D>().enabled = true;
        //GetComponent<CircleCollider2D>().enabled = true;
    }

    void Explosion()
    {
        explosion = true;
        audioM.Play("Explosion");
    }

    void LightningSound()
    {
        audioM.Play("Lightning");
    }
}
    