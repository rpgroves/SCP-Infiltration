using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip1;
    [SerializeField] GameObject DialogueBoxPrefab;
    DialogueBox dialogueBox;
    TextMeshProUGUI textComponent;
    [SerializeField] DialogueSO dialogueSO;
    [SerializeField] float textSpeed;
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite speakerSprite;
    bool dialogueOn = false;
    int index;
    string[] lines;

    void Start()
    {
        source.clip = clip1;
    }

    void Update()
    {
        if(dialogueOn)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
    }

    public void StartDialogue()
    {
        dialogueOn = true;

        dialogueBox = Instantiate(DialogueBoxPrefab, GameObject.Find("Canvas").transform).GetComponent<DialogueBox>();
        textComponent = dialogueBox.GetTextComponent();
        
        lines = dialogueSO.GetLines();
        textComponent.text = "";
        index = 0;
        StartCoroutine(TypeLine());

        dialogueBox.SetImageLeft(playerSprite);
        dialogueBox.SetImageRight(speakerSprite);
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            source.pitch = Random.Range(.7f, .9f);
            source.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueOn = false;
            dialogueBox.DialogueEnd();
        }
    }
}
