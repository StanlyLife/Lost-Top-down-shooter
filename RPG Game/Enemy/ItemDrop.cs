using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Enemy EnemyScript;
    private Inventory playerInv;

    [Header ("Coins")]
    public GameObject coin;
    public int chanceOfCoin;
    public int maxCoins;
    public int minCoins;

    [Header ("Gold Bar")]
    public GameObject goldBar;
    public int chanceOfGoldBar;
    public int maxGoldBar;
    public int minGoldBar;

    private Transform randomTransform;

    public void Start()
    {
        EnemyScript = gameObject.GetComponent<Enemy>();
        playerInv = GameObject.FindGameObjectWithTag("NEVERDIE").GetComponent<Inventory>();

        maxGoldBar = Mathf.RoundToInt(playerInv.waveAttribute / 4);
        maxCoins = Mathf.RoundToInt(playerInv.waveAttribute*2.5f);
    }

    private Vector2 CreateRandomTransform()
    {
        float dieX = gameObject.transform.position.x;
        float dieY = gameObject.transform.position.y;

        float spawnPointX = Random.Range(dieX - 0.7f, dieX + 0.7f);
        float spawnPointY = Random.Range(dieY - 0.7f, dieY + 0.7f);

        Vector2 spawnPosition = new Vector2(spawnPointX,spawnPointY);
        return spawnPosition;
    }

    public void goldBarDrop()
    {
        if (chanceOfGoldBar > 100) { chanceOfGoldBar = 100; };

        int randomInt = Random.Range(0,100);
        int randomAmountDropped = Random.Range(minGoldBar, maxGoldBar);

        if (randomInt < chanceOfGoldBar){
            for(int i = 0; i != randomAmountDropped; i++) {
                Instantiate(goldBar, CreateRandomTransform(), Quaternion.identity);
            }
        }
    }
    public void goldCoinDrop()
    {
        if (chanceOfCoin > 100) { chanceOfCoin = 100; };

        int randomInt = Random.Range(0,100);
        int randomAmountDropped = Random.Range(minCoins, maxCoins);

        if (randomInt < chanceOfCoin){
            for(int i = 0; i != randomAmountDropped; i++) {
                Instantiate(coin, CreateRandomTransform(), Quaternion.identity);
            }
        }
    }
}
