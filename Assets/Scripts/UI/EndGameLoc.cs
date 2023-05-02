using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameLoc : MonoBehaviour
{
    [SerializeField] private string _text;
    private Localization localization;
    void Start()
    {
        localization = GetComponent<Localization>();
        if(localization is not null && _text is not null)
        {
            GetComponent<TextMeshProUGUI>().text = localization.GetEndGameText(_text);
        }
    }

}
