using TMPro;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private GameObject ownerRef;
    private Localization localization;
    void Start()
    {
        LocalizationPreporation();
        SetDialogueTextValue();
    }
    private void SetDialogueTextValue()
    {
        ownerRef.GetComponent<TextMeshProUGUI>().text = localization.GetDialogueText(ownerRef.name);
    }
    private void LocalizationPreporation()
    {
        if (GetComponent<Localization>() is not null)
        {
            localization = GetComponent<Localization>();
        }
        else
        {
            gameObject.AddComponent<Localization>();
            localization = GetComponent<Localization>();
        }
    }

}
