using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public int damage;
    //public Vector2 mousePos = Input.mousePosition;

    public GameObject particleExplotion;
    private Enemy enemyScript;
    private CharacterScript playerScript;

    void Start(){
        Invoke("DestroyProjectile", lifeTime);
        if(playerScript == null) {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        }damage = playerScript.damage;
    }


    void Update(){
        transform.Translate(Vector2.one * Time.deltaTime * speed);
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void DestroyProjectile(){
        try {
        Instantiate(particleExplotion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        }catch {
            print("No particle on impact");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {



        if (collision.tag == "Player") {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
            enemyScript = collision.GetComponent<Enemy>();

        if (collision.tag == "bg") {
            Destroy(gameObject);
            DestroyProjectile();
        }else if (collision.tag == "Enemy" && !enemyScript.isDead) {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
            DestroyProjectile();
        }
    }

}
