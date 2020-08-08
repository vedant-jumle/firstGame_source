using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Post;
public class weapon : MonoBehaviour
{
	public GameObject Molotov;
	public int molotovCount = 1;
	public Transform throwPoint;

    public PostProcessingBehaviour ppBehaviour;
    [HideInInspector]public bool godMode = false;
    public float godModeTime = 5f;
    public float godModeRate = 0.2f;

    public PostProcessingProfile ppProfile;
    private ChromaticAberrationModel CAberration; 
    public float CA_intensity = 0.7f;
    public float CA_base_intensity = 0.15f;

	private AudioManager audio;
    public GameObject SG;
    public GameObject AK;
    private GameObject activeWeapon;
    public GameObject bulletPrefab;

    public float recoilPerBullet = 400f;
    public Transform[] SGfirepoint = new Transform[11];
    public Transform AKfirepoint;
    
    private int weaponNo = 1;

    public float firerate = 3f;
    private float nextTimeToFire = 0f;
    private float weaponDamage;

    private Rigidbody2D rb;
    private Vector2 recoilDirection;

    private bool startFlag = true;

    [SerializeField] private MainPlayer player;
    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    	audio = GetComponent<AudioManager>();
        CAberration = ppProfile.chromaticAberration;
        ppBehaviour.profile = ppProfile;
        var ca2 = ppProfile.chromaticAberration.settings;
        ca2.intensity = CA_base_intensity;
        ppProfile.chromaticAberration.settings = ca2;
    }

    void Update()
    {
        if(startFlag)
        {
            weaponNo = 0;
            setWeapon(weaponNo);
            startFlag = false;
        }
        if(molotovCount > 0 && Input.GetButtonDown("Throw") && molotovCount > 0)
        {
        	Instantiate(Molotov,throwPoint.position,throwPoint.rotation);
        	molotovCount--;
        }
        if(godMode)
        {
            setWeapon(0);
            firerate = 25f;

            if(Time.time <= godModeTime)
            {
                var ca = ppProfile.chromaticAberration.settings;
                ca.intensity = CA_intensity;
                ppProfile.chromaticAberration.settings = ca;
                if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f/firerate;
                    for(int i = 0;i < 5;i++)
                    {
                        Shoot(SGfirepoint[i]);
                    }
                    player.shake.camShake();
                    recoilDirection = SGfirepoint[2].right;
                    addRecoil(50f);
                }
            }
            else
            {
                bulletPrefab.GetComponent<bulletScript>().ResetBulletRecoil();
                godMode = false;
                setWeapon(weaponNo);
                var ca2 = ppProfile.chromaticAberration.settings;
                ca2.intensity = CA_base_intensity;
                ppProfile.chromaticAberration.settings = ca2;
            }
        }
        else
        {
            if(Input.GetButtonDown("Fire2"))
            {
               if(weaponNo == 1){weaponNo = 0;}
                else{weaponNo = 1;}
                setWeapon(weaponNo);
            }
            else if(weaponNo == 0)
            {
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {

               	    nextTimeToFire = Time.time + 1f/firerate;
               	    Shoot(AKfirepoint);
                    recoilDirection = AKfirepoint.right;
                    addRecoil(150f);
                }   
            }
            else if(weaponNo == 1)
            {
                if(Input.GetButton("Fire1")&&Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f/firerate;
                    for(int i = 0;i < 5;i++)
                    {
                        Shoot(SGfirepoint[i]);

                    }
                    recoilDirection = SGfirepoint[2].right;
                    addRecoil(300f);
                }
            }
        }
    }
    void Shoot(Transform firePos)
    {
        //shooting 
        AudioManager.instance.play("Shoot");
        bulletPrefab.GetComponent<bulletScript>().damage = weaponDamage;
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);

    }
    public void setWeapon(int i)
    {
        if(i == 0)
        {
            activeWeapon = AK;
            AK.GetComponent<Renderer>().enabled = true;
            SG.GetComponent<Renderer>().enabled = false;
        }
        else if(i == 1)
        {
            activeWeapon = SG;
            AK.GetComponent<Renderer>().enabled = false;
            SG.GetComponent<Renderer>().enabled = true;
        }
        firerate = activeWeapon.GetComponent<weaponDetails>().getFireRate();
        weaponDamage = activeWeapon.GetComponent<weaponDetails>().getWeaponDamage();
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        if(info.CompareTag("GodMode"))
        {
            godMode = true;
            godModeTime = Time.time + 1f/godModeRate;
            Destroy(info.gameObject);
            AudioManager.instance.play("Powerup");
        }
    }

    void addRecoil(float amount)
    {
        recoilDirection *= -1f;
        rb.AddForce(recoilDirection * amount);
    }
}
