using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSoldier : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoints;
    public float followSpeed = 5.0f;
    public GameObject Olhos;
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
                PATRULHANDO(); 
                break;
            case States.PERSEGUINDO:
                PERSEGUINDO();
                break;
            case States.MORTE:
                break;
            case States.ATIRANDO:
                ATIRANDO();
                break;
            case States.ALERTA:
                break;
        
        }
    }

    public void PATRULHANDO()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    public void PERSEGUINDO()
    {
        this.gameObject.transform.LookAt(player);
        Vector3 directionToTarget = player.position - transform.position;
        Vector3 desiredPosition = transform.position + directionToTarget.normalized * followSpeed * Time.deltaTime;
        transform.position = desiredPosition;
    }

    public void ATIRANDO()
    {
        this.gameObject.transform.LookAt(player);
        Olhos.gameObject.transform.LookAt(player);
        EnemyGunSystem.shooting = true;
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
            EnemyGunSystem.shooting = false;
        }
    }
}
