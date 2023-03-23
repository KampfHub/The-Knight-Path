using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image[] _iconSlots;
    [SerializeField] private Image[] _iconBorders;
    private Color[] setAlfa = new Color[6];
    private GameObject playerRef, hpBar, defenseBar;
    private void Awake()
    {
        playerRef = GameObject.FindWithTag("Player");
        hpBar = GameObject.Find("HPBar");
        defenseBar = GameObject.Find("DefenseBar");
        HideAllIcons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Start()
    {
        
    }
    private IEnumerator ChangeBorderOnTimer(int slot, float duration)
    {
        float fillValue = duration;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            _iconBorders[slot].fillAmount = duration/fillValue;
            yield return null;
        }
    }
    public void ÑhangeWidgetValue()
    {
        if (hpBar is not null)
        {
           hpBar.GetComponent<Image>().fillAmount = playerRef.GetComponent<Player>().GetFormattingHPValueToWidget(); 
        }
        if (defenseBar is not null)
        {
            defenseBar.GetComponent<Image>().fillAmount = playerRef.GetComponent<Player>().GetFormattingDefenceValueToWidget();
        }  
    }
    public void SetIconInSlot(string impactName, float duration)
    {
        switch (impactName)
        {
            case "JumpForce": ShowIcon(0, 1); Invoke("HideIconJumpForce", duration); StartCoroutine(ChangeBorderOnTimer(0, duration)); break;
            case "Speed": ShowIcon(1, 1); Invoke("HideIconSpeed", duration); StartCoroutine(ChangeBorderOnTimer(1, duration)); break;
            case "Poison": ShowIcon(2, 1); Invoke("HideIconBrokenSword", duration); StartCoroutine(ChangeBorderOnTimer(2, duration)); break;
            case "Power": ShowIcon(3, 1); Invoke("HideIconPower", duration); StartCoroutine(ChangeBorderOnTimer(3, duration)); break;
            case "Trap": ShowIcon(4, 1); Invoke("HideIconLowSpeed", duration); StartCoroutine(ChangeBorderOnTimer(4, duration)); break;
            case "Immortal": ShowIcon(5, 1); Invoke("HideIconImmortal", duration); StartCoroutine(ChangeBorderOnTimer(5, duration)); break;
        }
    }
    public void ShowIcon(int slot, int transparent)
    {
        //transparent 1 = visible, 0 = invisible;
        setAlfa[slot] = _iconSlots[slot].color;
        setAlfa[slot].a = transparent;
        _iconSlots[slot].color = setAlfa[slot];
        _iconBorders[slot].color = setAlfa[slot];
    }


    private void HideAllIcons()
    {
        for(int i = 0; i< _iconSlots.Length; i++)
        {
            setAlfa[i] = _iconSlots[i].color;
            setAlfa[i].a = 0;
            _iconSlots[i].color = setAlfa[i];
            _iconBorders[i].color = setAlfa[i];
        }
    }
    private void HideIconJumpForce()
    {
        ShowIcon(0, 0);
    }
    private void HideIconSpeed()
    {
        ShowIcon(1, 0);
    }
    private void HideIconBrokenSword()
    {
        ShowIcon(2, 0);
    }
    private void HideIconPower()
    {
        ShowIcon(3, 0);
    }
    private void HideIconLowSpeed()
    {
        ShowIcon(4, 0);
    }
    private void HideIconImmortal()
    {
        ShowIcon(5, 0);
    }
}
