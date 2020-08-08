using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOverlap : MonoBehaviour
{
    
	public Enemy enemy;
	public GameObject particle_green;
	public GameObject particle_red;
	public SpriteRenderer rend;
	public detonator detonator;
    private int i = 0;
    void OnTriggerStay2D(Collider2D hitInfo)
    {
    	if(hitInfo.tag == "player")
    	{
            detonator.isPlayerInRange = true;
    		enemy.deathParticles = particle_red;
    		rend.color = Color.red;
    	}
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        
        if(hitInfo.tag == "player")
        {
            i++;
            if(i <= 1)
            {
                detonator.player = hitInfo.GetComponent<MainPlayer>();
                detonator.setTimer();
                detonator.startDetonation = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D hitInfo)
    {
    	if(hitInfo.tag == "player")
    	{
            detonator.isPlayerInRange = false;
    		enemy.deathParticles = particle_green;
    		rend.color = Color.green;
    	}
    }
}
