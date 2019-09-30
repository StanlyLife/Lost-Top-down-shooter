using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectile;
    public Transform startPoint;
    public float timeBetweenShots;
    private CharacterScript player;

    private float shotTime;
    public float rotor;


    void Start()
    {
        //projectile = GameObject.FindWithTag("Weapon");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }
    public Quaternion a;
    void Update()
    {
        if(Input.GetMouseButton(0) && !player.isDead) //fire projectile
        {
            if(Time.time >= shotTime)
            {
                Instantiate(projectile, startPoint.position, transform.rotation);
                //transform.Rotate(0, 20 * Time.deltaTime, 90);
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}
