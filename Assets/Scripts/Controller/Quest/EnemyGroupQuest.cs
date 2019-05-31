using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupQuest : QuestController
{
    // Start is called before the first frame update
    public GameObject enemyLocation;
    public GameObject enemyLocationPrefab;

    public override void Setup()
    {
        base.Setup();
        gold = (int)(Random.value * 10f + 5f);
        completion = 5 + Random.value * 2;
    }

    public override void OnQuestCompleted(Collider2D collision)
    {
        if (enemyLocation)
        {
            var enemyLocationController = enemyLocation.GetComponent<EnemyLocationController>();
            enemyLocationController.dicovered += 10f;
            if (enemyLocationController.range.radius > 2f)
            {
                enemyLocationController.range.radius = Mathf.Max(enemyLocationController.range.radius - 1f, 2f);
            }         
        }
        base.OnQuestCompleted(collision);
    }
    public override void OnQuestFailed()
    {
        if (!enemyLocation)
        {
            Instantiate(enemyLocationPrefab, transform.position, Quaternion.identity); //GameObject newEnemyLocation = 
        }
        base.OnQuestFailed();
    }
}
