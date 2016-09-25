using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ItemsManager : MonoBehaviour {
    public GameObject[] AllSlots;
    public List<Image> AllSlotsAsImages; 
    public int pauseState;
	// Use this for initialization
    void OnEnable()
    {
        GameManager.pauseGameDelegate += pauseGame;
    }
    void OnDisable()
    {
       // GameManager.pauseGameDelegate  -= pauseGame;
    }
    public void pauseGame(int pausetInt)
    {Debug.Log("pauseGame");
        if (pausetInt == 0)
        {
            ShowWeapons();

        }

        if (pausetInt == 1)
        {
            for (int i = 0; i < AllSlotsAsImages.Count; i++)
            {

                AllSlotsAsImages[i].color = new Color(1, 1, 1, 0);
            }
        
          
        

        }
    }
    IEnumerator Start () {
        AllSlots = GameObject.FindGameObjectsWithTag("ItemSlot");

        for (int i = 0; i < AllSlots.Length; i++)
        {
            for(int y = 0 ;y <AllSlots.Length; y++)
            {        
                if (AllSlots[y].name == "ItemSlot ("+i+")")
                {
                    AllSlotsAsImages.Add(AllSlots[y].GetComponent<Image>());
                }
            }

          
        }
        yield return new WaitForSeconds(0.02f);
        transform.parent.gameObject.SetActive(false);
	}
    void ResetSlots()
    {          for (int i = 0; i < AllSlotsAsImages.Count; i++)
        {
            AllSlotsAsImages[i].color = new Color(0, 0, 0, 0);
            AllSlotsAsImages[i].sprite = null;
        }
    
    }

    public void ShowWeapons()
    {ResetSlots();
        if (PlayerStates.PS.weaponSprites.Count >0)
        {
            for (int i = 0; i < PlayerStates.PS.weaponSprites.Count; i++)
            {

                AllSlotsAsImages[i].sprite = PlayerStates.PS.weaponSprites[i];
                AllSlotsAsImages[i].color = new Color(1, 1, 1, 1);

            }
        }
    }

    public void ShowArmors()
    {ResetSlots();
        if (PlayerStates.PS.armorSprites.Count >0)
        {
            for (int i = 0; i < PlayerStates.PS.armorSprites.Count; i++)
            {

                AllSlotsAsImages[i].sprite = PlayerStates.PS.armorSprites[i];
                AllSlotsAsImages[i].color = new Color(1, 1, 1, 1);

            }

        }
    }

    public void ShowItems()
    {ResetSlots();

        if (PlayerStates.PS.itemsSprites.Count >0)
        {
            for (int i = 0; i < PlayerStates.PS.itemsSprites.Count; i++)
            {

                AllSlotsAsImages[i].sprite = PlayerStates.PS.itemsSprites[i];
                AllSlotsAsImages[i].color = new Color(1, 1, 1, 1);

            }
        }
    }
  
	
	// Update is called once per frame
	void Update () {
     
		
	}
}
