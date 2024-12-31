using UnityEngine;
using UnityEngine.AI;

namespace BehaviorLib{
public abstract class BehaviorNode
{
    public abstract bool Execute();
}

public class BehaviorTreeManager : MonoBehaviour
{
    private BehaviorNode root;
 
 

    ThiefController thiefController;

    void Start()
    {
        
        thiefController = this.GetComponent<ThiefController>();

        BehaviorNode alertBehavior = new AlertBehavior(thiefController);
        BehaviorNode chasingBehavior = new ChasingBehavior(thiefController);
        BehaviorNode idleBehavior = new IdleBehavior(thiefController);

        
        root = new SelectorNode(idleBehavior,alertBehavior, chasingBehavior);
    }

    void Update()
    {
        root.Execute();
    }
}
}
