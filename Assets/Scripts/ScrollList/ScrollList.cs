using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScrollList : MonoBehaviour
{
    public GameObject target;
    //public Transform contentPanel;
    public ScrollList otherInventory;
    public GameObject button;


    private void OnEnable()
    {
        RefreshDisplay();
        if (!target) return;
        target.GetComponent<GroupController>().ListChanged += RefreshDisplay;
    }
    private void OnDisable()
    {
        if (!target) return;
        target.GetComponent<GroupController>().ListChanged -= RefreshDisplay;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void RefreshDisplay()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        AddButtons();
    }

    public virtual void AddButtons()
    {
        if (!target) return;
    }


}
