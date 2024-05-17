using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour, IInteractable //this is called an interface, look into it if you do not know what it is, they are going to be very helpful
{

    [SerializeField] public Transform PlayerSpawner;
    [SerializeField] private string GoToScene;
    [SerializeField] public int DoorID;

    public void Interact()
    {
        SceneManagement.SceneTrackerScript.Instance.DoorID = DoorID;
        SceneManager.UnloadSceneAsync(gameObject.scene);
        SceneManager.LoadSceneAsync(GoToScene);
    }
}
