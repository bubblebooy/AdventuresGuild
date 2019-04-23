using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWord : MonoBehaviour
{
    public TextAsset adverbs;
    public TextAsset adjective;
    public TextAsset verbs;
    public TextAsset nouns;
    private static string[] adverbsList;
    private static string[] adjectiveList;
    private static string[] verbsList;
    private static string[] nounsList;

    private void Awake()
    {
        if(adverbs != null)
        {
            adverbsList = adverbs.text.Split('\n');
        }
        if (adjective != null)
        {
            adjectiveList = adjective.text.Split('\n');
        }
        if (verbs != null)
        {
            verbsList = verbs.text.Split('\n');
        }
        if (nouns != null)
        {
            nounsList = nouns.text.Split('\n');
        }
    }
    public static string Word()
    {
        string word;
        switch ((int)Random.Range(0,3))
        {
            case 0:
                Debug.Log("0");
                word = Adverb();
                break;
            case 1:
                Debug.Log("1");
                word = Adjective();
                break;
            case 2:
                Debug.Log("2");
                word = Verb();
                break;
            case 3:
                Debug.Log("3");
                word = Noun();
                break;
            default:
                word = null;
                break;
        }

        return word;
    }
    public static string Adverb()
    {
        if (adverbsList == null) return null;
        return adverbsList[Random.Range(0, adverbsList.Length)];
    }
    public static string Adjective()
    {
        if (adjectiveList == null) return null;
        return adjectiveList[Random.Range(0, adjectiveList.Length)];
    }
    public static string Verb()
    {
        if (verbsList == null) return null;
        return verbsList[Random.Range(0, verbsList.Length)];
    }
    public static string Noun()
    {
        if (nounsList == null) return null;
        return nounsList[Random.Range(0, nounsList.Length)];
    }
}
