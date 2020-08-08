using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
	public Barrel Barrel;
	void OnTriggerStay2D(Collider2D info)
	{
		if(info.CompareTag("player"))
		{
			Barrel.canDamagePlayer = true;
		}
	}
	void OnTriggerExit2D(Collider2D info)
	{	
		if(info.CompareTag("player")){Barrel.canDamagePlayer = false;}
	}
}
