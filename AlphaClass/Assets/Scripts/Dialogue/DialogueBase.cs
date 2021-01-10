using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase: ScriptableObject
{
    [System.Serializable]
    public class InfoInit
    {
        public string myName;
        public Sprite portrait;
        [TextArea(4, 8)]
        public string myText;
        public TextAsset textFile;     // drop your file here in inspector
    }

    public class Info
    {
        public string myName;
        public Sprite portrait;
        [TextArea(4, 8)]
        public string myText;
    }

    [Header("Insert Dialogue Information Below")]
    //public TextAsset textFile;     // drop your file here in inspector
    public InfoInit dialogueInit;
   // public Info[] dialogueInfo;
    public List<Info> dialogueInfo;
    public int DialogueCount;
    //  public Info[] dialogueInfo;

    //public Info tempDialogue;

    public void Init()
    {
      //  dialogueInit.myText = dialogueInit.textFile.text;

        string[] lines = dialogueInit.myText.Split('\r');
       // Info tempDialogue ;
        //Debug.Log("Line 1 : " + lines[1]);
        int i = 0;
       foreach (string dialogueLines in lines)
        {
            Debug.Log("Line 0 : " + dialogueLines);
            dialogueInfo.Add(new Info { myName = dialogueInit.myName, portrait = dialogueInit.portrait, myText = dialogueLines});
            i++;
        }
        DialogueCount = i;
    }
}
