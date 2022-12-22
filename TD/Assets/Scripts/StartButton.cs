using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject sbHighlight;
    public GameObject mainGameLogic;
    public bool waveReady = true;
    // Start is called before the first frame update
    void Start()
    {
        mainGameLogic = GameObject.Find("GameLogic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        sbHighlight.SetActive(true);

        if (Input.GetMouseButtonDown(0) && waveReady)
        {
            mainGameLogic.GetComponent<LevelLogic>().waveReady = true;
        }
    }

    private void OnMouseExit()
    {
        sbHighlight.SetActive(false);
    }
}
