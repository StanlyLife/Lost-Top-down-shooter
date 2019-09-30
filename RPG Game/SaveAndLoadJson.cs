using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveAndLoadJson : MonoBehaviour
{
    public GameObject goPlayer;
    private CharacterScript csPlayer;
    private Inventory invPlayer;
    [Header ("Inventory")]
    //string
    public string helm;
    public string armor;
    public string weapon;
    public string ability;
    [Header ("Variables")]
    //int
    public int health;
    public int coins;

    public void Start() {
        Debug.Log("started SAVEANDLOADJSON");
        DontDestroyOnLoad(gameObject);
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        csPlayer = goPlayer.GetComponent<CharacterScript>();
        invPlayer = gameObject.GetComponent<Inventory>();

        //health = csPlayer.health;
        //coins = csPlayer.coins;
    }

    public void getItems() {
        health = csPlayer.health;
        coins = invPlayer.coins;
    }

    public void getInventory(){
        //invPlayer.getInventory();
    }

    public void SetCoins(int value) {
        //csPlayer.coins = value;
    }
    public void SetHealth(int value) {
        csPlayer.health = value;
    }

    private void Awake() {
        if (File.Exists(Application.dataPath + "/save.txt")) {
            File.Delete(Application.dataPath + "/save.txt");
            Debug.Log("DELETED SAVE.TXT");
        } else {
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Save();
        }else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            Load();
        }
            
    }


    private void Save() {
        SaveObject saveObject = new SaveObject { purse = coins, };
        string jsonFile = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", jsonFile);

        Debug.Log("Save complete");
    }

    private void Load() {
        getInventory();
        if (File.Exists(Application.dataPath + "/save.txt")) {
            print("it does exist");
        } else {
            print("it does NOT exist");
        }
    }

    private class SaveObject {
        public int purse; //money

    }

}
