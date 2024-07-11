using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using Ink.UnityIntegration;
public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Global Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameTextLeft;
    [SerializeField] private TextMeshProUGUI displayNameTextRight;
    [SerializeField] private Animator portraitAnimatorLeft;
    [SerializeField] private Animator portraitAnimatorRight;

    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private Story currentStory;

    [Header("Checks")]
    public bool dialogueIsPlaying;
    public bool makingChoice;
    private string whichSide;
    private Coroutine displayLineCoroutine;
    private bool canContinueNextLine = false;

    [Header("References")]
    private static DialogueManager instance;
    private DialogueVariables dialogueVariables;


    [Header("UI For Name, Portraits, and their Layouts")]
    private const string speakerTag = "speaker";
    private const string portrait = "portrait";
    private const string layout = "layout";


    


    void Start()
    {
        
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        whichSide = "left";

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

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
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

        dialogueVariables.StartListening(currentStory);

        dialogueText.text = currentStory.currentText;
        PlayerDisable.Instance.DisablePMovement(true);



        //Reset Name, Portrait, and layout.
        displayNameTextLeft.text = "???";
        displayNameTextRight.text = "???";
        portraitAnimatorLeft.Play("default");
        portraitAnimatorRight.Play("default");
        layoutAnimator.Play("middle");


    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        dialogueVariables.StopListening(currentStory);

        PlayerDisable.Instance.DisablePMovement(false);
        displayNameTextLeft.text = "???";
        displayNameTextRight.text = "???";
        portraitAnimatorLeft.Play("default");
        portraitAnimatorRight.Play("default");
        layoutAnimator.Play("middle");
    }

    private void ContinueStory()
    {
        
        
        if (currentStory.canContinue)
        {
            //dialogueText.text = "Hello?";
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine); 
            }
            
            displayLineCoroutine = StartCoroutine(displayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
            DisplayChoices();
            Debug.Log(currentStory.currentText);
            
        }
        else
        {
            ExitDialogueMode();
        }
    }

    public void onPress(InputAction.CallbackContext context)
    {
        
        
        if (context.started && dialogueIsPlaying && !makingChoice)
        {
            
            ContinueStory();
   
        }
    }

    

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        
        if(currentChoices.Count > 0)
        {
            makingChoice = true;
        }
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
        
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        
    }

    public void MakeChoice(int choiceIndex)
    {

        if (canContinueNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            makingChoice = false;
            Debug.Log("MakeChoice worked");
            ContinueStory();
        }

}

        public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    private IEnumerator displayLine(string line)
    {
        dialogueText.text = "";
        canContinueNextLine = false;

        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }

        canContinueNextLine = true;
        Debug.Log("Is this displayLine working?");
    }


    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            //Parses the tag, splitting the key and the value
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //Handles the tag
           if(tagKey == "layout")
            {
                if(tagValue == "left")
                {
                    whichSide = "left";
                    
                }
                if (tagValue == "right")
                {
                    whichSide = "right";
                    
                }
                if (tagValue == "leftStart")
                {
                    whichSide = "leftStart"; 
                    

                }
                if(tagValue == "middle")
                {
                    whichSide = "middle;";
                }
            }

            if (whichSide == "middle")
            {
                switch (tagKey)
                {
                    case speakerTag:
                        displayNameTextLeft.text = tagValue;

                        break;
                    case portrait:
                        
                        portraitAnimatorLeft.Play(tagValue);

                        break;
                    case layout:
                        
                        layoutAnimator.Play(tagValue);

                        break;

                    default:
                        Debug.LogError("Tag came in, but it is not currently being handles: " + tag);
                        break;
                }
            }

            if (whichSide == "leftStart")
            {
                switch (tagKey)
                {
                    case speakerTag:
                        displayNameTextLeft.text = tagValue;

                        break;
                    case portrait:
                        
                        portraitAnimatorLeft.Play(tagValue);

                        break;
                    case layout:
                        
                        layoutAnimator.Play(tagValue);

                        break;

                    default:
                        Debug.LogError("Tag came in, but it is not currently being handles: " + tag);
                        break;
                }
            }

            if(whichSide == "left")
            {
                switch (tagKey)
                {
                    case speakerTag:
                        displayNameTextLeft.text = tagValue;

                        break;
                    case portrait:
                        
                        portraitAnimatorLeft.Play(tagValue);

                        break;
                    case layout:
                        
                        layoutAnimator.Play(tagValue);
                        
                        break;

                    default:
                        Debug.LogError("Tag came in, but it is not currently being handles: " + tag);
                        break;
                }
            }

            if (whichSide == "right")
            {
                switch (tagKey)
                {
                    case speakerTag:
                        displayNameTextRight.text = tagValue;

                        break;
                    case portrait:
                        
                        portraitAnimatorRight.Play(tagValue);

                        break;
                    case layout:
                        
                        layoutAnimator.Play(tagValue);
                        
                        break;

                    default:
                        Debug.LogError("Tag came in, but it is not currently being handles: " + tag);
                        break;
                }
            }

            /*switch (tagKey)
            {
                case speakerTag:
                    displayNameTextLeft.text = tagValue;

                    break;
                case portrait:
                    Debug.Log("Portrait=" + tagValue);
                    portraitAnimatorLeft.Play(tagValue);

                    break;
                case layout:
            Debug.Log(tagValue);
                    layoutAnimator.Play(tagValue);
                    if(tagValue == "left")
                    {
                        Debug.Log("Left side is playing");
                    }
                    if (tagValue == "left")
                    {
                       Debug.Log("Left side is playing");
                    }
            break;

                default:
                    Debug.LogError("Tag came in, but it is not currently being handles: " + tag);
                    break;
            }
            */

        }
            
        }
    }

