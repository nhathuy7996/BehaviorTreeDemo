using UnityEngine;
using UnityEngine.AI;  

namespace BehaviorLib{
public class ChasingBehavior : BehaviorNode
{
    private ThiefController thiefController;
    private NavMeshAgent agent;
    private float chaseTimeout = 5f;  
    private float elapsedTime = 0f;

    public ChasingBehavior(ThiefController player)
    {
        this.thiefController = player; 
    }

    public override bool Execute()
    {
        
        if (!thiefController.IsPlayerInFOV())
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= chaseTimeout)
            {
                Debug.Log("Chasing: Lost sight of the player. Exiting chase...");
                return false;  
            }
        }
        else
        {
            elapsedTime = 0f;  
            Debug.Log("Chasing: Player spotted, chasing...");

            thiefController.ChasePlayer();
        }

        return true;  
    }

   
}
}
