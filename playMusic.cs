using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusic : MonoBehaviour
{
    public AudioManager audio;

    void start()
    {
    	audio.play("music");
    }
}
