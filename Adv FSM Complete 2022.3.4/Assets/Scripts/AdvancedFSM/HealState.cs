using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealState : FSMState
{
    public HealState() 
    {
        stateID = FSMStateID.Healing;
        timer = 0;
        maxHealth = 100;
        healPerSecond = 5f;
    }

    public override void Reason(Transform player, Transform npc)
    {
       /* if (health >= maxHealth)
        {
            health = maxHealth;
            Debug.Log("Switch to Patrol state");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.LostPlayer);
        } */
    }

    public override void Act(Transform player, Transform npc)
    {
      /*  if (timer > 1)
        {
            health += Mathf.CeilToInt(healPerSecond);
            timer = 0;
        } */
    }
}
