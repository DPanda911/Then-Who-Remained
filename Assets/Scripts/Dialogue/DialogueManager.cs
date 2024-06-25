using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    public bool dialogueIsPlaying;

    [SerializeField] DialogueTrigger trigger;

    private static DialogueManager instance;
    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
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

    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        Debug.Log("The story is now over");
    }

    public void onPress(InputAction.CallbackContext context)
    {
        Debug.Log("Is onPress working?");
        if (context.started)
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
        }
        else
        {
            ExitDialogueMode();
        }
    }

    
}
