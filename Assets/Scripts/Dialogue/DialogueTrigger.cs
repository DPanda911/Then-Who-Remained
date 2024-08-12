using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueTrigger : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    

    public bool playerInRange;

    
    
    
    

    

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);

        
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = false; 
        }
    }

    
    public void Interact()
    {
        if (playerInRange)
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying == false)
            {
                DialogueManager.GetInstance().EnterDialogueMode(InkJSON);
            }
        }
        
    }
}
