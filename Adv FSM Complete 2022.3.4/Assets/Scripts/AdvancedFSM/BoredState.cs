using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CS
public class BoredState : FSMState
{
    public BoredState() 
    {
        stateID = FSMStateID.Dancing;
        timer = 0;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(npc.position, player.position) <= 300.0f)
        {
            Debug.Log("Switch to Chase Position");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.SawPlayer);
        }

        timer += Time.deltaTime;
        if (timer > 3)
        {
            Debug.Log("Switch Patrol Position");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        npc.GetComponent<Renderer>().material.color = Color.blue;
        Transform turret = npc.GetComponent<NPCTankController>().turret;
        turret.transform.Rotate(0f, 10f, 0f, Space.Self);
    }
}
