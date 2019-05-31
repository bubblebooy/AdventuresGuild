using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public List<GameObject> Characters;
    public List<GameObject> DeadCharacters;

    private void Update()
    {
        //Characters.RemoveAll(character => character == null);
        Characters.RemoveAll(character =>
            {
                if (character.GetComponent<CharacterController>().health <= 0)
                {
                    DeadCharacters.Add(character);
                    return true;
                }
                return false;
            }
        );
    }
    public void Heal() // overload this to heal a certain amount
    {
        Characters.ForEach(
            c =>
            {
                var contoller = c.GetComponent<CharacterController>();
                contoller.Heal();
            }
        );
    }
    public void GiveXp(int xp)
    {
        Characters.ForEach(c => c.GetComponent<CharacterController>().experience += xp);
    }
    public void Attack(CharacterList enemyList)
    {
        if (enemyList.Characters.Count == 0 || Characters.Count == 0) return;
        Characters.ForEach(
            c =>
            {  
                var contoller = c.GetComponent<CharacterController>();
                
                var enemyController = enemyList.Characters[Random.Range(0, enemyList.Characters.Count)].GetComponent<CharacterController>();
                if (Random.Range(0, contoller.attack) > Random.Range(0, enemyController.defence))
                {
                    enemyController.health -= contoller.damage;
                }
            }
        );
    }
}
