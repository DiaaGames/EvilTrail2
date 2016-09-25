using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour {
    public string ItemType;
    public Sprite ItemSprite;

    public float Timer=1;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStates.PS.AddItem(ItemType, ItemSprite);
            Destroy(gameObject);
        }
    }
    IEnumerator ItemMovement()
    {
        while (Timer > 0)
        {
            Debug.Log("down");
            Timer -= Time.deltaTime;
            transform.position -= Vector3.up * Time.deltaTime / 10;
            yield return null;
        }

        while (Timer < 0 && Timer > -1)
        {    Debug.Log("up");
            Timer -= Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime / 10;
            yield return null;
        }
     
        Timer = 1;
        StartCoroutine("ItemMovement");
    }
    void Start()
    {
        
        StartCoroutine("ItemMovement");

    }
}
