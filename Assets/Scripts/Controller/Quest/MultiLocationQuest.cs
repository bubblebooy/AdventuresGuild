using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLocationQuest : QuestController
{
    public List<Vector3> locations;

    public override void Setup()
    {
        base.Setup();
        gold = (int)(Random.value * 5f + 10f);
        completion = 1 + Random.value * 2;
        if (locations.Count == 0)
        {
            int length = (int)Random.Range(1, 3);
            for (int i = 0; i < length; i++)
            {
                Vector2 pos = Random.insideUnitCircle * 5f;
                if (i == 0)
                {
                    locations.Add(transform.position + (Vector3)pos);
                }
                else
                {
                    locations.Add(locations[i - 1] + (Vector3)pos);
                }
            }
        }

    }
    public override void OnQuestCompleted(Collider2D collision)
    {
        if(locations.Count <= 0)
        {
            base.OnQuestCompleted(collision);
        } else
        {
            completion = 1 + Random.value * 2;
            transform.position = locations[0];
            locations.RemoveAt(0);
        }

    }
}
