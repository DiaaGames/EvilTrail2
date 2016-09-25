using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Transform player;
    public int pauseState;
    public GameObject PauseMenu;
    public delegate void PauseGame(int pauseInt);
    public static PauseGame pauseGameDelegate;
    public static GameManager GM;
	// Use this for initialization
	void Start () {
        GM = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PauseMenu = GameObject.Find("PauseMenu");
		
	}
    void OnEnable()
    {
        pauseGameDelegate += pauseGame;
    }
    void OnDisable()
    {
        pauseGameDelegate -= pauseGame;
    }
    void pauseGame(int pauseInt)
    {
        if (pauseState == 0)
        {

            Time.timeScale = 0;
            PauseMenu.SetActive(true);


        }

        if (pauseState == 1)
        {
            Time.timeScale =1;
            PauseMenu.SetActive(false);
        }
        pauseState++;
        if (pauseState > 1)
        {
            pauseState = 0;

        }
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {

            pauseGameDelegate(pauseState);


        }
        if (player.position.y < -20)
        {
            player.position = GameObject.Find("StartPos").transform.position;
        }





	}

}
