using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeCam : MonoBehaviour
{

	public Animator camAnim;

    public void camShake()
    {
    	camAnim.SetTrigger("shake");
    }
    public void explosionShake()
    {
    	camAnim.SetTrigger("explosion");
    }
}
