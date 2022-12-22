using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.UI;

public class enemyBehavior : MonoBehaviour
{
    public float enemySpeed = 1.5f;
    public float duration = 1;

    public PathCreator mainPath;
    public EndOfPathInstruction end;
    public float speed = 5;
    public float originalSpeed;
    float dstTravelled;

    public float enemyHealth = 1;

    public GameObject mainGameLogic;

    public int enemyGoldValue = 5;

    public Material originalMaterial;
    public GameObject model;

    // SPEED VARIABLES
    public bool startSlow = false;
    public float speedPercent;
    public float speedDuration;
    public float speedTimer;

    // DOT VARIABLES
    public float dotDuration;
    public bool startDot = false;
    public float dotTimer;
    public float dotDmgTimer;
    public float dotCadence = .5f;

    public Tower hitMods;
    public float flashTimer;

    public bool enemyDmgFlash = false;

    // VULNERABLE VARIABLES
    public bool vulnerable;
    public float vulnerableAdd;

    // HEALTHBAR VARS
    public Slider healthbar;
    public float enemyOriginalHealth;


    // Start is called before the first frame update
    void Start()
    {
        mainGameLogic = GameObject.Find("GameLogic");
        enemyOriginalHealth = enemyHealth;

        // SPEED SETUP
        originalSpeed = speed;

        mainPath = GameObject.Find("Path").GetComponent<PathCreator>();
        if (mainPath != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            //mainPath.pathUpdated += OnPathChanged;
        }
    }
    void Update()
    {
        dstTravelled += speed * Time.deltaTime;
        transform.position = mainPath.path.GetPointAtDistance(dstTravelled, end);
        transform.rotation = mainPath.path.GetRotationAtDistance(dstTravelled, end);

        // SLOWING LOGIC
        if (startSlow)
        {
            speedTimer += Time.deltaTime;
            if (speedTimer > speedDuration)
            {
                ResetMaterial();
                speedTimer = 0;
                speed = originalSpeed;
                startSlow = false;
            }
        }

        // DOT LOGIC
        if (startDot)
        {
            vulnerable = true;
            dotTimer += Time.deltaTime;
            if (dotTimer < dotDuration)
            {
                dotDmgTimer += Time.deltaTime;
                if (dotDmgTimer > dotCadence)
                {
                    enemyHealthAdjustment();
                    dotDmgTimer = 0;
                }
            }
            else
            {
                ResetMaterial();
                dotDmgTimer = 0;
                dotTimer = 0;
                vulnerable = false;
                startDot = false;
            }
        }

        if (enemyDmgFlash)
        {

            flashTimer += Time.deltaTime;
            if (flashTimer > .2f)
            {
                model.GetComponent<Renderer>().material = originalMaterial;
                enemyDmgFlash = false;
                flashTimer = 0;
            }
        }
    }

        void ResetMaterial()
        {
            model.GetComponent<Renderer>().material = originalMaterial;
        }
        void OnCollisionEnter(Collision collision)
        {
            //Check for a match with the specified name on any GameObject that collides with your GameObject
            if (collision.gameObject.name == "Redirect")
            {
                //If the GameObject's name matches the one you suggest, output this message in the console
                Debug.Log("Do something here");
            }

            //Check for a match with the specific tag on any GameObject that collides with your GameObject
            if (collision.gameObject.tag == "Redirect")
            {
                //If the GameObject has the same tag as specified, output this message in the console
                Debug.Log("Do something else here");
            }
        }

        void enemyHealthAdjustment()
        {
            if(vulnerable && hitMods.type != "garlic")
            {
                vulnerableAdd = hitMods.dmg * hitMods.vulnerability;
            }

            enemyHealth -= hitMods.dmg + vulnerableAdd;
            healthbar.value = enemyHealth / enemyOriginalHealth;
            vulnerableAdd = 0;


            if (enemyHealth <= 0)
            {
                mainGameLogic.GetComponent<LevelLogic>().cleanUpEnemyList(gameObject);
                mainGameLogic.GetComponent<LevelLogic>().modifyGold(enemyGoldValue);
                Destroy(gameObject);
            }
        }

        //public IEnumerator RotateEnemy(Quaternion direction, Vector3 position)
        //{
        //    Debug.Log("rotate!");

        //    Debug.Log(duration);
        //    float time = 0;
        //    Quaternion startValue = transform.rotation;
        //    Vector3 startPosition = transform.position;

        //    while (time < duration)
        //    {
        //        transform.rotation = Quaternion.Lerp(startValue, direction, time / duration);
        //        time += Time.deltaTime;
        //        yield return null;
        //    }
        //    transform.rotation = direction;
        //}

        //public void OnPathChanged()
        //{
        //    dstTravelled = mainPath.path.GetClosestDistanceAlongPath(transform.position);
        //}

        public void EnemyHit()
        {

            if (hitMods.dotDuration == 0)
            {
                model.GetComponent<Renderer>().material = Resources.Load("dmgflash") as Material;
                enemyDmgFlash = true;
                enemyHealthAdjustment();
            }

            // SET UP SLOW 
            if (hitMods.slowPercent > 0)
            {
                speedTimer = 0;
                model.GetComponent<Renderer>().material = Resources.Load("blue") as Material;
                speed = speed * hitMods.slowPercent;
                speedDuration = hitMods.slowDuration;
                startSlow = true;
            }

            if (hitMods.dotDuration > 0)
            {
                dotTimer = 0;

                if(hitMods.type == "habanero")
                { 
                    model.GetComponent<Renderer>().material = Resources.Load("red") as Material;
                }
                else
                {
                    model.GetComponent<Renderer>().material = Resources.Load("green") as Material;
                }

                dotDuration = hitMods.dotDuration;
                startDot = true;
            }
        }
    }


