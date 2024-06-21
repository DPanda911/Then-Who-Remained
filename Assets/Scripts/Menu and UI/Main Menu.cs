using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuAndUI
{
    public class MainMenu : MonoBehaviour
    {

        private void Start()
        {
            if (!SceneManager.GetSceneByName("AlwaysActive").isLoaded)
                SceneManager.LoadSceneAsync("AlwaysActive", LoadSceneMode.Additive);
        }

        public void StartGame()
        {

            SceneManager.LoadSceneAsync("FirstFloorMainHall", LoadSceneMode.Additive);
            StartCoroutine(UnloadMenu());
        }

        public void SettingButton()
        {
            // nah...
            //dont really feel like it
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }

        IEnumerator UnloadMenu()
        {
            SceneManager.UnloadSceneAsync("Main Menu");
            yield return null;
        }
    }
}

