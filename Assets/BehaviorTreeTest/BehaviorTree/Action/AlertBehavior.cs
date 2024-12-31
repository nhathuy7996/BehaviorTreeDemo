using UnityEngine;

namespace BehaviorLib{
public class AlertBehavior : BehaviorNode
{
    private float alertTime = 2f;
    private float elapsedTime = 0f;
    ThiefController thiefController;

    public AlertBehavior(ThiefController player){
        thiefController = player;
    }

    public override bool Execute()
    {
        if (elapsedTime < alertTime)
        {
            
            Debug.Log("Alert: Looking around...");
            elapsedTime += Time.deltaTime;
            if(thiefController.IsPlayerInFOV())
                return false;
            return true;
        }

       
        Debug.Log("Alert: Finished looking around.");
        elapsedTime = 0f;
        thiefController.isInterrupt = false;
        return false;
    }


    private void OnDrawGizmos() {
        Debug.LogError("Herreee");
    }
}
}
