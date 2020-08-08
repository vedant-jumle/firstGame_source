using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameParticlesS : MonoBehaviour
{
	public float aliveTime = 5f;
    void Start()
    {
    	aliveTime += Time.time;
    }

    void Update()
    {
    	if(Time.time >= aliveTime){Destroy(gameObject);}
    	else{}
    }
}
