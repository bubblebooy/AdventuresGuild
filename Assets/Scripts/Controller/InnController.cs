using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnController : GroupController
{
    public List<GameObject> questPrefabs;
    public GameObject partyPrefab;
    public GameObject charaterPrefab;
    //public List<GameObject> Quests;
    public List<GameObject> Parties;
    public float questDiscovery = 0.25f;
    public int maxQuests = 5;
    public float charaterDiscovery = 0.5f;
    // Start is called before the first frame update
    public override void Start()
    {
        InvokeRepeating("DiscoverQuest", 0f, 0.5f);
        InvokeRepeating("DiscoverCharacter", 0f, 0.5f);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Gold > 3 && CharacterList.Characters.Count > 4)
        {
            Gold -= 3;
            GameObject newParty = Instantiate(partyPrefab, transform.position, Quaternion.identity);
            maxQuests += 2;
        }
    }

    private void DiscoverQuest()
    {
        if (Random.value * 10f < questDiscovery && Quests.Count < maxQuests)
        {
            Vector2 pos = new Vector2(50f * Random.value - 25f, 50f * Random.value - 25f);
            GameObject quest = Instantiate(questPrefabs[Random.Range(0, questPrefabs.Count)], pos, Quaternion.identity);
            Quests.Add(quest);
            OnListChanged();
        }
    }
    private void DiscoverCharacter()
    {
        if (Random.value * 10f < charaterDiscovery)
        {
            GameObject character = Instantiate(charaterPrefab, transform);
            CharacterList.Characters.Add(character);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            questDiscovery += 1;
            Parties.Add(collision.gameObject);
            OnListChanged();
            collision.gameObject.GetComponent<PartyController>().CharacterList.Heal();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            questDiscovery -= 1;
            Parties.Remove(collision.gameObject);
            OnListChanged();
            collision.gameObject.GetComponent<PartyController>().CharacterList.Heal();
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("At inn!!");
        if (collision.gameObject.tag == "Party")
        {
            PartyController partyController = collision.GetComponent<PartyController>();
            if(partyController.fatigue > 0) partyController.fatigue -= 5 * Time.deltaTime;
            if (partyController.Gold > 0)
            {
                partyController.Gold -= 0.5f * Time.deltaTime;
                Gold += 0.5f * Time.deltaTime;
            }
            if (Quests.Count > 0 && collision.GetComponent<PartyController>().Quests.Count == 0 )
            {
                collision.GetComponent<PartyController>().Quests.Add(Quests[0]);
                Quests.Remove(Quests[0]);
                OnListChanged();
            }
            if (CharacterList.Characters.Count > 0 && collision.GetComponent<PartyController>().CharacterList.Characters.Count < 4)
            {
                collision.GetComponent<PartyController>().CharacterList.Characters.Add(CharacterList.Characters[0]);
                CharacterList.Characters[0].transform.parent = collision.transform;
                CharacterList.Characters.Remove(CharacterList.Characters[0]);
            }
        }
    }

}
