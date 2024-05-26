using UnityEngine;
using Player;
using SceneManagement;

/// <summary>
/// used to disable varius aspects of player interactions for what/when ever we may need
/// </summary>
public class PlayerDisable : MonoBehaviour
{

    private MovementHandler p_MovementHandler;
    private NoteBook p_NoteBook;
    private InteractionHandler p_Interact;

    private static PlayerDisable _instance;
    public static PlayerDisable Instance { get { return _instance; } }

    private GameObject player;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void GetCurrentPlayerOBJ()
    {
        Debug.Log("Hmmm");
        player = GameObject.FindWithTag("Player");

        p_Interact = player.GetComponent<InteractionHandler>();
        p_NoteBook = player.GetComponent<NoteBook>();
        p_Interact = player.GetComponent<InteractionHandler>();


        //p_Interact = FindFirstObjectByType<InteractionHandler>();
        //p_NoteBook = FindFirstObjectByType<NoteBook>();
        //p_Interact = FindFirstObjectByType<InteractionHandler>();
    }

    public void CompleteDisable(bool enable)
    {
        DisableNote(enable);
        DisablePInteract(enable);
        DisablePMovement(enable);
    }

    public void DisablePMovement(bool enable)
    {
        GetCurrentPlayerOBJ();
        p_MovementHandler.enabled = enable;
    }

    public void DisablePInteract(bool enable)
    {
            GetCurrentPlayerOBJ();
        p_Interact.enabled = enable;
    }
    public void DisableNote(bool enable)
    {
            GetCurrentPlayerOBJ();
        p_NoteBook.enabled = enable;
    }

}   