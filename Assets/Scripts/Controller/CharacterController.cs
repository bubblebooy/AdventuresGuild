using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    //public CharacterClass class;
    //public CharacterRace race;
    public int health = 10;
    public int maxHealth = 10;
    public int attack = 90;
    public int damage = 2;
    public int defence = 65;
    public int experience = 0;
    public int levelUpExperience = 100;
    public int sneak = 50;
    public int smarts = 50;
    public int strength = 50;

    void Start()
    {
        name = RandomWord.Adverb(); // race.randomName
        int[] randList = RandomList.FixedSum(3, 50);
        sneak += randList[0];
        smarts += randList[1];
        strength += randList[2];
        Heal();
    }

    private void LevelUp()
    {
        experience = 0;
        levelUpExperience *= 2;
        maxHealth += Random.Range(2, 8);
        health = maxHealth;
        if (Random.value > .5)
        {
            attack += Random.Range(2, 8);
        }
        else if (Random.value > .5)
        {
            defence += Random.Range(2, 8);
        }
        else
        {
            damage += 1;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if (experience >= levelUpExperience) LevelUp();
        //if (health <= 0) Destroy(gameObject);
    }

    public void Heal()
    {
        health = maxHealth;
    }

}
