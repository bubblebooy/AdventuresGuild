using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScrollList : ScrollList
{
    public override void AddButtons()
    {
        base.AddButtons();
        if (!target) return;
        var Quests = target.GetComponent<GroupController>().Quests;
        for (int i = 0; i < Quests.Count; i++)
        {
            var quest = Quests[i];
            GameObject newButton = Instantiate(button); //should be from a SimpleObjectPool
            newButton.transform.SetParent(transform);

            InventoryQuestButton inventoryQuestButton = newButton.GetComponent<InventoryQuestButton>();
            inventoryQuestButton.Setup(quest, this);
        }
    }

    public void TryTransnferQuest(GameObject quest)
    {
        if (!otherInventory.target) return;
        if (otherInventory.GetType() == typeof(QuestScrollList))
        {
            ((QuestScrollList)otherInventory).AddQuest(quest);
            RemoveQuest(quest);
            RefreshDisplay();
            otherInventory.RefreshDisplay();
        } else if (otherInventory.GetType() == typeof(PartyScrollList))
        {
            Debug.Log("bleaa");
            var parties = otherInventory.target.GetComponent<InnController>().Parties;
            if (parties.Count > 0)
            {
                parties[Random.Range(0, parties.Count)].GetComponent<GroupController>().Quests.Add(quest);
                RemoveQuest(quest);
                RefreshDisplay();
            }  
        }
    }

    public void AddQuest(GameObject quest)
    {
        target.GetComponent<GroupController>().Quests.Add(quest);
    }
    private void RemoveQuest(GameObject quest)
    {
        //int i = target.GetComponent<GroupController>().Quests.FindIndex(q => q == quest);
        //target.GetComponent<GroupController>().Quests.RemoveAt(i);
        target.GetComponent<GroupController>().Quests.Remove(quest);
    }
}
