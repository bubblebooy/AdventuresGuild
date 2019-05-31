using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PartyController : GroupController
{
    private GameObject parties;
    public List<MonoBehaviour> CurrentBehaviours;
    public float idleTime;
    public float fatigue;
    public float fatigueRate = 0.1f;
    private GameObject atInn;
    private GameObject atQuest;
    // Start is called before the first frame update
    public override void Start()
    {
        parties = GameObject.Find("Parties");
        transform.SetParent(parties.transform);
        name = RandomWord.Noun() + " " + transform.parent.childCount.ToString();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!atInn) fatigue += fatigueRate * Time.deltaTime;
        idleTime += Time.deltaTime;
        CurrentBehaviours.RemoveAll(behaviour => behaviour == null);  // should i do this less often?
        Quests.RemoveAll(Quest => Quest == null);                     // should i do this less often?
        if (CurrentBehaviours.Count != 0)
        {
            idleTime = 0;
            return;
        }
        if (Quests.Count > 0 && (!atInn || fatigue <=0) && (!atQuest) && CharacterList.Characters.Count >= 4)
        {
            idleTime = 0;
            Travel travel = gameObject.AddComponent<Travel>();
            GameObject Quest = Quests[0];
            Quests.ForEach(
                q => {
                    if (Vector3.Distance(q.transform.position, transform.position) < Vector3.Distance(Quest.transform.position, transform.position))
                    {
                        Quest = q;
                    }          
                }
            );
            travel.targetLocation = Quest.transform.position;
            CurrentBehaviours.Add(travel);
        } else if (!atInn && !atQuest && idleTime > 1f)
        {
            idleTime = 0;
            Travel travel = gameObject.AddComponent<Travel>();
            travel.targetLocation = new Vector3(0f,0f,0f);
            CurrentBehaviours.Add(travel);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Inn") atInn = collision.gameObject;
        if (collision.gameObject.tag == "Quest") atQuest = collision.gameObject;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inn") atInn = null;
        if (collision.gameObject.tag == "Quest") atQuest = null;

    }
}

//Travel travel = gameObject.AddComponent<Travel>();
//float distance = road.polygonCollider.Distance(GetComponent<Collider2D>()).distance + 0.2f;
//int closetPoint = System.Array.FindIndex(road.polygonCollider.points, point => Vector3.Distance(road.transform.TransformPoint(point), transform.position) < distance);
//Debug.Log(road.polygonCollider.points[closetPoint]);
//travel.targetLocation = road.transform.TransformPoint(road.polygonCollider.points[closetPoint]); //road.transform.TransformPoint
//CurrentBehaviours.Add(travel);    


//Spline spline = road.spline;
//Vector3 target;
//float t = 0.3f;
//Vector3 P0 = spline.GetPosition(4);
//Vector3 P1 = spline.GetRightTangent(4);
//Vector3 P2 = spline.GetLeftTangent(3);
//Vector3 P3 = spline.GetPosition(3);
//target = Mathf.Pow((1f - t), 3) * P0 + 3.0f*(1f - t)*(1f - t)*t*P1 + 3.0f * (1f - t) * t * t * P2 + Mathf.Pow(t, 3)*P3;