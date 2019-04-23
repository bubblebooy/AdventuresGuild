using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnController : GroupController
{
    public GameObject questPrefab;
    public GameObject partyPrefab;
    //public List<GameObject> Quests;
    public List<GameObject> Parties;
    public float questDiscovery = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value * 100f < questDiscovery)
        {
            Vector2 pos = new Vector2(50f * Random.value - 25f, 50f * Random.value - 25f);
            GameObject quest = Instantiate(questPrefab, pos, Quaternion.identity);
            Quests.Add(quest);
            OnListChanged();
        }
        if ( Gold > 3)
        {
            Gold -= 3;
            GameObject newParty = Instantiate(partyPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            questDiscovery += 1;
            Parties.Add(collision.gameObject);
            OnListChanged();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            questDiscovery -= 1;
            Parties.Remove(collision.gameObject);
            OnListChanged();
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("At inn!!");
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
        }
    }

}
