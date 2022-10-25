using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WieldItemCountUiController : MonoBehaviour
{
    [SerializeField] private WieldCompleteEvent WieldCompleteEvent;
    [SerializeField] private TextMeshProUGUI _wieldItemCountText;
    [SerializeField] private PipeNumbersController _pipeNumbersController;
    //TODO: Remove this to another class => TODO: Make new Script-controller for this 
    [SerializeField] private Button _restartButton;
    //[SerializeField] private Button _cancelButton;  

    private int WieldItemCount = 1;
    private int TotalWieldItem = 0;

    void Start()
    {
        StartCoroutine(LateStart());
        WieldCompleteEvent.OnWieldFinished += OnWieldFinished;
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveAllListeners();
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        TotalWieldItem = _pipeNumbersController.maxPipeCount;
        _wieldItemCountText.text = $" {WieldItemCount} / {TotalWieldItem}";
        
    }

    private void OnWieldFinished()
    {
        WieldItemCount++;   
        _wieldItemCountText.text = $" {WieldItemCount} / {TotalWieldItem}";
        if (WieldItemCount == TotalWieldItem)
        {
            _restartButton.gameObject.SetActive(true);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
