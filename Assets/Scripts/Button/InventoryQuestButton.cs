using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryQuestButton : MonoBehaviour
{
    public Button button;
    private GameObject quest;
    private QuestScrollList scrollList;
    public Text goldLabel;
    public Text distanceLabel;
    public Text nameLabel;
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void Setup(GameObject currentQuest, QuestScrollList currentScrollList)
    {
        quest = currentQuest;
        QuestController questController = quest.GetComponent<QuestController>();
        goldLabel.text = "Gold: " + questController.gold;
        distanceLabel.text = "Distance: ";
        nameLabel.text = quest.gameObject.name;
        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        scrollList.TryTransnferQuest(quest);
    }
}
