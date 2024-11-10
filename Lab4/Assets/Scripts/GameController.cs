using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject _onScreenControllers;
    [SerializeField] 
    private bool _isTesting;
    // Start is called before the first frame update
    void Start()
    {
        _onScreenControllers = GameObject.Find("OnScreenControllers");
        if (_isTesting)
        {
            _onScreenControllers.SetActive(Application.platform != RuntimePlatform.WindowsEditor &&
                                        Application.platform != RuntimePlatform.WindowsPlayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
