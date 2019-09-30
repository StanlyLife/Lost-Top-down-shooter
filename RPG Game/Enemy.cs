using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;

    public float attackTime;
    public float timeBetweenAttacks;
    public float stopDistance;
    public float attackSpeed;

    //Type of enemy
    public bool isMeleeEnemy;
    public bool isSummonerEnemy;
    public bool isProjectileEnemy;

    public int damage;

    private ItemDrop ItemDropScript;

    //private Animator attackAnimation;
    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject playerScript;

    private float lifeTime = 40;

    //privates
    private Transform currentPosition;
    private Transform lastPosition;

    private Inventory playerInv;

    //start
    public virtual void Start()
    {
        //playerScript.GetComponent<CharacterScript>();
        fwt();
        lastPosition = currentPosition = gameObject.transform;
        gameObject.GetComponent<Animator>();

        speed += Mathf.RoundToInt(playerInv.waveAttribute/4);
        //damage += Mathf.RoundToInt(playerInv.waveAttribute/3);
        health += Mathf.RoundToInt(playerInv.waveAttributeX5/2);

        Invoke("DestroyEnemy", lifeTime);
    }//end start
    public virtual void Update()
    {//update
        //moveTowardsPlayer();
    }//end update

    public void moveTowardsPlayer() {
        if(!playerScript.GetComponent<CharacterScript>().isDead && !isDead) {
            if (Vector2.Distance(transform.position, player.transform.position) > stopDistance) {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                playAnimation("run");
            } else{
                if (Time.time >= attackTime && !playerScript.GetComponent<CharacterScript>().isDead && isMeleeEnemy) {
                    //mele enemy attack
                    meleeAttack();
                }else if(Time.time >= attackTime && !playerScript.GetComponent<CharacterScript>().isDead && isSummonerEnemy) {
                    //summoner enemy attack
                } else if(Time.time >= attackTime && !playerScript.GetComponent<CharacterScript>().isDead && isProjectileEnemy) {
                    //projectile enemy attack
                    //playAnimation("attack1");
                }
            }
        }
    }
    public void meleeAttack(){//attack animation
        if(!isDead) {
            gameObject.GetComponent<Animator>().SetTrigger("attack1");
            attackTime = Time.time + timeBetweenAttacks;
            StartCoroutine(Attack());
        }
    }

    public void TakeDamage(int damageAmount)
    {//take damage
        health -= damageAmount;
        //check if Enemy dead
        if (health <= 0) {
            EnemyDie();
        }

        if(!isDead) { //do not play animation of enemy is dead 
            if (Random.Range(-1, 1) > 0) {
                gameObject.GetComponent<Animator>().SetTrigger("hurt1");
            } else {
                gameObject.GetComponent<Animator>().SetTrigger("hurt2");
            }
        }

    }//end take damag
    
    private float timeBetweenAnimation;
    private float lastAnimationTime;
    public void playAnimation(string thisAnimation)
    {
        if (Time.time >= timeBetweenAnimation) {
            gameObject.GetComponent<Animator>().SetTrigger(thisAnimation);
            timeBetweenAnimation = Time.time + gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
    }
    public void flipEnemy()
    {
        if (player != null && !isDead) {
            if (player.transform.position.x > gameObject.transform.position.x) {
                gameObject.transform.rotation = Quaternion.identity;
            } else {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    IEnumerator Attack()
    {//start IEnumerator
        playerScript.GetComponent<CharacterScript>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.transform.position;

        float percent = 0;
        while (percent <= 1) {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }//end IEnumerator
    //find with tag
    private void fwt()
    {
        try {
            player = GameObject.FindWithTag("POSITION");
            playerScript = GameObject.FindWithTag("Player");
            ItemDropScript = gameObject.GetComponent<ItemDrop>();
            playerInv = GameObject.FindGameObjectWithTag("NEVERDIE").GetComponent<Inventory>();
        }
        catch {
            print("Did not find player position");
        }
    }

    public void EnemyDie() {
            isDead = true;
            gameObject.GetComponent<Animator>().SetBool("die",true);
            StartCoroutine(dropItem());
    }

    IEnumerator dropItem()
    {
        ItemDropScript.goldBarDrop();
        ItemDropScript.goldCoinDrop();

        yield return new WaitForSeconds(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length*2);
        Destroy(gameObject);
    }


    void DestroyEnemy() {
        try {
            Destroy(gameObject);
        }
        catch {
            print("No particle on impact");
        }
    }

}//end class
