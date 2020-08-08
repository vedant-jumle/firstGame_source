using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoil : MonoBehaviour
{
	public RotateGun weapon;
	public float maxOffset;
	public float acc;
	public float amount;

	private bool recoilInEffect = false;
	private bool weaponHeadedBack = false;

	private Vector3 offsetPos = new Vector3(0f,0f);
	private Vector3 velocity = new Vector3(0f,0f);
    // Start is called before the first frame update

    public void AddRecoil()
    {
    	recoilInEffect = true;
    	weaponHeadedBack = false;
    	velocity = transform.right * amount;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateRecoil();
    }

    void updateRecoil()
    {
    	if(!recoilInEffect){return;}
    	velocity += (-offsetPos.normalized) * acc * Time.deltaTime;
    	Vector3 newOffsetPos = offsetPos + velocity * Time.deltaTime;
    	Vector3 newPos = transform.position - offsetPos;
    	if(newOffsetPos.magnitude >= maxOffset)
    	{
    		velocity = Vector3.zero;
    		weaponHeadedBack = true;
    	}

    	transform.position = newPos + newOffsetPos;
    	offsetPos = newOffsetPos;
    }
}
