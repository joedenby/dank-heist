using UnityEngine;

public class Character : MonoBehaviour
{
    public InventoryManager inventoryManager;

    CharacterLocomotion player;
    Animator animator;

    //player stats
    public int health;
    public bool dead => health <= 0;
    public bool safe = false;
    public int money;
    public int noOfdeaths;
    public string playerName;

    //equipment
    public int ammo;
    public int grenades;
    public GameObject gun;
    public Transform[] weapons;


    public void Start()
    {
        if (transform.CompareTag("Player"))
            GameManager.Players.Add(transform);
    }

    public void Damage()
    {
        health--;
        animator.SetBool("isDead", dead);
        if (dead)
        {
            noOfdeaths++;
            player.speed = 0f;
        }
    }

    public void ResetCharacter() {
        health = 100;
        animator?.SetBool("isDead", dead);
    }


    /**
     * returns player health
     * sets player status to dead if health == 0
     */
    int GetHealth()
    {
        return health;
    }


    /**
    *Returns plaurt ammo count
    *secondary weapon e.g pistol wont have ammo count
    */
    int GetAmmo()
    {
        return ammo;
    }

    /**
    *Returns player money count
    */
    int GetMoney()
    {
        return money;
    }

    /**
     * Changes player selected weapon
     */
    public void ChangeWeapon(int num)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
            {
                weapons[i].gameObject.SetActive(true);
                gun = weapons[i].gameObject;
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }

    /**
     * Identifies paramaters tag
     * checks if tag is equal to "Weapon"
     * 
     * @param GameObject the item to be analysed
     */
    public bool IsWeapon(GameObject item)
    {
        if (item.tag == "Weapon")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}