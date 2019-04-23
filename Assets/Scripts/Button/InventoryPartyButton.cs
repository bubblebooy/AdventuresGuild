using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPartyButton : MonoBehaviour
{
    public Button button;
    private GameObject party;
    private PartyScrollList scrollList;
    public Text goldLabel;
    public Text nameLabel;
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void Setup(GameObject currentParty, PartyScrollList currentScrollList)
    {
        party = currentParty;
        PartyController partyContoller = party.GetComponent<PartyController>();
        goldLabel.text = "Gold: " + partyContoller.Gold.ToString("#0.00");
        nameLabel.text = party.gameObject.name;
        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        scrollList.SelectQuest(party);
    }
}
