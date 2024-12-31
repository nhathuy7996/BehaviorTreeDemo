using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefController : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public Animator animator;

    [SerializeField] List<GameObject> targets = new List<GameObject>();
    public List<GameObject> Targets => targets;

    public Transform exitDoor, handPos;

    public bool isInterrupt = false;

    [SerializeField] Transform player;

    private void Start() {
        animator = this.GetComponentInChildren<Animator>();
        agent = this.GetComponent<NavMeshAgent>();    
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            this.animator.SetTrigger("Falling");
            animator.SetTrigger("TrapTrigger");

            isInterrupt = true;
            Debug.Log("Kẻ trộm bị phát hiện!");
        }
    }

     public bool IsPlayerInFOV()
    {
        
        Vector3 directionToPlayer = player.position - this.transform.position;
        float angle = Vector3.Angle(this.transform.forward, directionToPlayer);

         
        float fovAngle = 120f;
        float detectionRange = 10f; 

        return angle < fovAngle / 2 && directionToPlayer.magnitude <= detectionRange;
    }

    public void ChasePlayer(){
         this.transform.position = Vector3.MoveTowards( this.transform.position ,player.transform.position, 5 * Time.deltaTime );
    }

 
}
