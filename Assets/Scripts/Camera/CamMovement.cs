using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {
    public Transform player;
    public float zdistance=10,yOffset=0;
    public float camLerpSpeed=2;
    public static Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        player = ResourcesHolder.Player;

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position,new Vector3(player.position.x,player.position.y+yOffset , zdistance),Time.deltaTime*
            (ResourcesHolder.pMovement.SetUpMovementSpeed /1.4f) );
		
	}
}
