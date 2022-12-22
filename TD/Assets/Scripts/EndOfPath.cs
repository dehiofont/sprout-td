using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfPath : MonoBehaviour
{
    public GameObject gamelogic;
    // Start is called before the first frame update
    void Start()
    {
        gamelogic = GameObject.Find("GameLogic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gamelogic.GetComponent<LevelLogic>().LifeCounter(-1);
        }

    }
}
