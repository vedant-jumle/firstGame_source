using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{

	private weapon weapon;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    	if(hitInfo.CompareTag("player"))
    	{
    		weapon = hitInfo.GetComponent<weapon>();
    		weapon.godMode = true;
    		weapon.godModeTime += Time.time;
    		Destroy(gameObject);
    	}
    }
}
