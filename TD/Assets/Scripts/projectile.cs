using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public LevelLogic mainGameLogic;

    public GameObject target;
    public GameObject towerOrigin;
    public enemyBehavior enemyScript;
    public float projectileSpeed = 1;
    public Tower towerStats;

    public GameObject closestEnemy;

    public int projectileBounces = 0;

    public List<GameObject> enemies = new List<GameObject>();

    // COLLIDERS
    public SphereCollider sCollider;
    public CapsuleCollider cCollider;
    public float defaultCColliderSize;

    // AOE VARIABLES
    public float aoeGrowTimer;
    public float aoeGrowthTime;

    // PROJECTILE VISUALS
    public GameObject aoeDisplay;
    public GameObject defaultProjectileDisplay;

    // VULNERABLE VARIABLES
    public float gasDuration;
    public float gasTimer;
    public bool startGas = false;
    public GameObject gasVisual;

    // Start is called before the first frame update
    void Start()
    {
        mainGameLogic = GameObject.Find("GameLogic").GetComponent<LevelLogic>();
        projectileBounces = towerStats.bounces;

        cCollider = gameObject.GetComponent<CapsuleCollider>();
        sCollider = gameObject.GetComponent<SphereCollider>();

        if (towerStats.type == "artichoke")
        {
            cCollider.enabled = true;
            sCollider.enabled = false;
            aoeDisplay.SetActive(true);
            defaultProjectileDisplay.SetActive(false);
        }
        defaultCColliderSize = cCollider.radius;
        aoeGrowthTime = .25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (towerStats.type == "banana")
        {
            if (projectileBounces <= 0 || !target)
            {
                Destroy(gameObject);
            }
        }

        if(towerStats.type != "artichoke" && !startGas)
        {
            if (target)
            {
                transform.LookAt(target.transform.position);
                transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
            }
            else if(towerStats.type != "banana" || towerStats.type != "garlic")
            {
                Destroy(gameObject);
            }
        }

        if(towerStats.type == "artichoke")
        {
            aoeGrowTimer += Time.deltaTime;
            cCollider.radius = Mathf.Lerp(defaultCColliderSize, towerStats.radius, aoeGrowTimer/aoeGrowthTime);
            if(aoeGrowTimer >= aoeGrowthTime)
            {
                Destroy(gameObject);
            }
        }

        // VULNERABLE LOGIC
        if (startGas)
        {
            gasTimer += Time.deltaTime;

            if (gasTimer > towerStats.gasDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if(towerStats.type == "artichoke" && other.tag == "Enemy")
        {
            enemyScript = other.GetComponent<enemyBehavior>();
            enemyScript.hitMods = towerStats;
            enemyScript.EnemyHit();
        }

        if (towerStats.type == "garlic" && other.tag == "Enemy")
        {
            startGas = true;
            enemyScript = other.GetComponent<enemyBehavior>();
            enemyScript.hitMods = towerStats;
            transform.eulerAngles = new Vector3(0, 0, 0);
            gasVisual.SetActive(true);
            cCollider.enabled = true;
            sCollider.enabled = false;
            enemyScript.EnemyHit();
        }

        if (other.gameObject == target && towerStats.type != "artichoke" && towerStats.type != "garlic")
        {
         
            enemyScript = other.GetComponent<enemyBehavior>();
            enemyScript.hitMods = towerStats;
            enemyScript.EnemyHit();

            if(towerStats.type != "banana")
            {
                Destroy(gameObject);
            }

            if(projectileBounces <= 0)
            {
                Destroy(gameObject);
            }


            if (towerStats.type == "banana" && projectileBounces > 0)
            {
                //Debug.Log("Banana Hit!");
                --projectileBounces;
                findNewTarget();
            }
        }
    }

    private void findNewTarget()
    {
        enemies = mainGameLogic.activeEnemies;

        float lastDistance = Vector3.Distance(gameObject.transform.position, mainGameLogic.activeEnemies[0].transform.position);
        foreach (GameObject enemy in mainGameLogic.activeEnemies)
        {
            //Debug.Log(enemy.name);
            float temp = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
            if ((Vector3.Distance(gameObject.transform.position, enemy.transform.position) < 3) && target != enemy)
            {
                target = enemy;
            }
        }
    }

    //public void GrabTarget(GameObject towerTarget)
    //{
    //    target = towerTarget;
    //}
}
