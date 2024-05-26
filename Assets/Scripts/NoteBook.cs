using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteBook : MonoBehaviour
{
    private PlayerInputs inputs;
    private InputAction NotebookIn;

    [SerializeField] private GameObject notebook;

    private void Awake()
    {
        inputs = new PlayerInputs();
    }
    private void OnEnable()
    {
        NotebookIn = inputs.Player.Note;
        NotebookIn.Enable();
    }

    private void OnDisable()
    {
        NotebookIn.Disable();
    }

    bool canOpen;
    void Update()
    {
        
        if (NotebookIn.IsPressed() && canOpen)
        {
            notebook.SetActive(!notebook.activeInHierarchy);
            PlayerDisable.Instance.DisablePMovement(!notebook.activeInHierarchy);
            canOpen = false;
        }

        if (NotebookIn.WasReleasedThisFrame())
        {
            canOpen = true;
        }
    }
}
