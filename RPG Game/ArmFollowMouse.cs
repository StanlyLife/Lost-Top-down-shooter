using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFollowMouse : MonoBehaviour
{
    private CharacterScript player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }
    void Update()
    {
        if (!player.isDead) {

        //move item to follow mouse
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 250, Vector3.forward);
        gameObject.transform.rotation = rotation;
        }
    }
}
