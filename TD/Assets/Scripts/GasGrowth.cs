using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGrowth : MonoBehaviour
{
    public float randomMaxSize;
    public float growStartDirection;
    public int growDirection = 1;
    public float sphereStartSize;
    public float growingSize;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        sphereStartSize = transform.localScale.y;
        randomMaxSize = Random.Range((sphereStartSize + .075f), 0.7f);
        speed = Random.Range(0.1f, 0.3f);
        growStartDirection = Random.Range(0.1f, 1.1f);
        
        if(growStartDirection > .51f)
        {
            growDirection = -1;
            growingSize = randomMaxSize;
        }
        else
        {
            growingSize = sphereStartSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        growingSize += Time.deltaTime * speed * growDirection;
        transform.localScale = new Vector3(growingSize, growingSize, growingSize);
        
        if(growingSize < sphereStartSize && growDirection == -1)
        {
            growDirection = 1;
        }
        else if(growingSize > randomMaxSize && growDirection == 1)
        {
            growDirection *= -1;
        }
    }
}
