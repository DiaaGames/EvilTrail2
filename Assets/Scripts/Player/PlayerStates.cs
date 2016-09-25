using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStates : MonoBehaviour {
    public List<Sprite> weaponSprites, armorSprites, itemsSprites;
    public static PlayerStates PS;

    public void AddItem(string itemType,Sprite ItemImage)
    {

        if (itemType == "Weapon")
        {
            weaponSprites.Add(ItemImage);
        }

        if (itemType == "Armor")
        {
            armorSprites.Add(ItemImage);
        }

        if (itemType == "Item")
        {
            itemsSprites.Add(ItemImage);
        }

    }
    void Awake()
    {
        if (PS == null)
        {
            PS = this;
        }
        if (PS != null && PS != this)
        {
            Destroy(gameObject);
        }
    }

}
