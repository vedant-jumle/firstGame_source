using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
	public enum Scenes
	{
		Scene1,
		Loading,
	}

	private static Action onLoaderCallBack;

	public static void Load(Scenes scene)
	{
		onLoaderCallBack = () => {
		
			SceneManager.LoadScene(scene.ToString());
			
		};
		SceneManager.LoadScene(Scenes.Loading.ToString());
	}
	public static void LoaderCallBack()
	{
		if(onLoaderCallBack != null)
		{
			onLoaderCallBack();
			onLoaderCallBack = null;
		}
	}
}
