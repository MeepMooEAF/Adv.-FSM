using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedState : FSMState
{
    public DamagedState() 
    {
        stateID = FSMStateID.Retreating;
        RestArea = GameObject.FindGameObjectWithTag("RestArea");
        destPos = RestArea.transform.position;
        curRotSpeed = 1.0f;
        curSpeed = 100.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(npc.position, destPos) <= 100.0f)
        {
            Debug.Log("Switch to Heal state");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.FoundRestArea);
        }

        if (health <= 0)
        {
            Debug.Log("Switch to Dead state");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.NoHealth);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed); 
        npc.Translate(Vector3.forward * Time.deltaTime * curSpeed); 
    }
}
