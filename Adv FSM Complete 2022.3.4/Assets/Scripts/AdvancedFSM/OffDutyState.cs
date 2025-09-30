using System.Collections;
using UnityEngine;

//every x amount of seconds spent in patrol state
//there will be gambling, y in z chance for unit to transition to OffDuty state

//in parent class, create an empty list
//when tank initialized, add to list
//in DeadState, remove from list
//LastStanding == true if length of list is <= 1
public class OffDutyState : FSMState
{
    public OffDutyState()
    {
        stateID = FSMStateID.Vacation;
        //VacationHome = find game object with tag vacay
    }

    public override void Reason(Transform player, Transform npc)
    {
        //if ! LastStanding
        //destination = VacationHome.transform.position;
        //if distance npc.position, VacationHome.position > x
        //travel to home

        //Check the distance to the VacationHome
        float dist = Vector3.Distance(npc.position, VacationHome.transform.position);
        if (dist >= 200.0f && dist < 300.0f)
        {
            //Rotate to the target point
            Quaternion targetRotation = Quaternion.LookRotation(VacationHome.transform.position - npc.position);
            npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

            //Go Forward
            npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);
        }

        //color switch
        //istargetable = false
        //health = maxhealth
        //fixed timer, then switch to patrol state
    }

    public override void Act(Transform player, Transform npc)
    {
        //i think this will be empty?
    }
}
