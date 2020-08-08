using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDetails : MonoBehaviour
{
	public Transform[] firePoints;
	public SpriteRenderer sprite;
	public float weaponDamage = 15f;
	public float fireRate;
	void start()
	{
		sprite.enabled = false;
	}
	void update()
	{
		sprite.enabled = false;
	}
	public void display()
	{
		Debug.Log("fuck this");
	}
	public float getFireRate(){return fireRate;}
	public float getWeaponDamage(){return weaponDamage;}
	public Transform[] getFirePoints(){return firePoints;}
}
