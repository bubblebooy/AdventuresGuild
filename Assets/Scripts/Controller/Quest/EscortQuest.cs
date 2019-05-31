using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EscortQuest : QuestController
{
    public Vector3 location;
    IAstarAI agent;
    // need to change completion condition

    public override void Setup()
    {
        base.Setup();
        gold = (int)(Random.value * 5f + 10f);
        completion = 1 + Random.value * 2;
        agent = GetComponent<IAstarAI>();
        if (location == Vector3.zero)
        {
            Vector2 pos = Random.insideUnitCircle.normalized * Random.Range(8f, 15f);//Random.insideUnitCircle * 20f;
            location = transform.position + (Vector3)pos;
        }
    }

    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            IAstarAI partyAgent = collision.GetComponent<IAstarAI>();
            partyAgent.canMove = true;
            partyAgent.canSearch = true;
            partyAgent.destination = transform.position;
            partyAgent.SearchPath();
        }

    }

    public override void OnQuestCompleted(Collider2D collision)
    {
        if (agent.reachedEndOfPath)
        {
            base.OnQuestCompleted(collision);
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            agent.destination = location;
        }

    }


}
