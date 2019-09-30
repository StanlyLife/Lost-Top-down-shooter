using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{



    public Transform playerTransform;
    public float speed;

    //restrainers on camera
    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

     private void Start()
    {
        transform.position = playerTransform.position;
    }

    private void Update()
    {
        float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);
        if (playerTransform != null) {
            transform.position = Vector2.Lerp(transform.position, playerTransform.position, speed);
        } else {
            print("player is dead");
        }


    }
}
