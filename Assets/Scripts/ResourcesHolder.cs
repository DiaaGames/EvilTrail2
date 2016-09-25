using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHolder : MonoBehaviour {
    public static Transform Player;
    public static ResourcesHolder RH;
    public  float drawSpeed=1;
    public CamMovement camMovement;
    public static PMovement pMovement;
	// Use this for initialization
	void Awake () {
     
        if (RH == null)
        {
            RH = this;


        }
        if (RH != null && RH != this)
        {
        
            Destroy(gameObject);
        }


        Player = GameObject.FindGameObjectWithTag("Player").transform;

        pMovement = Player.GetComponent<PMovement>();
	}
	
    IEnumerator Start()
    {

        yield return  null;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
