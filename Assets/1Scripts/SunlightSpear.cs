using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunlightSpear : StateMachineBehaviour
{
    public GameObject lightningBolt;
    Transform lightningStatic;
    Vector3 spawnPoint;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
      lightningStatic = animator.transform.Find("LightningSpawn");
      spawnPoint = lightningStatic.transform.position;
      GameObject sun = Instantiate(lightningBolt, spawnPoint, lightningBolt.transform.rotation);
      Destroy(sun, 3f); 
    }


}
