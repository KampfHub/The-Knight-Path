using System.Collections.Generic;
using UnityEngine;
public class GOReferencesContainer : MonoBehaviour
{
    private List<GameObject> _uiObjects= new List<GameObject>();
    private GameObject _playerRef, _soundsControllerRef, _levelControllerRef, _GUIRef, _gameDataController;
    private void Awake()
    {
        SearchGameObjectsAndSetReferences();
        FillUIObjectsList();
    }
    private void SearchGameObjectsAndSetReferences()
    {
        _playerRef = GameObject.FindWithTag("Player");
        _soundsControllerRef = GameObject.Find("SoundsController");
        _levelControllerRef = GameObject.Find("LevelController");
        _GUIRef = GameObject.Find("GUI");
        _gameDataController = GameObject.Find("GameDataController");
    }

    public GameObject GetPlayerRef() => _playerRef;
    public GameObject GetSoundControllerRef() => _soundsControllerRef;
    public GameObject GetLevelControllerRef() => _levelControllerRef;
    public GameObject GetGUIRef() => _GUIRef;
    public GameObject GetDataController() => _gameDataController;
    public List<GameObject> GetUIObjects() => _uiObjects;
    private void FillUIObjectsList()
    {
        foreach (Transform child in _GUIRef.GetComponentsInChildren<Transform>())
        {
            _uiObjects.Add(child.gameObject);
        }
    }
}
