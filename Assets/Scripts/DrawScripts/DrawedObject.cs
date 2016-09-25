using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawedObject : MonoBehaviour {

    public float distanceToPlayer;
    void OnEnable()
    {
        PMovement.draw += Draw;
    }

    void OnDisable()
    {
        PMovement.draw -= Draw;
    }
  public  void Draw()
    {
        distanceToPlayer = Vector3.Distance(transform.position, ResourcesHolder.Player.position);
        if(distanceToPlayer <30){
            
        transform.position = Vector3.Lerp(transform.position,ResourcesHolder.Player.position,Time.deltaTime * ResourcesHolder.RH.drawSpeed);

        }
        
    }
}
