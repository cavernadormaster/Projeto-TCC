using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSoldier : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform[] player;
    public Transform[] waypoints;
    public float followSpeed = 5.0f;
    public GameObject Olhos;
    float atencao;
    public float increaseRate = 1.0f; 
    int waypointIndex;
    Vector3 target;
    int playerId;

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
                ALERTA();
                break;
        
        }
    }

    public void ALERTA()
    {
        atencao += increaseRate * Time.deltaTime;
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
        if ( atencao >= 2)
        {
            state = States.ATIRANDO;
        }
    }

    public void PATRULHANDO()
    {
        if (atencao > 0)
        {
            atencao -= increaseRate * Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    public void PERSEGUINDO()
    {
        this.gameObject.transform.LookAt(player[0]);
        Vector3 directionToTarget = player[0].position - transform.position;
        Vector3 desiredPosition = transform.position + directionToTarget.normalized * followSpeed * Time.deltaTime;
        transform.position = desiredPosition;

        if (Vector3.Distance(transform.position, player[0].position) <= 7.0f)
        {
            state = States.ATIRANDO;
        }
    }

    public void ATIRANDO()
    {
        if (playerId == 1)
        {
            this.gameObject.transform.LookAt(player[0]);
            Olhos.gameObject.transform.LookAt(player[0]);
            if(!UIController.isAlive)
            {
                state = States.PATRULHANDO;
                EnemyGunSystem.shooting = false;

            }
        }else if(playerId == 2)
        {
            this.gameObject.transform.LookAt(player[1]);
            Olhos.gameObject.transform.LookAt(player[1]);
        }
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
        if (UIController.isAlive)
        {
            if (other.CompareTag("Player"))
            {
                state = States.ALERTA;
                playerId = 1;
            }
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            state = States.PATRULHANDO;
            EnemyGunSystem.shooting = false;
            playerId = 0;
        }
    }
}
