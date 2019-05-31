using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocationQuest : QuestController
{
    public GameObject enemyLocation;
    public override void Setup()
    {
        gold = (int)(Random.value * 10f + 20f);
        completion = 10 + Random.value * 2;
    }

    //public override void OnQuestCompleted(Collider2D collision)
    //{
    //        base.OnQuestCompleted(collision);
    //}
    public override void OnQuestFailed()
    {
        completion += 10;
        enemyLocation.GetComponent<EnemyLocationController>().range.radius += 5f;
        expireTime = 60f;
    }
    public override void SetName()
    {
        gameObject.name = "ENEMY LOCATION:" + RandomWord.Adjective() + " " + RandomWord.Noun();
    }
}
