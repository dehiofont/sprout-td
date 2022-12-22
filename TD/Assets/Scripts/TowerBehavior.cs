using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower
{
    public Tower(string typeName, string colorName, float dmgAmount, float shotDelayAmount, 
        float slowPercentAmount, float slowDurationAmount, int bouncesAmount, 
        float dotDurationAmount, float aoeRadius, float range, float gasDurationAmount, 
        float vulnerableAmount)
    {
        type = typeName;
        color = colorName;
        dmg = dmgAmount;
        shotDelay = shotDelayAmount;
        slowPercent = slowPercentAmount;
        slowDuration = slowDurationAmount;
        bounces = bouncesAmount;
        dotDuration = dotDurationAmount;
        radius = aoeRadius;
        towerRange = range;
        gasDuration = gasDurationAmount;
        vulnerability = vulnerableAmount;
    }

    public string type = "";
    public string color = "";
    public float dmg = 0;
    public float shotDelay = 0;
    public float slowPercent = 0;
    public float slowDuration = 0;
    public int bounces = 0;
    public float dotDuration = 0;
    public float radius = 0;
    public float towerRange = 0;
    public float gasDuration = 0;
    public float vulnerability = 0;
}

public class TowerBehavior : MonoBehaviour
{
    // TOWER OBJECTS
    public Tower blueberry = new Tower("blueberry", "blue", 1, 1, .85f, 1, 0, 0, 0, 2.5f, 0, 0); // SLOW
    public Tower blueberry2 = new Tower("blueberry", "blue", 1, 1.5f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // SLOW
    public Tower blueberry3 = new Tower("blueberry", "blue", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // SLOW
    public Tower blueberry4 = new Tower("blueberry", "blue", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // SLOW

    public Tower habanero = new Tower("habanero", "red", .5f, 1.5f, 0, 0, 0, 2.2f, 0, 2.5f, 0, 0); // DOT
    public Tower habanero2 = new Tower("habanero", "red", 1, 1.5f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // DOT
    public Tower habanero3 = new Tower("habanero", "red", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // DOT
    public Tower habanero4 = new Tower("habanero", "red", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // DOT

    public Tower banana = new Tower("banana", "yellow", .5f, 2.5f, 0, 0, 3, 0, 0, 2.5f, 0, 0); // BOUNCING BULLET
    public Tower banana2 = new Tower("banana", "yellow", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // BOUNCING BULLET
    public Tower banana3 = new Tower("banana", "yellow", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // BOUNCING BULLET
    public Tower banana4 = new Tower("banana", "yellow", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // BOUNCING BULLET

    public Tower artichoke = new Tower("artichoke", "green", .5f, 3, 0, 0, 0, 0, 2.75f, 2.5f, 0, 0); // AOE 360 SPREAD SHOT
    public Tower artichoke2 = new Tower("artichoke", "green", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // AOE 360 SPREAD SHOT
    public Tower artichoke3 = new Tower("artichoke", "green", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // AOE 360 SPREAD SHOT
    public Tower artichoke4 = new Tower("artichoke", "green", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // AOE 360 SPREAD SHOT

    public Tower corn = new Tower("corn", "purple", .3f, .25f, 0, 0, 0, 0, 0, 2.5f, 0, 0); // MACHINE GUN - FAST/LOW DMG
    public Tower corn2 = new Tower("corn", "purple", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // MACHINE GUN - FAST/LOW DMG
    public Tower corn3 = new Tower("corn", "purple", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // MACHINE GUN - FAST/LOW DMG
    public Tower corn4 = new Tower("corn", "purple", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // MACHINE GUN - FAST/LOW DMG

    public Tower carrot = new Tower("carrot", "orange", 2, 3, 0, 0, 0, 0, 0, 3.5f, 0, 0); // SNIPER = SLOW/HIGH
    public Tower carrot2 = new Tower("carrot", "orange", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // SNIPER = SLOW/HIGH
    public Tower carrot3 = new Tower("carrot", "orange", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // SNIPER = SLOW/HIGH
    public Tower carrot4 = new Tower("carrot", "orange", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // SNIPER = SLOW/HIGH

    public Tower garlic = new Tower("garlic", "white", .2f, 5, 0, 0, 0, 3, 0, 2.5f, 5, 0.2f); // MAKE VULNERABLE
    public Tower garlic2 = new Tower("garlic", "white", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // MAKE VULNERABLE
    public Tower garlic3 = new Tower("garlic", "white", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // MAKE VULNERABLE
    public Tower garlic4 = new Tower("garlic", "white", 1, .75f, .95f, .25f, 0, 0, 0, 2.5f, 0, 0); // MAKE VULNERABLE

    public Tower towerType;
    public int randomTower;

    public GameObject towerProjectile;
    public GameObject spawnPoint;

    public GameObject model;

    public float spawnTimer;
    public float spawnDelay;
    public List<GameObject> enemiesInRange = new List<GameObject>();

    public SphereCollider towerRange;

    public string typeOfTower;
    public float towerDmg;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnDelay;
        //Instantiate(towerProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
        //Debug.Log(blueberry.type + " " + blueberry.color);
        randomTower = Random.Range(0, 7);        

        switch (randomTower)
        {
            case 0: 
                towerType = blueberry;
                model.GetComponent<Renderer>().material = Resources.Load("blue") as Material;
                break;
            case 1:
                towerType = habanero;
                model.GetComponent<Renderer>().material = Resources.Load("red") as Material;
                break;
            case 2:
                towerType = banana;
                model.GetComponent<Renderer>().material = Resources.Load("yellow") as Material;
                break;
            case 3:
                towerType = artichoke;
                model.GetComponent<Renderer>().material = Resources.Load("green") as Material;
                break;
            case 4:
                towerType = corn;
                model.GetComponent<Renderer>().material = Resources.Load("purple") as Material;
                break;
            case 5:
                towerType = carrot;
                model.GetComponent<Renderer>().material = Resources.Load("orange") as Material;
                break;
            case 6:
                towerType = garlic;
                model.GetComponent<Renderer>().material = Resources.Load("beige") as Material;
                break;
        }

        // SETTING TOWER STATS
        spawnDelay = towerType.shotDelay;

        gameObject.GetComponent<SphereCollider>().radius = towerType.towerRange;
        typeOfTower = towerType.type;
        towerDmg = towerType.dmg;

        spawnTimer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(enemiesInRange.Count > 0)
        {
            cleanUpEnemyList();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (spawnTimer > spawnDelay)
        {
            //Debug.Log("Enemy Entered");
            var newProjectile = Instantiate(towerProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
            newProjectile.GetComponent<projectile>().towerStats = towerType;
            newProjectile.GetComponent<projectile>().target = enemiesInRange[0];
            spawnTimer = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemiesInRange.Remove(other.gameObject);
        //Debug.Log(other.gameObject.name);
    }

    private void cleanUpEnemyList()
    {
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
            }
        }
    }
}
