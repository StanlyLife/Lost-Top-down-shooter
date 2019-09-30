using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public GameObject neverdie;
    public onLoadPlayer playerOnLoad;
    public Inventory neverdieInventory;
    private Animator transition;

    void Start()
    {
        transition = GetComponent<Animator>();
    }

    public void Update() {

            try {
                neverdieInventory = GameObject.FindGameObjectWithTag("NEVERDIE").GetComponent<Inventory>();
                playerOnLoad = GameObject.FindGameObjectWithTag("Player").GetComponent<onLoadPlayer>();
            }
            catch {
                Debug.Log("trying to initialise neverdie inventory, FAILED");
            }

    }
    public void MainMenuLoadMain() {
        StartCoroutine(Transition("Main"));
    }

    public void LoadScene(string sceneName) {
        //neverdieScript.Start();
        neverdieInventory.fwt();
            if (sceneName.ToLower() == "main") {
                Debug.Log("Transitioning to scene: " + sceneName + " LOADING VALUES: INVENTORY -> PLAYER");
                playerOnLoad.PlayerGetValuesFromInventory();
            } else if(sceneName.ToLower() == "store") {
                Debug.Log("Transitioning to scene: " + sceneName + " LOADING VALUES: PLAYER -> INVENTORY");
                neverdieInventory.InventoryGetPlayerValues();
                neverdieInventory.Equip();
            }
            StartCoroutine(Transition(sceneName));
            
    }

    
    IEnumerator Transition(string sceneName) {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1);
        if (sceneName.ToLower() == "store") {
            neverdieInventory.waveAttribute++;
        }
        SceneManager.LoadScene(sceneName);
    }

}

