using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    public ChangeWeapon ChangeEquipment;
    public Inventory invPlayer;


    private string nameOfItem;
    private string Collection;
    private string typeOfItem;
    private int priceOfItem;
    private int valueProperty; //Attack Or Defence

    //TODO
    //SaveSetup to next scene
        //Change Dummy used
        // if isInstore -> do not move or shoot projectiles
    //Add Image to Inventory
    //Add Image to Equipment
    //Remove Coins from purse
    //Unable To buy multipletimes

    //Leaderboards
    public void Update() {
        try {
            invPlayer = GameObject.FindGameObjectWithTag("NEVERDIE").GetComponent<Inventory>();
        }catch{
            Debug.Log("Cant find NEVERDIE");
        }
    }
    public void setNameOfItem(string inputString) {
        nameOfItem = inputString;
    }    public void setNameOfCollection(string inputString) {
        Collection = inputString;
    }    public void setTypeOfItem(string inputString) {
        typeOfItem = inputString;
    }public void setPriceOfItem(int price) {
        priceOfItem = price;
    }public void setValueProperty(int value) {
        valueProperty = value;
    }
    
    public void BuyThis() {
            StartCoroutine(JustWait());
    }

    IEnumerator JustWait() {
        yield return new WaitForSeconds(0);
        if(invPlayer.addToInventory(nameOfItem, Collection, typeOfItem, priceOfItem, valueProperty)) {
            invPlayer.addCoin(-priceOfItem);
            ChangeEquipment.changeWeapon(nameOfItem, Collection, typeOfItem);
        }
    }
    public void BuySpeed() {
        if (invPlayer.coins >= priceOfItem && invPlayer.speed <= 10) {
            invPlayer.coins -= priceOfItem;
            invPlayer.speed += 1;
        }
    }
    public void BuyHearth() {
        if(invPlayer.coins >= priceOfItem && invPlayer.maxHealth > invPlayer.health) {
            invPlayer.coins -= priceOfItem;
            invPlayer.health += 1;
        }

    }
    public void buyDash() {
        if (invPlayer.coins >= priceOfItem) {
            invPlayer.coins -= priceOfItem;
            invPlayer.hasDash = true;
        }
    }    public void buyEmptyHeart() { //Max health
        if (invPlayer.coins >= priceOfItem) {
            invPlayer.coins -= priceOfItem;
            invPlayer.maxHealth = 10;
            invPlayer.health = 10;
        }
    }    public void buyCoinsDrop() {
        if (invPlayer.coins >= priceOfItem) {
            invPlayer.coins -= priceOfItem;
            invPlayer.hasMagnet = true;
        }
    }


}
