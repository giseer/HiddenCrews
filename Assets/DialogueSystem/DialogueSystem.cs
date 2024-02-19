using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] textLines;
    [SerializeField] float textSpeed = 0.2f;

    int index;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        index = 0;
        
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (var line in textLines) 
        {
        
        }
        yield return new WaitForSeconds(textSpeed);
    }
}
