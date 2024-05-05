using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRainDamage : MonoBehaviour
{
   float damage = 10f;
   bool hit = false;

   void OnTriggerEnter2D(Collider2D col)
   {
      if (col != null)
      {
          PlayerHealth hp = col.GetComponent<PlayerHealth>();
          if (hp != null && hit == false)
          {
              hp.enabled = true;
              hp.TakeDamage(damage);
              hit = true;
              Destroy(gameObject, 0.15f);
          }
          else
          {
              Destroy(gameObject);
          }
      }
   }
}
