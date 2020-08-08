using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript_enemy : MonoBehaviour
{
    private Vector2 impactPoint = new Vector2(0f,0f);
    private float bulletEnergy = 300f, bulletForce = 300f;
    private AudioManager audio;
	public GameObject hitEffect;
    public float damage = 50f;
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject selfRef;
    public bool disableParticles = false;
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        rb.velocity = transform.right * speed;
    }
    
    void update()
    {}


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    	
    	Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
        	enemy.takeDamage(damage);
    		destroy();
        }
        MainPlayer player = hitInfo.GetComponent<MainPlayer>();
        if(player != null)
        {
        	player.takeDamage(damage);
            addTorqueToBody(hitInfo.GetComponent<Transform>(),hitInfo.GetComponent<Rigidbody2D>());
            addForceToBody(hitInfo.GetComponent<Rigidbody2D>());
        	destroy();
        } 
        else if(hitInfo.CompareTag("HealthPack")){}
        else if(hitInfo.CompareTag("Projectile")){destroy();}
        else if(hitInfo.CompareTag("Exploder")){}
        else if(hitInfo.CompareTag("EnemyProjectile")){}
        else if(hitInfo.CompareTag("BarrelRadius")){}
        else if(hitInfo.CompareTag("GodMode")){}
        else if(hitInfo.CompareTag("FloatingSurface"))
        {
            addTorqueToBody(hitInfo.GetComponent<Transform>(), hitInfo.GetComponent<Rigidbody2D>());
            destroy();
            
        }
        else if(hitInfo.CompareTag("Barrel"))
        {
            Barrel barrel = hitInfo.GetComponent<Barrel>();
            barrel.explode();
            destroy();
        }
        else if(hitInfo.CompareTag("MolotovObject")){}
        else if(hitInfo.CompareTag("MolotovDamage")){}

        else
        {
        	
        	destroy();
        }
    }
    void destroy()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 180-rot.z;
        Quaternion final = Quaternion.Euler(rot);
        Instantiate(hitEffect,transform.position,final);
        Destroy(selfRef);
    }
    void addTorqueToBody(Transform objectTransform, Rigidbody2D hitInfo)
    {   

        Vector2 hitToCenter = transform.position - objectTransform.position;
        float sin,finalLength,torqueAmount;
        sin = Mathf.Sin(Mathf.Deg2Rad * Vector2.SignedAngle(hitToCenter, transform.right));
        finalLength = sin * hitToCenter.magnitude;
        torqueAmount = bulletForce * finalLength;
        hitInfo.AddTorque(torqueAmount);
    }
    void addForceToBody(Rigidbody2D hitInfo)
    {
        Vector2 force = bulletEnergy * transform.right;
        hitInfo.AddForce(force);
    }
}
