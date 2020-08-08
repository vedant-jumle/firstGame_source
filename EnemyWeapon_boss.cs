using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon_boss : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioManager audio;
    public Transform[] firepoint;
    public GameObject bulletPrefab;
    public float firerate = 0.5f;
    private float nextTimeToFire = 1f;
    public bool disableFire = false;
    public float weaponDamage = 25f;
    private GameObject[] temp;
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        nextTimeToFire = Time.time + nextTimeToFire;
    }

    // Update is called once per frame
    void Update()
    {   
        temp = GameObject.FindGameObjectsWithTag("player");
        if(temp[0] == null){disableFire = true;}
        if(Time.time >= nextTimeToFire && !disableFire)
        {
        	for(int i = 0;i < firepoint.Length;i++)
        	{
        		shoot(firepoint[i]);
        		nextTimeToFire = Time.time + 1f/firerate;
        	}
        }
    }

    void shoot(Transform fp)
    {
        bulletPrefab.GetComponent<bulletScript_enemy>().damage = weaponDamage;
        Instantiate(bulletPrefab, fp.position, fp.rotation);
    }
}
