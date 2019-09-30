using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesScript : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            collision.GetComponent<CharacterScript>().TakeDamage(1);
        }
        if(collision.tag == "Enemy") {
            if(Random.Range(0,100) <= 10) {
                collision.GetComponent<Enemy>().TakeDamage(1);
            }
        }
    }
}
