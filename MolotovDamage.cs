using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovDamage : MonoBehaviour
{
	public Transform parent;
	public MolotovScript molotov;
	private Quaternion parentRot;
	private Quaternion rotateTo;
	void Awake()
	{
		parentRot = parent.rotation;
	}
	void FixedUpdate()
	{
		rotateTo.z = -1f * parentRot.z;
		transform.rotation = rotateTo;
	}
}
