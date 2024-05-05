using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public GameObject player;
    public Text hp;

    void Update()
    {
         PlayerHealth remhp = player.GetComponent<PlayerHealth>();
         var disphealth = remhp.current_Health;
         if (disphealth > 0)
         {
             hp.text = disphealth.ToString();
         }
         else if(disphealth<=0)
         {
             hp.text =" DEAD";
         }
    }
}
