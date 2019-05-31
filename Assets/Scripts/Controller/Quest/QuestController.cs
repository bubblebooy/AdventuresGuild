using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    //private GameObject quests;
    public int gold;
    public int xp = 50;
    public float completion;
    public float expireTime = 60f;
    public CharacterList enemies;
    public GameObject enemyPrefab;
    public bool hostile = true;
    public GameObject completedBy;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject quests = GameObject.Find("Quests");
        enemies = GetComponent<CharacterList>();
        Setup();
        expireTime += completion;
        SetName();
        transform.SetParent(quests.transform);
        InvokeRepeating("NearbyObjects", 0f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        expireTime -= Time.deltaTime;
        if (expireTime <= 0)
        {
            OnQuestFailed(); // add failure conequence here
        }
    }

    protected virtual void NearbyObjects()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, GetComponent <CircleCollider2D>().radius, LayerMask.GetMask("Allies"));
        if (collider2Ds.Length > 0)
        {
            completion -= 0.5f;
            if (hostile && enemies.Characters.Count > 0)
            {
                var Characters = collider2Ds[Random.Range(0, collider2Ds.Length)].gameObject.GetComponent<PartyController>().CharacterList;
                Characters.Attack(enemies);
                enemies.Attack(Characters);
            }
        }
        if (completion <= 0 && collider2Ds.Length > 0)
        {
            OnQuestCompleted(collider2Ds[Random.Range(0, collider2Ds.Length)]);
        }
    }

    public virtual void OnQuestCompleted(Collider2D collision)
    {
        collision.gameObject.GetComponent<PartyController>().Gold += gold;
        collision.gameObject.GetComponent<PartyController>().CharacterList.GiveXp(xp);
        completedBy = collision.gameObject;
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public virtual void OnQuestFailed()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public virtual void Setup()
    {
        gold = (int)(Random.value * 10f);
        completion = 2 + Random.value * 8;
        int numberEnemies = Random.Range(0, 5);
        for (int i = 0; i < numberEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemies.Characters.Add(enemy);
        }
    }
    public virtual void SetName()
    {
        gameObject.name = RandomWord.Adjective() + " " + RandomWord.Noun();
    }
}
