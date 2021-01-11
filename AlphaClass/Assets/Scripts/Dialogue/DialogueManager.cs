using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public TextAsset textFile;     // drop your file here in inspector
    public static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            UnityEngine.Debug.LogWarning("fix this" + gameObject.name );
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
      //  string text = textFile.text;  //this is the content as string
      //  byte[] byteText = textFile.bytes;  //this is the content as byte array
    }

    private void textGen()
    {
        
        // TextFile Generation
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = false;
        cmd.StartInfo.UseShellExecute = false;
        //cmd.StartInfo.Arguments = "ECHO Hello World";
        cmd.Start();

        string cPath = System.IO.Directory.GetCurrentDirectory();
        UnityEngine.Debug.Log(cPath);

        cmd.StandardInput.WriteLine("d:");
        cmd.StandardInput.WriteLine("cd " + cPath);
        cmd.StandardInput.WriteLine("cd textgen");
        cmd.StandardInput.WriteLine("python generator.py");
        //cmd.StandardInput.WriteLine("PAUSE");
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();
        UnityEngine.Debug.Log(cmd.StandardOutput.ReadToEnd());
        // UnityEngine.Debug.Log("Heeyyyyyyy TEXTGENNNN");
        

        /*
        string strCmdText;
        strCmdText = "/C pause";
        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        */

    }

    public void EnqueueDialogue(DialogueBase db)
    {
        // Start TextFile Generation
        textGen();

        dialogueBox.SetActive(true);
        dialogueInfo.Clear();
        db.Init();

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
        dialogueText.text = info.myText;
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