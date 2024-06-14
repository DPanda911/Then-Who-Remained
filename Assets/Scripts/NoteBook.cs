using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NoteBook : MonoBehaviour
{
    private PlayerInputs inputs;
    private InputAction NotebookIn;
    private InputAction NextIn;
    private InputAction PrevIn;

    [SerializeField] private GameObject notebook;

    private TMP_InputField[] notePage;
    private GameObject[] notePagesDisplay_GO;

    private int notePageIndex;
    private void Awake()
    {
        inputs = new PlayerInputs();
        notePage = GetComponentsInChildren<TMP_InputField>(true);
        notePagesDisplay_GO = new GameObject[notePage.Length];
        for (int i = 0; i < notePage.Length; i++)
        {
            notePagesDisplay_GO[i] = notePage[i].gameObject;
        }
        notePageIndex = 0;
    //    Debug.Log(notePagesDisplay_GO.Length);
    }
    private void OnEnable()
    {
        NotebookIn = inputs.Player.Note;
        NextIn = inputs.Player.Next;
        PrevIn = inputs.Player.Previous;

        NotebookIn.Enable();
        NextIn.Enable();
        PrevIn.Enable();
        DisableNotes();
        EnableCurrentNote(notePageIndex);
    }

    private void OnDisable()
    {
        NotebookIn.Disable();
        NextIn.Disable();
        PrevIn.Disable();
    }

    bool canOpen = true;
    bool isOpen = false;
    bool pressN = true;
    bool pressP = true;
    void Update()
    {

        if (NotebookIn.IsPressed() && canOpen && !isOpen)
        {
            notebook.SetActive(true);
            PlayerDisable.Instance.DisablePMovement(true);
           // PlayerDisable.Instance.DisablePMovement(!notebook.activeInHierarchy);
            canOpen = false;
            isOpen = true;
        }
        if(NotebookIn.IsPressed() && canOpen && isOpen)
        {
            notebook.SetActive(false);
            PlayerDisable.Instance.DisablePMovement(false);
            canOpen = false;
            isOpen = false;
        }

        if (NotebookIn.WasReleasedThisFrame())
        {
            canOpen = true;

        }

        if(notebook.activeInHierarchy)
        {
            if(NextIn.IsPressed() && pressN)
            {
                pressN = false;
                Debug.Log("Next");
                NextNotePage();
            }
            if (NextIn.WasReleasedThisFrame())
            {
                pressN = true;
            }

            if (PrevIn.IsPressed() && pressP)
            {
                pressP = false;
                Debug.Log("Prev");
                PrevNotePage();
            }
            if (PrevIn.WasReleasedThisFrame())
            {
                pressP = true;

            }

        }


    }

    private void NextNotePage()
    {
        DisableNotes();
        notePageIndex++;
        if(notePageIndex >= notePagesDisplay_GO.Length)
            notePageIndex = 0;
        EnableCurrentNote(notePageIndex);
    }

    private void PrevNotePage()
    {
        DisableNotes();
        notePageIndex--;
        if (notePageIndex < 0)
            notePageIndex = notePagesDisplay_GO.Length - 1;
        EnableCurrentNote(notePageIndex);
    }

    private void DisableNotes()
    {
        foreach (GameObject note in notePagesDisplay_GO)
        {
            note.SetActive(false);
        }

    }

    private void EnableCurrentNote(int toEnable)
    {
        notePagesDisplay_GO[toEnable].SetActive(true);
    }
}
