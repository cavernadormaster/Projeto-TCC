using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSoldier : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;


    public enum States
    { 
       PATRULHANDO,
       ALERTA,
       PERSEGUINDO,
       ATIRANDO,
       MORTE
    
    
    }

    public States state;

    void Start()
    {
        state = States.PATRULHANDO;
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case States.PATRULHANDO:
                if (Vector3.Distance(transform.position, target) < 1)
                {
                    IterateWaypointIndex();
                    UpdateDestination();
                }
                break;
            case States.PERSEGUINDO:
                break;
            case States.MORTE:
                break;
            case States.ATIRANDO:
                Debug.Log("Atirando");
                break;
            case States.ALERTA:
                break;
        
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            state = States.ATIRANDO;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            state = States.PATRULHANDO;
        }
    }
}
