using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainGame : MonoBehaviour
{
    public void StartGame()
    {
    	Loader.Load(Loader.Scenes.Scene1);
    }
}
