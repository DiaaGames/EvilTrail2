using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
public class Talk : MonoBehaviour {
    public string storyTmpTxt,nameTmpTxt;
    public Text StoryText,TalkerName;
    public string tString  = "{محمد} لماذا أنت هنا؟";
    public char[] AllTextChars;
    public int CharNumber;
    public bool TypingName;
	// Use this for initialization
	void Start () {
        StoryText = GameObject.Find("StoryText").GetComponent<Text>();
        TalkerName = GameObject.Find("TalkerName").GetComponent<Text>();
        CharNumber = 0;
        AllTextChars = tString.ToCharArray();

	}
	
	// Update is called once per frame
	void Update () {

        //StoryText.text = ArabicFixer.Fix("بسم الله");
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine("TypeText");

        }

		
	}

    IEnumerator TypeText()
    {
        while (CharNumber < AllTextChars.Length)
        {
            if (AllTextChars[CharNumber].ToString() == "{")
            {
                TypingName = true;

            }

         
            if (TypingName == true)
            {
                if (AllTextChars[CharNumber].ToString() != "{" && AllTextChars[CharNumber].ToString() != "}")
                {
                    nameTmpTxt += AllTextChars[CharNumber];
                    TalkerName.text = ArabicFixer.Fix(nameTmpTxt);
                }
            }
            if (TypingName == false)
            {
                storyTmpTxt += AllTextChars[CharNumber];

                StoryText.text = ArabicFixer.Fix(storyTmpTxt);

            }
            if (AllTextChars[CharNumber].ToString() == "}")
            {
                TypingName = false;

            }
            CharNumber++;

            yield return new WaitForSeconds(0.1f);

           
        
        }
        
    }
}
