using UnityEngine;
using System.Collections;

public class PatrolState : FSMState
{
    public PatrolState(Transform[] wp) 
    { 
        waypoints = wp;
        stateID = FSMStateID.Patrolling;

        curRotSpeed = 1.0f;
        curSpeed = 100.0f;
        //randomizing starting time so they don't all switch to bored or off-duty at the same time
        timer = Random.Range(0, 4); 
    }

    public override void Reason(Transform player, Transform npc)
    {
        npc.GetComponent<Renderer>().material.color = Color.white; //CS

        //1. Check the distance with player tank
        if (Vector3.Distance(npc.position, player.position) <= 300.0f)
        {
            //2. Since the distance is near, transition to chase state
            Debug.Log("Switch to Chase State");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.SawPlayer);
        }
        else if (Vector3.Distance(npc.position, player.position) <= 400.0f)
        {
            Debug.Log("Switch to Camp State");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.SensedPlayer);
        }

        timer += Time.deltaTime;
        if (timer == 5) //gamble here
        {
            int gamba = Random.Range(0, 5);
            if (gamba >= 3)
            {
                Debug.Log("Switch to Bored State");
                npc.GetComponent<NPCTankController>().SetTransition(Transition.Bored);
            }
        }
        if (timer > 10)
        {
            timer = 0;
            int gamba = Random.Range(0, 10);
            if (gamba > 8)
            {
                //occupied is true (some variable to stop multiple tanks from going offduty)
                Debug.Log("Switch to OffDuty State");
                npc.GetComponent<NPCTankController>().SetTransition(Transition.VacationTime);
            }
            
        }

    }

    public override void Act(Transform player, Transform npc)
    {
        //1. Find another random patrol point if the current point is reached
        if (Vector3.Distance(npc.position, destPos) <= 100.0f)
        {
            Debug.Log("Reached to the destination point, calculating the next point");
            FindNextPoint();
        }

        //2. Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        //3. Go Forward
        npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);

    }
}