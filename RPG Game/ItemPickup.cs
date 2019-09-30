using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject player;
    private Inventory playerScript;
    public int value;

    private bool coinMagnet;
    private int speed = 10;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = GameObject.FindWithTag("NEVERDIE").GetComponent<Inventory>();
    }

    public void Update()
    {
        playerMagnet();
    }

    private void playerMagnet()
    {
        coinMagnet = playerScript.hasMagnet;
        if (coinMagnet) {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            playerScript.addCoin(value);
            Destroy(gameObject);
        } else {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
