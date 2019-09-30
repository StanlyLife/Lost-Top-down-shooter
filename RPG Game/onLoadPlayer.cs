using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Assets.HeroEditor.Common.ExampleScripts;
using System;

public class onLoadPlayer : MonoBehaviour
{
    public CharacterScript player;
    public Inventory playerInv;

    public GameObject neverdie;

    private int counter;



    public void Update() {
        try {
                neverdie = GameObject.FindWithTag("NEVERDIE");
                player = gameObject.GetComponent<CharacterScript>();
                playerInv = neverdie.GetComponent<Inventory>();

        }
        catch {
            Debug.Log("OnloadPlayer cannot find CharacterScript nor Inventory");
        }
    }

    public void Start() {
        neverdie = GameObject.FindWithTag("NEVERDIE");
        player = gameObject.GetComponent<CharacterScript>();
        playerInv = neverdie.GetComponent<Inventory>();
        PlayerGetValuesFromInventory();

        playerInv.Equip();
    }

    public void PlayerGetValuesFromInventory() {
        player.health = playerInv.health;
        player.damage = playerInv.attack;
        player.defence = playerInv.defence;
        player.speed = playerInv.speed;
        player.hasDash = playerInv.hasDash;
        player.maxHealth = playerInv.maxHealth;
        Debug.Log("INVENTORY values -> PLAYER");
    }
}
