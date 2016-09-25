using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour {
    public Transform player;
    public PMovement pMovement;

    public Transform RHPos,LHPos;
    public bool rightLeft;
    public Transform Offset;
    public BoxCollider offsetCollider;
	// Use this for initialization
	void Start () {
        Offset = transform.parent.FindChild("Offset");
        offsetCollider = Offset.GetComponent<BoxCollider>();
        RHPos = transform.parent.FindChild("RHPos");
        LHPos = transform.parent.FindChild("LHPos");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pMovement = player.GetComponent<PMovement>();
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Climb"))
        {
            //pMovement.Climp(RHPos.position, LHPos.position,rightLeft);
        }
        
    }
	
    // Update is called once per frame
	void Update () {
        if (player.position.y > Offset.position.y + 5)
        {
            offsetCollider.enabled = false;
        }
        else
        {
            offsetCollider.enabled = true;
        }
	
    }
}
