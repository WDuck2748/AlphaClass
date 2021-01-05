using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public DialogueBase dialogue;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && DialogueManager.instance.dialogueBox.activeSelf == true)
        {
            GetNextLine();
            if (DialogueManager.instance.dialogueBox.activeSelf == false)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && DialogueManager.instance.dialogueBox.activeSelf == false)
        {
            //    active = true;
            TriggerDialogue();
        }

    }
}