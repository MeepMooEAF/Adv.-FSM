using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampState : FSMState
{
    public CampState() 
    {
        stateID = FSMStateID.Camping;
        timer = 0;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(npc.position, player.position) <= 300.0f)
        {
            Debug.Log("Switch to Chase Position");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.SawPlayer);
        }
        else if (Vector3.Distance(npc.position, player.position) <= 200.0f)
        {
            Debug.Log("Switch to Attack position");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.ReachPlayer);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                Debug.Log("Switch Patrol Position");
                npc.GetComponent<NPCTankController>().SetTransition(Transition.LostPlayer);
            }
        }

        if (health >= 30)
        {
            Debug.Log("Switch to Damaged state");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.LowHealth);
        }

    }

    public override void Act(Transform player, Transform npc)
    {
        //Do Nothing for the camp state
    }
}
