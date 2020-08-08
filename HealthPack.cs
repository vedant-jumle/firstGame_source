using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
	public GameObject selfRef;

	public float value;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    	MainPlayer player = hitInfo.GetComponent<MainPlayer>();
    	if(player != null)
    	{
    		if(player.health < player.startHealth)
            {
                if(player.health < player.startHealth - value)
                {
                    player.health += value;
                }
                else
                {
                    player.health = player.startHealth;
                }
                destroy();
            }
    	}
    }

    void destroy()
    {
        AudioManager.instance.play("Collect");
    	Destroy(selfRef);
    }
}
