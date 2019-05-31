using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterList))]
public abstract class GroupController : MonoBehaviour
{
    public delegate void ListChangedHandler();
    public event ListChangedHandler ListChanged;
    public float Gold;
    public List<GameObject> Quests;
    public CharacterList CharacterList;
    //private List<GameObject> Characters;

    public virtual void Start()
    {
        CharacterList = GetComponent<CharacterList>();
    }

    public virtual void OnListChanged()
    {
        ListChangedHandler handler = ListChanged;
        if( handler != null)
        {
            handler();
        }
    }
}
