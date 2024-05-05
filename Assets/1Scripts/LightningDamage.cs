using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{
   SunlightSpearFire audioM;
   float damage = 10f;
   bool hit = false;
   void Awake()
   {
        audioM = GetComponent<SunlightSpearFire>();
   }
   void OnTriggerEnter2D(Collider2D col)
   {
        if (col != null)
        {
            if (col.gameObject.layer==3)
            {
                PlayerHealth hp = col.GetComponent<PlayerHealth>();
                if (hp.enabled == false)
                {
                    audioM.Stop();
                    Destroy(gameObject, 0.15f);
                    return;
                }
                if (hp != null && hit==false)
                {
                    hp.TakeDamage(damage);
                    hit = true;
                    audioM.Stop();
                    Destroy(gameObject,0.15f);
                }
            }
            else if (col.gameObject.layer==7)
            {
                audioM.Stop();
                Destroy(gameObject);
            }
        }
   }
}
