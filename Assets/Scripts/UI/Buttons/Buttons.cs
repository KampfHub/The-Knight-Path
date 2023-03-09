using UnityEngine;
public class Buttons : MonoBehaviour
{
    public event PlayerTrigger JumpTrigger;
    public event PlayerTrigger AttackTrigger;
    private bool isPauseButtonPressed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SettingsButton()
    {
        if (isPauseButtonPressed) Pause(false);
        else Pause(true);
    }
    public void JumpButton()
    {
        JumpTrigger();
    }
    public void AttackButton()
    {
        AttackTrigger();
    }
    private void Pause(bool pauseState)
    {
        if (pauseState) Time.timeScale = 0;
        else Time.timeScale = 1;
        isPauseButtonPressed = pauseState;
    }
}
