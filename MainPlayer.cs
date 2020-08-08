using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    MolotovDamage tempMoli;
    private bool isMoliOverlap = false;
    private bool isTakingMolotovDamage = false;
    private float moliDamage = 0f;
    private  float moliDamageRate = 0.5f;
    private  float nextMoliDamage = 0f;

    public weapon weapon;
	private AudioManager audio;
    public OverWatch overwatch;
	public SpriteRenderer renderer;
	private Color hitColor = new Color(255,0,0);
	private Color normalColor = new Color(10,255,255);
	public float colorChangeTime = 1f;

    [HideInInspector]public shakeCam shake;

    public Transform playerTransform;

    public float startHealth = 100f;
	[HideInInspector]public float health = 100f;
    private int Score = 0;

    public GameObject selfRef;
    public GameObject deathEffect;

    public Text moliCounter;
    public Text scoreBoard;
    public Text EndText;
    public Text Exit;
    public Text restart;

    public Button restartButton;
    public Button exitButton;
    public Image restartButtonImage;
    public Image exitButtonImage;
    void Start()
    {
        health = startHealth;
    	audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    	renderer.color = Color.cyan;
        shake = GameObject.FindGameObjectWithTag("CamShaker").GetComponent<shakeCam>();
        EndText.enabled = false;
        Exit.enabled = false;
        restart.enabled = false;
        restartButtonImage.enabled = false;
        exitButtonImage.enabled = false;
        restartButton.enabled = false;
        exitButton.enabled = false;
    }   
    void Update()
    {
        if(isMoliOverlap && Time.time >= nextMoliDamage)
        {
            takeDamage(moliDamage);
            nextMoliDamage = Time.time + 1f/moliDamageRate;
        }
		if(health <= 0)
		{
            AudioManager.instance.play("Explosion");
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            EndText.enabled = true;
            Exit.enabled = true;
            restart.enabled = true;
            restartButtonImage.enabled = true;
            exitButtonImage.enabled = true;
            restartButton.enabled = true;
            exitButton.enabled = true;
            overwatch.stopSpawning = true;
			Destroy(selfRef);
		}     
        displayMoliCount();   
    }

    public void takeDamage(float value)
    {
        if(!weapon.godMode)
        {
            health = health - value;
            audio.play("Hurt");
        }
    }

    public void displayMoliCount()
    {
        moliCounter.text = "Molotov: " + weapon.molotovCount;
    }

    public void addScore(int value)
    {
        Score = Score + value;
        displayScore();
    }

    public void displayScore()
    {
        scoreBoard.text = "Score: "+ Score;
    }

    public void addHealth(float value)
    {
        
        health += value;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Lava"){health = 0f;}
        else{}
    }

    void wait(float value)
    {
    	for(int i = 0;i < 1000000;i++);
    }

    void OnTriggerStay2D(Collider2D info)
    {
        if(info.CompareTag("MolotovDamage"))
        {
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
