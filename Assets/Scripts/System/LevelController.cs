using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private int _availableLevel = 1;
    private int _finalLevelID = 9;
    private bool _isPauseButtonPressed;
    private AsyncOperation _asyncOperation;
    public IEnumerator LoadLevel(int levelId, GameObject loadBar)
    {
        Image loadProgressBar = loadBar.GetComponent<Image>();
        yield return null;
        _asyncOperation = SceneManager.LoadSceneAsync(levelId);
        while (!_asyncOperation.isDone)
        {
            float loadProgress = _asyncOperation.progress / 0.9f;
            loadProgressBar.fillAmount = loadProgress;
            yield return null;
        }
    }    
    public int GetCurrentLevelID() => SceneManager.GetActiveScene().buildIndex;
    public int GetAvailableLevel() => _availableLevel;
    public int GetFinalLevel() => _finalLevelID;
    public void AvailableLevelUpgrade(int level) => _availableLevel = level;
    public void QuitGame() => Application.Quit(); 
    public void Pause(bool pauseState)
    {
        if (pauseState) Time.timeScale = 0;
        else Time.timeScale = 1;
        _isPauseButtonPressed = pauseState;
    }
}
