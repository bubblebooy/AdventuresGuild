using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBackButton : MonoBehaviour
{
    public Button button;
    //public Text nameLabel;

    private ScrollList currentScrollList;
    private ScrollList previousScrollList;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void Setup(ScrollList currentSL, ScrollList previousSL)
    {
        currentScrollList = currentSL;
        previousScrollList = previousSL;
    }

    public void HandleClick()
    {
        currentScrollList.otherInventory.otherInventory = previousScrollList;
        currentScrollList.enabled = false;
        previousScrollList.enabled = true;
        gameObject.SetActive(false);
    }
}
