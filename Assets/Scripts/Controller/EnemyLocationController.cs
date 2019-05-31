using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocationController : MonoBehaviour
{
    public GameObject questPrefab;
    public GameObject charaterPrefab;
    public GameObject inn;
    public float dicovered = 0;

    public CircleCollider2D range;
    public float maxRadius = 15f;

    public float attackChance = 0.25f;
    public float attackDamage = 4;
    private Collider2D[] nearbyColliders = new Collider2D[24];
    public float charaterDiscovery = 1f;
    public CharacterList CharacterList;
    public List<GameObject> Quests;
    public int maxQuests = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (!inn) inn = GameObject.Find("Inn"); // inn = closest inn
        range = GetComponent<CircleCollider2D>();
        InvokeRepeating("DiscoverQuest", 0f, 0.5f);
        InvokeRepeating("DiscoverCharacter", 0f, 0.5f);
        InvokeRepeating("AttackNearby", 0f, .1f);
        CharacterList = GetComponent<CharacterList>();
    }

    // Update is called once per frame
    void Update()
    {
        InnController innController = inn.GetComponent<InnController>();
        if (range.radius < maxRadius) range.radius += 0.1f * Time.deltaTime;
        if ( dicovered >= 100)
        {
            dicovered = 0;
            var EnemyLocationQuest = GetComponent<EnemyLocationQuest>();
            if (!EnemyLocationQuest)
            {
                EnemyLocationQuest = gameObject.AddComponent<EnemyLocationQuest>() as EnemyLocationQuest;
                EnemyLocationQuest.enemyLocation = gameObject;
                EnemyLocationQuest.enabled = true;
                innController.Quests.Add(gameObject);
                range.radius = 0.1f;
            }


        }
    }

    private void DiscoverQuest()
    {
        Quests.RemoveAll(quest => quest == null);
        InnController innController = inn.GetComponent<InnController>();
        if (Random.value * 20f < innController.questDiscovery && range.radius > 2f && Quests.Count < maxQuests) // factor in discoverd into this
        {
            Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * range.radius;
            GameObject quest = Instantiate(questPrefab, pos, Quaternion.identity);
            quest.GetComponent<EnemyGroupQuest>().enemyLocation = gameObject;
            innController.Quests.Add(quest);
            Quests.Add(quest);
            innController.OnListChanged();
        }
    }

    private void DiscoverCharacter()
    {
        if (Random.value * 10f < charaterDiscovery)
        {
            CharacterList.DeadCharacters.RemoveAll(
                c =>
                {
                    c.GetComponent<CharacterController>().Heal();
                    CharacterList.Characters.Add(c);
                    return true;
                }
            );
        }
    }

    private void AttackNearby()
    {
        int length = Physics2D.OverlapCircleNonAlloc(transform.position, range.radius, nearbyColliders, LayerMask.GetMask("Allies"));
        
        if (length > 0)
        {
            // Debug.Log(nearbyColliders[Random.Range(0,length)]);
            var Characters = nearbyColliders[Random.Range(0, length)].gameObject.GetComponent<PartyController>().CharacterList;
            var Enemies = gameObject.GetComponent<CharacterList>();
            Characters.Attack(Enemies);
            Enemies.Attack(Characters);
            if (range.radius > 1f && Enemies.Characters.Count > 0) range.radius -= .2f;
        }
        //for (int i = 0; i < length; i++)
        //{

        //    if (nearbyColliders[i].gameObject.tag == "Party" && Random.value > attackChance)
        //    {

        //        var Characters = nearbyColliders[i].gameObject.GetComponent<PartyController>().CharacterList.Characters;
        //        Characters[Random.Range(0, Characters.Count)].GetComponent<CharacterController>().health -= (int)Random.Range(0, attackDamage); // this all should be repaced with a battle system
        //        if (range.radius > 1f) range.radius -= .5f;
        //    }
                
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            
            dicovered += 0.1f * Time.deltaTime;
        }

    }
}
