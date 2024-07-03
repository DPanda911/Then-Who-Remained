using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class buttonPress : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EventSystem eventSystem;
    
    void Start()
    {
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onPress(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            Button button = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            button.onClick.Invoke();
        }
    }
}
