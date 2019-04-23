using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Travel : MonoBehaviour
{
    public Vector3 targetLocation;
    private List<Vector3> pathTargets;
    private PartyController partyController;

    int index;
    IAstarAI agent;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<IAstarAI>();
    }
    void Start()
    {
        partyController = GetComponent<PartyController>();
        partyController.fatigueRate += 1;
        agent.canMove = true;
        agent.canSearch = true;
        pathTargets = new List<Vector3>();
        NNConstraint constraint = NNConstraint.None;
        constraint.constrainTags = true;
        constraint.tags = (1 << 1);
        //Debug.Log("position:" + transform.position);
        Vector3 closestRoad = (Vector3)AstarPath.active.GetNearest(transform.position, constraint).node.position;      
        Vector3 closestRoadTarget = (Vector3)AstarPath.active.GetNearest(targetLocation, constraint).node.position;
        if (Vector3.Distance(targetLocation, transform.position) > Vector3.Distance(closestRoad, transform.position) + Vector3.Distance(targetLocation, closestRoadTarget))
        {
            pathTargets.Add(closestRoad);
            pathTargets.Add(closestRoadTarget);
        }
        pathTargets.Add(targetLocation);
        agent.destination = pathTargets[0];
        agent.SearchPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathTargets.Count == 0) return;
        //Debug.Log(agent.reachedEndOfPath);
        bool search = false;

        // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
        // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
        if (agent.reachedEndOfPath && !agent.pathPending)
        {
            index = index + 1;
            search = true;
        }

        if (index >= pathTargets.Count)
        {
            //Debug.Log("index:" + index + "  Count:" + pathTargets.Count);
            agent.canMove = false;
            agent.canSearch = false;
            agent.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            //agent.SetPath(null);
            partyController.fatigueRate -= 1;
            Destroy(this);
        } else
        {
            agent.destination = pathTargets[index];
        }     

        if (search) agent.SearchPath();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) agent.maxSpeed += 5;
        if (collision.gameObject.layer == 8) agent.maxSpeed -= 2;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) agent.maxSpeed -= 5;
        if (collision.gameObject.layer == 8) agent.maxSpeed += 2;
    }
}
