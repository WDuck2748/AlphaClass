using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name );
        }
        else
        {
            instance = this;
        }
    }

    public GameObject dialogueBox;

    public Text dialogueName;
    public Text dialogueText;
    public Image dialoguePortrait;
    public float delay = 0.001f;

    public bool outOfRange = true; 

    public Queue<DialogueBase.Info> dialogueInfo; //FIFO Collection

    private void Start()
    {
        dialogueInfo = new Queue<DialogueBase.Info>();
    }

    public void EnqueueDialogue(DialogueBase db)
    {
        dialogueBox.SetActive(true);
        dialogueInfo.Clear();

        foreach(DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();

    }
   
    public void DequeueDialogue()
    {
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        // add in code that detects if we have no more dialgoue and return

        DialogueBase.Info info = dialogueInfo.Dequeue();

        dialogueName.text = info.myName;
        dialogueText.text = info.myText;
        dialoguePortrait.sprite = info.portrait;

        StartCoroutine(TypeText(info));

    }
    
    IEnumerator TypeText(DialogueBase.Info info)
    {
        dialogueText.text = "";
        foreach(char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
    }
    
    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
    }

    public void EnterRangeOfNPC()
    {
        outOfRange = false;
        //dialogueGUI.SetActive(true);
        //if (dialogueActive == true)
        //{
        //    dialogueGUI.SetActive(false);
        //}
    }

    public void OutOfRange()
    {
        outOfRange = true;
        EndOfDialogue();
    }
}
