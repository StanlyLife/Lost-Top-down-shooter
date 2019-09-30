using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {


    public override void Start() {
        isMeleeEnemy = true;
        base.Start();
        //print("started MeleeEnemey for " + gameObject.name);
    }

    public override void Update() {
        MeleeMTP();
        flipEnemy();
    }


    public void MeleeMTP() {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player.GetComponent<CharacterScript>().isDead && !isDead) {
            if (Vector2.Distance(transform.position, player.transform.position) > stopDistance) {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                playAnimation("run");
            } else {
                if (Time.time >= attackTime && !playerScript.GetComponent<CharacterScript>().isDead && isMeleeEnemy) {
                    //mele enemy attack
                    meleeAttack();
                }
            }
        }
    }
}