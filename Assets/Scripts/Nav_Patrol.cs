using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav_Patrol : MonoBehaviour
{
    public GameObject waypointParent;

    private NavMeshAgent agent;
    private GameObject[] waypoints;
    private int waypointIndex = 0;
    private int maxWaypoints;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        maxWaypoints = waypointParent.transform.childCount;
        waypoints = new GameObject[maxWaypoints];
        Debug.Log(maxWaypoints);
        for (int i = 0; i < maxWaypoints; i++)
        {
            waypoints[i] = waypointParent.transform.GetChild(i).gameObject;
        }
        GoToNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            waypointIndex = (waypointIndex + 1) % maxWaypoints;
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        agent.SetDestination(waypoints[waypointIndex].transform.position);
    }
}
