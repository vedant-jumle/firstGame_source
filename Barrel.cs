using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	private GameObject[] temp;
	private shakeCam shake;
	public Enemy[] enemiesInRange;
	public GameObject explosion;
	private MainPlayer player;
	public float damage = 100f;
	[HideInInspector]public bool canDamagePlayer = false;
	private bool canExplode = false;
	[HideInInspector]public bool destroyed = false;
	void Start()
	{
		temp = GameObject.FindGameObjectsWithTag("player");
        player = temp[0].GetComponent<MainPlayer>();;
		shake = GameObject.FindGameObjectWithTag("CamShaker").GetComponent<shakeCam>();
	}
	void Update()
	{
		if(canExplode && destroyed)
		{explode();}
	}
	void FixedUpdate()
	{
		if(canExplode){destroyed = true;}
	}
    public void explode()
    {
    	
		AudioManager.instance.play("Explosion");
		if(canDamagePlayer){player.takeDamage(damage);}
		Instantiate(explosion,transform.position,transform.rotation);
		shake.explosionShake();
		Destroy(gameObject);
		return;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    	if(hitInfo.CompareTag("Lava")){canExplode = true;}

    	if(hitInfo.CompareTag("Projectile"))
    	{

    		canExplode = true;
    		Destroy(hitInfo.GetComponent<bulletScript>().selfRef);

    	}
    	else if(hitInfo.CompareTag("EnemyProjectile"))
    	{
    		canExplode = true;
    		Destroy(hitInfo.GetComponent<bulletScript_enemy>().selfRef);
    	}
    }
}
