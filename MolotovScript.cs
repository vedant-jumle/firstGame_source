using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovScript : MonoBehaviour
{
	public GameObject initialParticles;
	public float startCoolDown = 0.5f;
	private bool destroy = false;
	public Collider2D myCollider;
	public Collider2D polyCollider;
	public GameObject particles;
	public Renderer renderer;
	public GameObject self;
	public Transform parentTransform;
	public Rigidbody2D rb;
	public float damage = 25f;
	public float damageRate = 2f;
	public float nextTimeToDamage = 0f;
	public float throwVelocity = 5f;
	public float burnOutTime = 10f;
	[HideInInspector]public bool burnOut = false;
	private int i = 1;
	void Start()
	{
		startCoolDown += Time.time;
		renderer.enabled = true;
		polyCollider.isTrigger = false;
		rb.velocity = parentTransform.right * throwVelocity;
		rb.AddTorque(25f);
	}
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    	if(hitInfo.CompareTag("Surface") || hitInfo.CompareTag("Enemy") && i == 1 && Time.time >= startCoolDown)
    	{
    		polyCollider.isTrigger = true;
    		burnOut = true;
    		renderer.enabled = false;
    		burnOutTime = burnOutTime + Time.time;
    		rb.velocity = Vector2.zero;
    		rb.angularVelocity = 0f;
    		rb.gravityScale = 0f;
    		i++;
    		Instantiate(initialParticles,parentTransform.position,Quaternion.identity);
    		Instantiate(particles,parentTransform.position,Quaternion.identity);
    	}
    }
    void FixedUpdate()
    {
    	if(burnOut && Time.time >= burnOutTime)
    	{
    		burnOut = false;
    		destroy = true;
    	}
    }
    void Update()
    {
    	if(destroy)
    	{
    		Destroy(self);
    	}
    	else{}
    }
}
