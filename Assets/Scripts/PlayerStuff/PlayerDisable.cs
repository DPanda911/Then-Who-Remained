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

    [SerializeField] private GameObject player;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void SetCurrentPlayerOBJ(GameObject p)
    {
        player = p;

        p_MovementHandler = player.GetComponent<MovementHandler>();
        p_Interact = player.GetComponent<InteractionHandler>();

    }

    public void CompleteDisable(bool enable)
    {
        DisableNote(enable);
        DisablePInteract(enable);
        DisablePMovement(enable);
    }

    public void DisablePMovement(bool enable)
    {
        p_MovementHandler.enabled = !enable;
    }

    public void DisablePInteract(bool enable)
    {
        p_Interact.enabled = !enable;
    }
    public void DisableNote(bool enable)
    {
        p_NoteBook.enabled = !enable;
    }

}   