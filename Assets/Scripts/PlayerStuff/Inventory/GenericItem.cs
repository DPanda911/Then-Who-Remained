using UnityEngine;

[CreateAssetMenu(menuName = "New Items/Basic Item")]
public class GenericItem : ScriptableObject
{
    public string itemName = "";
    public Sprite Icon = null;

    [Tooltip("leave blank if not a key")]
    public string KeyID = "";
    // im actually not sure this^ is necessary

    [TextArea]
    public string description;
}
