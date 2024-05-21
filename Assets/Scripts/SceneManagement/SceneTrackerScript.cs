using UnityEngine;

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
        }

        public Transform PrevDoorPos;
        public int DoorID;

    }

}
