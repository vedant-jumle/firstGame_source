using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
	public Transform t;
    public MainPlayer player;
    private float startHealth;
    private float currentHealth;
    private float ratio;
    private Vector3 fill;
    void Start()
    {
    	fill = new Vector3(1f,1f,1f);

    	startHealth = player.health;
    	currentHealth = startHealth;
    }

    void Update()
    {

    	currentHealth = player.health;
    	ratio = currentHealth/startHealth;
    	if(ratio < 0){ratio = 0f;}
    	fill.x = ratio;
    	t.localScale = fill;
    }
}
