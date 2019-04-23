using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyScrollList : ScrollList
{
    public GameObject backButton;
    public override void AddButtons()
    {
        base.AddButtons();
        if (!target) return;
        var Parties = target.GetComponent<InnController>().Parties;
        for (int i = 0; i < Parties.Count; i++)
        {
            var party = Parties[i];
            GameObject newButton = Instantiate(button); //should be from a SimpleObjectPool
            newButton.transform.SetParent(transform);

            InventoryPartyButton inventoryPartyButton = newButton.GetComponent<InventoryPartyButton>();
            inventoryPartyButton.Setup(party, this);
        }
    }

    public void SelectQuest(GameObject quest)
    {
        QuestScrollList questScrollList = GetComponent<QuestScrollList>();
        questScrollList.target = quest;
        otherInventory.otherInventory = questScrollList;
        questScrollList.otherInventory = otherInventory;
        questScrollList.enabled = true;
        backButton.SetActive(true);
        backButton.GetComponent<InventoryBackButton>().Setup(questScrollList,this);
        this.enabled = false;
    }
}
