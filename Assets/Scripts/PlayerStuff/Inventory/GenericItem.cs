using UnityEngine;

[CreateAssetMenu(menuName = "New Items/Basic Item")]
public class GenericItem : ScriptableObject
{
    [SerializeField] string itemName = "";
    [SerializeField] Sprite Icon = null;

    [Tooltip("leave blank if not a key")]
    public string KeyID = "";
    // im actually not sure this^ is necessary

}
