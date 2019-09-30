using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CoinTextManager : MonoBehaviour
{
    public string variable;

    private string coins;
    private Inventory inventory;
    Text coinsText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        try {

        coinsText = GetComponent<Text>();
        inventory = GameObject.FindWithTag("NEVERDIE").GetComponent<Inventory>();
        if(variable.ToLower() == "coins") {
            coinsText.text = inventory.coins.ToString();
        }
        if (variable.ToLower() == "health") {
            coinsText.text = inventory.health.ToString() + "/" + inventory.maxHealth;
        }
        if (variable.ToLower() == "defence") {
            coinsText.text = inventory.defence.ToString();
        }
        if (variable.ToLower() == "damage") {
            coinsText.text = inventory.attack.ToString();
        }if (variable.ToLower() == "wave") {
                int WaveNumber = inventory.waveAttributeX5 - 1;
            coinsText.text = WaveNumber.ToString();
        }if (variable.ToLower() == "speed") {
            coinsText.text = inventory.speed.ToString();
        }
        }
        catch {
            print("trying to fwt again");
        }
    }
}

