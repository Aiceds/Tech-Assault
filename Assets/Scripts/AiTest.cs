using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiTest : MonoBehaviour
{
    public Transform[] points;
    private int destinationPoint = 0;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance > 0.5f) GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (points.Length == 0) 
            return;

        agent.destination = points[destinationPoint].position;

        destinationPoint = (destinationPoint + 1) % points.Length;
    }
}
