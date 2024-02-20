using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] string[] textLines;
    [SerializeField] float textSpeed = 0.1f;

    int index;



    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) 
        {
            if(dialogueText.text == textLines[index]) 
            {
               NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = textLines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in textLines[index].ToCharArray()) 
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if(index < textLines.Length - 1) 
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine (WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
