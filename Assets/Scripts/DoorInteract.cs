using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour, IInteractable 
{

    [SerializeField] public Transform PlayerSpawner;
    [SerializeField] private string GoToScene;
    [SerializeField] public int DoorID;

    public void Interact()
    {
        SceneManagement.SceneTrackerScript.Instance.DoorID = DoorID;
        SceneManagement.SceneTrackerScript.Instance.currentSceneName = GoToScene;
        SceneManager.UnloadSceneAsync(gameObject.scene);
        SceneManager.LoadSceneAsync(GoToScene, LoadSceneMode.Additive);

        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

}