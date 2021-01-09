using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public DialogueBase dialogue;
    public bool outOfRange = true;
    public KeyCode DialogueInput = KeyCode.F;

    //  public bool active = false;

    public void TriggerDialogue()
    {
        DialogueManager.instance.EnqueueDialogue(dialogue);
    }

    public void GetNextLine()
    {
        DialogueManager.instance.DequeueDialogue();
        //  if (DialogueManager.instance.dialogueBox.activeSelf == false)
        // {
        //    active = false;
        // }
    }
    /*
    private void Update()
    {
        FindObjectOfType<DialogueManager>().EnterRangeOfNPC();
        if (Input.GetKeyDown(DialogueInput) && DialogueManager.instance.dialogueBox.activeSelf == true)
        {
            GetNextLine();
            if (DialogueManager.instance.dialogueBox.activeSelf == false)
            {
                return;
            }
        }

        if (Input.GetKeyDown(DialogueInput) && DialogueManager.instance.dialogueBox.activeSelf == false)
        {
            //    active = true;
            TriggerDialogue();
        }

    }
    */
    
    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<TestScript>().enabled = true;
        FindObjectOfType<DialogueManager>().EnterRangeOfNPC();

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(DialogueInput) && DialogueManager.instance.dialogueBox.activeSelf == true)
        {
            GetNextLine();
            if (DialogueManager.instance.dialogueBox.activeSelf == false)
            {
                return;
            }
        }

        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(DialogueInput) && DialogueManager.instance.dialogueBox.activeSelf == false)
        {
            //    active = true;
            TriggerDialogue();
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialogueManager>().OutOfRange();
        this.gameObject.GetComponent<TestScript>().enabled = false;
    }
    
}