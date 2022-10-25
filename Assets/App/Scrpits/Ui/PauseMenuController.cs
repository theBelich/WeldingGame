using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button _quitButton;

    // Start is called before the first frame update
    void Start()
    {
        _quitButton.onClick.AddListener(QuitGame);    
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
