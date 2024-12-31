using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorLib{
public class IdleBehavior : BehaviorNode
{
    private NavMeshAgent agent;

    private Transform carringObject, target,exitDoor;

    private Animator animator;   

    List<GameObject> targets = new List<GameObject>();

    ThiefController thiefConttroller;

    public IdleBehavior(ThiefController player)
    {
        this.thiefConttroller = player;
        //this.agent = player.agent; 
        this.targets = player.Targets;
        this.exitDoor = player.exitDoor;
        this.animator = player.animator;
        ChooseTarget();
    }

    void ChooseTarget(){
        target = this.targets[Random.Range(0,this.targets.Count-1)].transform;
         RotateToTarget(target.position);
    }

    void RotateToTarget(Vector3 target){
            Vector3 directionToPlayer = target - this.thiefConttroller.transform.position;
         directionToPlayer.y = 0;
        float angle = Vector3.Angle(this.thiefConttroller.transform.forward, directionToPlayer.normalized);

        Quaternion tmp = new Quaternion();
        tmp.eulerAngles = new Vector3(0,angle,0);
        thiefConttroller.transform.rotation = tmp;
    }

    public override bool Execute()
    {
        if ( exitDoor == null)
        { 
            return false;  
        }

        if (this.thiefConttroller.isInterrupt)
        { 
            DropTarget();
            return false;
        }

        if (carringObject == null)
        {
             
            this.thiefConttroller.animator.SetTrigger("Walking");
            MoveToTarget(target.position);

            
            if (Vector3.Distance(this.thiefConttroller.transform.position, target.position) < 2f)
            {   
                  RotateToTarget(exitDoor.position);
                target.SetParent(thiefConttroller.handPos);
                 target.GetComponent<Rigidbody>().isKinematic = true; 
                target.localPosition = Vector3.zero;
                
                carringObject = target;
                return true;
            }

            return true;
        }

           this.thiefConttroller.animator.SetTrigger("Carring");
        MoveToTarget(exitDoor.position);
        if (Vector3.Distance(this.thiefConttroller.transform.position, exitDoor.position) < 1f)
        {
          
            DropTarget();
            ChooseTarget();
            return true;  
        }

        return true;  
    }

    private void MoveToTarget(Vector3 targetPosition)
    {
        
        Debug.Log(this.thiefConttroller.transform.forward);

        this.thiefConttroller.transform.position = Vector3.MoveTowards( this.thiefConttroller.transform.position ,targetPosition, 5 * Time.deltaTime );
        return;
        if (agent.pathPending || agent.remainingDistance > 0.5f)
            return;

        agent.destination = targetPosition;  

       
    }
 
    
    private void DropTarget()
    {
        if(carringObject == null)
            return;

        carringObject = null;
        target.SetParent(null);
        target.GetComponent<Rigidbody>().isKinematic = false; 
        target.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);  
    }

   
}
}
