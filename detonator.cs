using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detonator : MonoBehaviour
{
    private shakeCam shake;
	public GameObject enemy;
	public Transform location;
	public bool isPlayerInRange = false;
	public bool startDetonation;
    [HideInInspector]public bool destroyed = false;
	public GameObject explotion;
	public float timeBeforeExplotion = 1f;
	public float damage = 75f;
	[HideInInspector]
	public MainPlayer player = new MainPlayer();
    private bool canDestroy = false;

    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("CamShaker").GetComponent<shakeCam>();
    }

    void FixedUpdate()
    {	
        if(startDetonation && Time.time >= timeBeforeExplotion)
        {
            destroyed = true;
        	
        	canDestroy = true;
        }
    }
    void Update()
    {
        if(canDestroy)
        {destroy();}
    }
    public void destroy()
    {
        
        AudioManager.instance.play("Explosion");
    	if(isPlayerInRange)
    	{
    		player.takeDamage(damage);
    	}
        shake.explosionShake();
        Instantiate(explotion,location.position,Quaternion.identity);
    	Destroy(enemy);

    }

    public void setTimer()
    {
    	timeBeforeExplotion += Time.time;
    }
}
