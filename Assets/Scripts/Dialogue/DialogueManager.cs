using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool dialogueIsPlaying;

    

    private static DialogueManager instance;

    


    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("there's more than one?!");
        }
        instance = this;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueText.text = currentStory.currentText;
        PlayerDisable.Instance.DisablePMovement(true);
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        Debug.Log("The story is now over");
        PlayerDisable.Instance.DisablePMovement(false);
    }

    public void onPress(InputAction.CallbackContext context)
    {
        
        Debug.Log("Is onPress working?");
        if (context.started && dialogueIsPlaying)
        {
            Debug.Log("Is context.started working?");
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        Debug.Log("Is continueStory working correctly?");
        if (currentStory.canContinue)
        {
            //dialogueText.text = "Hello?";
            dialogueText.text = currentStory.Continue();
            Debug.Log("Can the story be continued?");

            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;


        //Checks if the UI supports the amount of choices. For the game, our max amount will ALWAYS be four.
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given:" + currentChoices.Count);
        }


        int index = 0;
        //Enable and initialize the choices up to the amount of choices for this line of dialogue.
        foreach(Choice choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;

        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        Debug.Log("Is the coroutine working");
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        Debug.Log("MakeChoice is playing?");
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
