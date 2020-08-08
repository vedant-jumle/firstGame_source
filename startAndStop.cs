using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class startAndStop : MonoBehaviour
{
    public MainPlayer player;
    public AudioSource music;

    void Update()
    {
    	if(player.health <= 0)
    	{
    		music.Stop();
    	}
    }
}
