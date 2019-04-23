using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public GameObject Canvas;
    private bool isShowing;
    private void OnMouseUp()
    {
        isShowing = !isShowing;
        if (isShowing)
        {
            Canvas.transform.GetChild(0).GetComponentInChildren<QuestScrollList>().target = gameObject;
            Canvas.transform.GetChild(0).GetComponentInChildren<QuestScrollList>().otherInventory = Canvas.transform.GetChild(1).GetComponentInChildren<PartyScrollList>();
            Canvas.transform.GetChild(1).GetComponentInChildren<PartyScrollList>().target = gameObject;
            Canvas.transform.GetChild(1).GetComponentInChildren<PartyScrollList>().otherInventory = Canvas.transform.GetChild(0).GetComponentInChildren<QuestScrollList>();
            
            //var parties = GetComponent<InnController>().Parties;
            //if (parties.Count > 0)
            //{
            //    Canvas.transform.GetChild(1).GetComponentInChildren<QuestScrollList>().target = GetComponent<InnController>().Parties[0];
            //} else
            //{
            //    Canvas.transform.GetChild(1).GetComponentInChildren<QuestScrollList>().target = null;
            //}
            
        }
        Canvas.SetActive(isShowing);
    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            isShowing = false;
            Canvas.SetActive(isShowing);
        }      
    }

}
