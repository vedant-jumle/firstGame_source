using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public void load()
    {
    	Loader.Load(Loader.Scenes.Scene1);
    }
}
