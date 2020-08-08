using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWatch : MonoBehaviour
{
    private bool rectractCam = false;
    private float originalCamSize;
    [SerializeField] private Transform healthBar;
    [SerializeField] private Camera camera;
    [SerializeField] private float maxCamSize;
    [HideInInspector] public bool stopSpawning = false;
    [SerializeField] private bool enemySpawner = true;
    [SerializeField] private bool healthSpawner = true;
    [SerializeField] private bool godSpawner = true;
    [SerializeField] private bool barrelSpawner = true;

    public GameObject boss;
    public float bossRate;
    public float nextBoss;

    public GameObject godMode;
    public float godModeRate = 0.05f;
    public float nextGodMode = 0f;

    public GameObject Barrel;
    public float barrelRate = 0.1f;
    private float nextBarrel = 10f;

    private Vector2 unit = new Vector2(0f,0f);
    private int enemyCount;

    public GameObject[] enemyList;
    public GameObject healthPack;
	public GameObject mainPlayer;
    private GameObject[] enemyArr;

    private static Random getrandom = new Random();
    private Vector2 enemyPos = new Vector2(0f,0f);
	private Quaternion baseRot = new Quaternion(0f,0f,0f,0f);

    public float healthPackRate = 0.1f;
    private float nextTimeToHealth = 20f;
    public int maxEnemyCount;
    public float nextAddEnemy = 5f;
    private float enemy_add = 0;
    private float enemy_addRate;
    public  float nextAddEnemyRate = 0.2f;
    private int x;
    private bool flag = false;

    private bool inBossMode = false;
    private bool stopBoss = false;

    void Start()
    {
        stopBoss = false;
        inBossMode = false;
        nextBoss += Time.time;
        nextAddEnemy += Time.time;
        nextBarrel += Time.time;
        enemy_add += Time.time;
        nextGodMode += Time.time;
        nextTimeToHealth += Time.time;
        originalCamSize = camera.orthographicSize;
        enemy_addRate = 1f/0.5f;
        mainPlayer.GetComponent<weapon>().setWeapon(0);
        for(int i = 1;i <= maxEnemyCount;i++)
        {spawnEnemy(generateRandomPoint());}
    }

    void Update()
    {
        if(Time.time >= 90){flag = true;}
        if(Time.time >= 90 && flag)
        {
            flag = false;
            enemy_addRate = 1f/0.25f;
        }
        getEnemy();
        if(Time.time >= nextAddEnemy)
        {
            maxEnemyCount++;
            nextAddEnemy = Time.time + 1f/nextAddEnemyRate;
        }
        if(enemyCount < maxEnemyCount && !stopSpawning)
        {
            if(Time.time >= enemy_add)
            {
                x = generateRandomInt(0f,100f);
                spawnEnemy(generateRandomPoint());
                enemy_add = Time.time + 1f/enemy_addRate;
            }
        }

        if(Time.time >= nextTimeToHealth && !stopSpawning)
        {
            spawnHealth(generateRandomPointForHealth());
            nextTimeToHealth = Time.time + 1f/healthPackRate;
        }
        if(Time.time >= nextBarrel && !stopSpawning)
        {
            spawnBarrel(generateRandomPointForBarrel());
            nextBarrel = Time.time + 1f/barrelRate;
        }
        if(Time.time >= nextGodMode && !stopSpawning)
        {
            spawnGodMode(generateRandomPointForHealth());
            nextGodMode = Time.time + 1f/godModeRate;
        }

        if(mainPlayer.GetComponent<Transform>().position.y >= 10)
        {
            rectractCam = true;
        }
        if(mainPlayer.GetComponent<Transform>().position.y < 10)
        {   
            rectractCam = false;
        }
        if(Time.time >= nextBoss && !stopBoss)
        {
            inBossMode = true;
            spawnBoss(generateRandomPoint());
            for(int i = 0; i < enemyList.Length;i++)
            {
                Destroy(enemyList[i]);
            }
        }
        inBossMode = stopSpawning;
        Debug.Log(rectractCam);
        UpdateCam();
        
    }
    void spawnGodMode(Vector2 pos)
    {
        if(godSpawner)
        {Instantiate(godMode,pos,baseRot);}
    }

    void spawnBarrel(Vector2 pos)
    {
        if(barrelSpawner)
        {Instantiate(Barrel,pos,baseRot);}
    }

    void spawnHealth(Vector2 pos)
    {
        if(healthSpawner)
        {Instantiate(healthPack,pos,baseRot);}

    }

    int generateRandomInt(float a, float b)
    {
        float y = Random.Range(a,b);
        if(y <= (a+b)/3){return 0;}
        else if(y > (a+b)/3 && y <= 5*(a+b)/6){return 1;}
        else{return 2;}
    }

    void getEnemy()
    {
        enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemyArr.Length;
    }

    void spawnEnemy(Vector2 pos)
    {
        if(enemySpawner)
	    {Instantiate(enemyList[x],pos,baseRot);}
    }
        
    public Vector2 generateRandomPointForHealth()
    {   
        Vector2 value;
        int side = generateRandomIntForHealth(100f,-100f);
        if(side == 1)
        {
            value = new Vector2(Random.Range(2.8f,16f),Random.Range(-5.2f,-1.8f));
        }
        else
        {
            value = new Vector2(Random.Range(-2.8f,-16f),Random.Range(-5.2f,-1.8f));
        }
        return value;
    }

    public Vector2 generateRandomPoint()
    {
        Vector2 value = new Vector2(Random.Range(-16f,16f),9f);
        return value;
    }
    public Vector2 generateRandomPointForBarrel()
    {
        Vector2 value = new Vector2(Random.Range(-10f,10f),9f);
        return value;
    }

    public int generateRandomIntForHealth(float a , float b)
    {
        float temp = Random.Range(a,b);
        if(temp <= (a+b)/2){return 0;}
        else{return 1;}
    }

    void UpdateCam()
    {
        Vector2 newPos = Vector2.zero;
        if(rectractCam && camera.orthographicSize <= maxCamSize)
        {
            camera.orthographicSize += 0.1f;
            newPos = healthBar.position;
            newPos.y += 0.1f;
            healthBar.position = newPos;
        }
        if(!rectractCam && camera.orthographicSize >= originalCamSize)
        {
            camera.orthographicSize -= 0.1f;
            newPos = healthBar.position;
            newPos.y -= 0.1f;
            healthBar.position = newPos;
        }
    }

    void spawnBoss(Vector2 pos)
    {   
        Instantiate(boss,pos,baseRot);
    }
}
