using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_boss : MonoBehaviour
{
    MolotovDamage tempMoli;
    private bool isMoliOverlap = false;
    private bool isTakingMolotovDamage = false;
    private float moliDamage = 0f;
    private float moliDamageRate = 0.5f;
    private float nextMoliDamage = 0f;
    private Barrel barrel;
    private detonator detonator;
    private AudioManager audio;
    private shakeCam shake;
    public GameObject[] temp;
    public GameObject deathParticles;
    public EnemyAI ai;
    private MainPlayer player;
    public float health = 100f;
    public int maxScore = 10;
    private float waitTime = 10f;

    [HideInInspector]public bool hasExploded = false;

    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        shake = GameObject.FindGameObjectWithTag("CamShaker").GetComponent<shakeCam>();
        temp = GameObject.FindGameObjectsWithTag("player");
        player = temp[0].GetComponent<MainPlayer>();
    }
    void Update()
    {
        if(isMoliOverlap && Time.time >= nextMoliDamage)
        {   

            takeDamage(moliDamage);
            nextMoliDamage = Time.time + 1f/moliDamageRate;
        }
        if(health <= 0f)
        {
            AudioManager.instance.play("Death");
            shake.camShake();
            Instantiate(deathParticles,transform.position,Quaternion.identity);
            
            Destroy(gameObject);

            player.addScore(maxScore);
        }
    }

    public int giveScore()
    {return maxScore;}
    public void takeDamage(float value)
    {
    	health = health - value;
    }
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Lava")
        {
            AudioManager.instance.play("Death");
            shake.camShake();
            Instantiate(deathParticles,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        else{}
        
    }
    void OnTriggerStay2D(Collider2D info)
    {
        if(info.CompareTag("Exploder"))
        {
            detonator = info.GetComponent<detonator>();
            if(detonator.destroyed)
            {
                AudioManager.instance.play("Death");
                shake.camShake();
                Instantiate(deathParticles,transform.position,Quaternion.identity);
                player.addScore(maxScore);
                Destroy(gameObject);
            }
            
        }
        if(info.CompareTag("BarrelRadius"))
        {
            DamageObject temp = info.GetComponent<DamageObject>();
            barrel = temp.Barrel;
            if(barrel.destroyed)
            {
            	takeDamage(200f);
            }
        }
        if(info.CompareTag("MolotovDamage"))
        {
            Debug.Log("true");
            tempMoli = info.GetComponent<MolotovDamage>();
            MolotovScript moli = tempMoli.molotov;
            if(moli.burnOut)
            {   
                isMoliOverlap = moli.burnOut;
                moliDamage = moli.damage;
                moliDamageRate = moli.damageRate;
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D info)
    {
        if(info.CompareTag("MolotovDamage"))
        {
            isMoliOverlap = false;
        }
    }
    
}
