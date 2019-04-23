using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroupController : MonoBehaviour
{
    public delegate void ListChangedHandler();
    public event ListChangedHandler ListChanged;
    public float Gold;
    public List<GameObject> Quests;

    protected virtual void OnListChanged()
    {
        ListChangedHandler handler = ListChanged;
        if( handler != null)
        {
            handler();
        }
    }
}
