using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private CharacterScript playerScript;
    private Vector2 targetPosition;
    private GameObject parentEnemy;

    public float speed;
    public int damage;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<CharacterScript>();
        targetPosition = GameObject.FindWithTag("POSITION").GetComponent<Transform>().position;
    }

    
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > .0001f) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Player") {
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
