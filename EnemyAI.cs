using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string type;
	public Transform selfRef;
	private GameObject[] temp;
	private GameObject player;
	private Transform playerPos;

	public float runSpeed = 10f;

	public CharacterController2D controller;


    // Start is called before the first frame update
    void Start()
    {
        getPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.GetComponent<Transform>();
        moveTo(findDirection());
    }

    int findDirection()
    {
    	if(playerPos.localPosition.x - selfRef.localPosition.x < 0){return -1;}
    	else {return 1;}
    }

    void moveTo(int direction)
    {
    	controller.Move(direction*runSpeed*Time.fixedDeltaTime,false,false);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    	if(hitInfo.CompareTag("Surface"))
    	{
    		Destroy(selfRef);
    	}
    
    }

    void getPlayer()
    {
    	temp = GameObject.FindGameObjectsWithTag("player");
        player = temp[0];
    }
}
