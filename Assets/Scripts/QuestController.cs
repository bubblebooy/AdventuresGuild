using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private GameObject quests;
    public int gold;
    public float completion;

    // Start is called before the first frame update
    void Awake()
    {
        quests = GameObject.Find("Quests");
        gold = (int)(Random.value * 10f);
        completion = 2 + Random.value * 8;
        gameObject.name = RandomWord.Adjective() + " " + RandomWord.Noun();
        transform.SetParent(quests.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Party")
        {
            completion -= 1 * Time.fixedDeltaTime;
            if (completion <= 0)
            {
                collision.gameObject.GetComponent<PartyController>().Gold += gold;
                Destroy(gameObject);
            }
        }

    }
}
