using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    private bool playerInRange;

    private bool isPressing; 

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            
        }
        else
        {
            visualCue.SetActive(false);
            if (isPressing)
            {
                Debug.Log(InkJSON.text);
            }
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

    public void onPress(InputAction.CallbackContext context)
    {
        isPressing = context.ReadValueAsButton();
    }
}
