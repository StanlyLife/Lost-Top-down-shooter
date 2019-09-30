using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    private Vector2 SpawnPoint;
    private Vector2 targetPosition;
    private Animator anim;

    //summoning variables
    public float timeBetweenSummons;
    private float summonTime;
    public Enemy summonThis;


    public override void Start()
    {
        base.Start();
        //SpawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint")[Random.Range(0,11)].GetComponent<Vector2>();
        SpawnPoint = gameObject.transform.position;
        isSummonerEnemy = true;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetPosition = GameObject.FindGameObjectsWithTag("SpawnPoint")[Random.Range(0, 11)].transform.position;
        //targetPosition = new Vector2(randomX, randomY);
    }

    public override void Update()
    {
        if(!isDead) {
            flipEnemy();
            moveToLocation();
        }
    }

    private void moveToLocation()
    {
        if(Vector2.Distance(transform.position, targetPosition) > 0.5f) { //go to location
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            playAnimation("run");
        }else {
            if(Time.time >= summonTime) { //summon this
                playAnimation("skill1");
                summonTime = Time.time + timeBetweenSummons;
            }
        }
    }
    public void summon()
    {
        if(!playerScript.GetComponent<CharacterScript>().isDead) {
            Instantiate(summonThis, transform.position, transform.rotation);
        }
    }
}
