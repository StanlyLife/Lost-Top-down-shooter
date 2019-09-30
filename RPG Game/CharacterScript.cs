using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.EditorScripts;
using HeroEditor.Common;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.Common.ExampleScripts;


public class CharacterScript : MonoBehaviour
{

    //publics
    [Header("Player Variables")]
    public float speed;
    public int health;
    public int maxHealth;
    public int damage;
    public int defence;


    //privates
    public Rigidbody2D rigBod;
    private Vector2 moveAmount;

    //[HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public bool hasDash = false;
    private float lastDash;
    private float timeBetweenDash = 2;

    //run animation
    private Animator charAnimation;

    [Header("Scripts")]
    public ChangeWeapon changeWeapon;
    public RuntimeSetup rts;
    public Character Character;
    public Inventory invPlayer;
    public onLoadPlayer onLoad;
    public TakeDamageScript takeDamageAnimation;

    [Header("UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite blackHeart;

    private float timeBetweenAnimation;
    private Stack<string> myStack = new Stack<string>();


    void Start(){
        rts = GameObject.FindWithTag("Character").GetComponent<RuntimeSetup>();
        Character = GameObject.FindWithTag("Character").GetComponent<Character>();
        //rigBod = GetComponent<Rigidbody2D>();
        onLoad = GetComponent<onLoadPlayer>();
        UpdateHealthUI(health);
    }

    void Update() {

        move();
    }  

    public void move() {
        if (!isDead) {

            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

            moveAmount = moveInput.normalized * speed;
            if (Input.GetKeyDown("space") && hasDash) {
                Dash();
            }
            //Run Animation
            if (moveInput != Vector2.zero) {
                playAnimation("Run");
            } else {
                playAnimation("Idle");
            }
        } else if (isDead) {
            playAnimation("DieFront");
        }
    }
    public void move2() {
        print("calling move2");
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * speed;
        //rigBod.MovePosition(rigBod.position + moveAmount * Time.fixedDeltaTime);
    }

    public void playAnimation(string thisAnimation) {
        charAnimation = gameObject.GetComponent<Animator>();
        if (Time.time >= timeBetweenAnimation) {
            

            if(thisAnimation == "jab") {
                Character.Animator.SetTrigger(Time.frameCount % 2 == 0 ? "Slash" : "Jab");
            }else if(myStack.Count > 0) {
                charAnimation.SetBool(myStack.Peek().ToString(),false);
                myStack.Pop();
                myStack.Push(thisAnimation);
                charAnimation.SetBool(myStack.Peek().ToString(),true);
            }else {
                myStack.Push(thisAnimation);
                charAnimation.SetBool(myStack.Peek().ToString(),true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead) {
            rigBod.MovePosition(rigBod.position + moveAmount * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (defence < UnityEngine.Random.Range(0,75+defence)) {

            health -= damageAmount;
            UpdateHealthUI(health);
            takeDamageAnimation.playAnimation();
            if(health <= 0) {
                isDead = true;
                print("player killed");
                //Destroy(gameObject);
            }
        } else {
            playAnimation("jab");
            Debug.Log("Blocked damage");
        }
    }

    public void Dash() {
        
                Debug.Log("pressed space");
                if (Time.time >= lastDash) {
                    lastDash = Time.time + timeBetweenDash;
                    StartCoroutine(doDash());
                    }
            }
        

    IEnumerator doDash() {
        speed += 20;
        yield return new WaitForSeconds(.1f);
        speed -= 20;
    }



    private void UpdateHealthUI(int currentHealth) {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentHealth) {
                hearts[i].sprite = fullHeart;
            }else {
                hearts[i].sprite = blackHeart;
            }
        }
    }
}
