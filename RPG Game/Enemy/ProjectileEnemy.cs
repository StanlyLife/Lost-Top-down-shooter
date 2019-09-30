using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    private Animator PjEnemyAnimation;
    public GameObject projectile;
    public Transform projectileDestination;
    public GameObject mouth;
    

    public override void Start()
    {
        base.Start();

        base.Start();
        isProjectileEnemy = true;
        PjEnemyAnimation = GetComponent<Animator>();
        //mouth = GameObject.FindWithTag("DragonMouth");
        
    }

    public override void Update()
    {
        PjEnemyMove();
        flipEnemy();
    }

    public void shootProjectile()
    {

        if(!isDead && Time.time > attackTime) {
            try {
                Vector2 direction = player.GetComponent<Transform>().position - mouth.transform.position;
                projectileDestination = player.transform;
                attackTime = Time.time + timeBetweenAttacks;
                playAnimation("attack1");
                //mouth = GameObject.FindWithTag("DragonMouth");

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                projectileDestination.rotation = rotation;
                Instantiate(projectile, mouth.transform.position, projectileDestination.transform.rotation);
        }catch (Exception e) {
            //Destroy(gameObject);
            Debug.Log("Error in shootProjectile, projectile enemy: " + e);
        }
        } else {
            
        }
    }

    public void PjEnemyMove() {
        if (Vector2.Distance(transform.position, player.transform.position) > stopDistance) {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            playAnimation("run");
        }else if (!playerScript.GetComponent<CharacterScript>().isDead) {
            //projectile enemy attack
            shootProjectile();
        }
    }
}
