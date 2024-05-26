using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneTrackerScript : MonoBehaviour
    {
        private static SceneTrackerScript _instance;
        public static SceneTrackerScript Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }

            currentSceneName = null;
        }

        private void Update()
        {
            if(SceneManager.GetActiveScene().name != currentSceneName && currentSceneName != null)
            {
                if (SceneManager.GetSceneByName(currentSceneName).isLoaded)
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentSceneName));
                    Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
                }
            }
        }

        public void PutThingInRightScene(GameObject thing)
        {
            if(thing.scene.name != currentSceneName && currentSceneName != null)
                SceneManager.MoveGameObjectToScene(thing, SceneManager.GetSceneByName(currentSceneName));
        }


        public Transform PrevDoorPos;
        public int DoorID;

        public string currentSceneName;

    }

}
