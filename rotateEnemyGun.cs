using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateEnemyGun : MonoBehaviour
{
    public Transform weapon;
    private GameObject[] temp = new GameObject[1];
    private Transform player;
    private Vector3 playerPos = new Vector3(0f,0f,0f);
    void start()   
    {}
    void Update()
    {
        updateObject();
    	rotateGun();
    }
    void updateObject()
    {
        temp = GameObject.FindGameObjectsWithTag("player");
        player = temp[0].transform;
        playerPos = player.localPosition;
    }
    void rotateGun()
    {
    	Vector2 lookAt = new Vector2(playerPos.x - transform.position.x , playerPos.y - transform.position.y);
    	weapon.right = lookAt;
    }
}