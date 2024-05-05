using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunlightSpearFire : MonoBehaviour
{
    private Vector2 force;
    private Vector2 dir;
    GameObject audioM;
    GameObject player;
    private Vector2 damp = new Vector2 (0f, -50f);

    void Awake()
    {
        player = GameObject.Find("Player");
        audioM = GameObject.Find("AudioManager");
    }
    void Start()
    {
        audioM.GetComponent<AudioManager>().Play("Lightning");
        dir = player.transform.position - gameObject.transform.position;
        force = new Vector2(500f, 0f)*Mathf.Sign(dir.x);
        if (Mathf.Sign(dir.x) > 0)
        {
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            theScale.y *= -1;
            transform.localScale = theScale;
            transform.Rotate(180.0f, 0.0f, -120.0f);

        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 push = new Vector2(force.x, damp.y);
        rb.AddForce(push) ;
    }
    public void Stop()
    {
        audioM.GetComponent<AudioManager>().Pause("Lightning");
    }
}
