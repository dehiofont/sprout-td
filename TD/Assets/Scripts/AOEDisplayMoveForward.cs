using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDisplayMoveForward : MonoBehaviour
{
    public float distance;
    public Vector3 destination;
    public float timer;
    public Vector3 startPosition;
    public float maxTime;

    public GameObject projectileParent;

    public float zPosition;

    // Start is called before the first frame update
    void Start()
    {
        distance = projectileParent.GetComponent<projectile>().towerStats.radius;
        maxTime = projectileParent.GetComponent<projectile>().aoeGrowthTime;

        if (gameObject.transform.position.z > 0)
        {
            distance *= -1;
        }
        startPosition = transform.position;
        destination = transform.position + transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, destination, timer / maxTime);
        if (timer >= maxTime)
        {
            Destroy(gameObject);
        }
    }
}
