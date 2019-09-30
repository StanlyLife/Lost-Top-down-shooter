using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private static List<List<string>> inventoryArray = new List<List<string>>();
    private static List<List<int>> inventoryIntArray = new List<List<int>>();
    private static List<string> inventoryArrayCounter = new List<string>();

    public int waveAttribute = 0;
    public int waveAttributeX5 = 0;

    public int health = 10;
    public int maxHealth = 10;

    public int attack = 1;
    public float speed = 5;
    public int defence = 0;

    public int coins;
    public bool hasMagnet = false;
    public bool hasDash = false;

    private ChangeWeapon ChangeEquipment;
    private CharacterScript playerScript;
    private onLoadPlayer PlayerLoad;
    

    public GameObject player;

    public bool gameStart = false;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        //PlayerLoad.PlayerGetValuesFromInventory();
    }

    public void fwt() {
        try {
        player = GameObject.FindGameObjectWithTag("Player");

        PlayerLoad = player.GetComponent<onLoadPlayer>();
        playerScript = player.GetComponent<CharacterScript>();
        ChangeEquipment = player.GetComponent<ChangeWeapon>();
        }
        catch {
            Debug.Log("Inventory could not find with tag");
        }
    }


    public void InventoryGetPlayerValues() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        if(playerScript != null) {
            health = playerScript.health;
            maxHealth = playerScript.maxHealth;
            attack = playerScript.damage;
            speed = playerScript.speed;
            defence = playerScript.defence;
            Debug.Log("PAYER -> INVENTORY");
        } else {
            Debug.Log("PLAYERSCRIPT == NULL");
        }
    }



    public void Equip() {
        ChangeEquipment = GameObject.FindGameObjectWithTag("Character").GetComponent<ChangeWeapon>();

        foreach (List<string> equipment in inventoryArray) {
            string nameOfItem = equipment[0];
            string collection = equipment[1];
            string typeOfItem = equipment[2];
            ChangeEquipment.changeWeapon(nameOfItem, collection, typeOfItem);
            foreach (List<int> valueProperty in inventoryIntArray) {
                if(typeOfItem.ToLower() == "sword") {
                    attack = valueProperty[1];
                }else if(typeOfItem.ToLower() == "armour"){
                    //add defence, but did it in buy item instead
                }
            }
        }
    }


    //LOAD EQUIPPED TO PLAYER ON MAIN SCENE
    public void removeFromInventory(string itemToRemove) {
        int index = inventoryArrayCounter.IndexOf(itemToRemove);
        inventoryArray.RemoveAt(index);
    }



    private bool ownsHood = false;
    private bool ownsShield = false;

    private int armourDefence;

    public bool addToInventory(string nameOfItem,string collection,string typeOfItem, int price,int valueProperty) {

        if (price <= coins) {
            //add to InventoryArray
            List<string> clothing = new List<string>();
            List<int> clothingIntValues = new List<int>();
            clothing.Add(nameOfItem);
            clothing.Add(collection);
            clothing.Add(typeOfItem);

            clothingIntValues.Add(price);
            clothingIntValues.Add(valueProperty);

            
            inventoryArray.Add(clothing);
            inventoryIntArray.Add(clothingIntValues);

            inventoryArrayCounter.Add(nameOfItem); //to find index for removing items

            if(typeOfItem.ToLower() == "sword") {
                attack = valueProperty;
            } else if(typeOfItem.ToLower() == "armour") {
                defence -= armourDefence;
                armourDefence = valueProperty;
                defence += valueProperty;
            }else if(typeOfItem.ToLower() == "shield" && !ownsShield) {
                    defence += valueProperty;
            }else if(typeOfItem.ToLower() == "helm" && !ownsHood) {
                    defence += valueProperty;
            }


            return true;
        } else {
            Debug.Log("Not enough coins");
            return false;
        }
    }

    public void addCoin(int value) {
        coins += value;
    }



}
