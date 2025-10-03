using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CS
public class HealState : FSMState
{
    public HealState() 
    {
        stateID = FSMStateID.Healing;
        maxHealth = 100;
        healPerSecond = 5f;
        timer = 0;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (npc.GetComponent<NPCTankController>().health >= maxHealth)
        {
            npc.GetComponent<NPCTankController>().health = maxHealth;
            npc.GetComponent<NPCTankController>().invincible = false;
            Debug.Log("Switch to Patrol state");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.LostPlayer);
        } 
    }

    public override void Act(Transform player, Transform npc)
    {
        npc.GetComponent<Renderer>().material.color = Color.cyan; //CS

        npc.GetComponent<NPCTankController>().invincible = true;
        timer += Time.deltaTime;
        if (timer > .5f)
        {
            npc.GetComponent<NPCTankController>().health += Mathf.CeilToInt(healPerSecond);
            timer = 0;
        }
    }
}
