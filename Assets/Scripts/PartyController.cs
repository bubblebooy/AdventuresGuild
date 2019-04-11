using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public float Gold;
    public List<GameObject> Quests;
    public List<MonoBehaviour> CurrentBehaviours;
    // Start is called before the first frame update
    void Start()
    {
        Travel travel = gameObject.AddComponent<Travel>();
        travel.targetLocation = Quests[0];
        CurrentBehaviours.Add(travel);    
    }

    // Update is called once per frame
    void Update()
    {
        CurrentBehaviours.RemoveAll(behaviour => behaviour == null);
        // if CurrentBehaviours = 0
        //      if quest finnish quest
        //      if at inn get new quest
        //      else travle to inn
    }
}
