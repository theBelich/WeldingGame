using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Cinemachine;

public class DetailController : MonoBehaviour
{
    [SerializeField] private GameObject _percentInfoPrefab;
    [SerializeField] private List<GameObject> _allDetails;
    [SerializeField] private List<WieldDetailsContainer> _wieldingDetails;

    private List<GameObject> _selectedDetails = new List<GameObject>();

    private WieldCompleteEvent _completeEvent;
    private int _countToOpenDetail = 0;
    private int _numberDetailToOpen = 2;
    private int _detailNumber = 0;

    public Action OnComplete;
    public Action<WieldDetails> OnChangeDetail;
    public bool isMove;

    private void Awake()
    {
        _completeEvent = GameObject.FindGameObjectWithTag("EventContainer").GetComponent<WieldCompleteEvent>();
        InitTextInfo();
    }
    
    void Start()
    {
        isMove = true;
        foreach (var detail in _allDetails)
        {
            detail.SetActive(false);
        }    
        _allDetails[0].SetActive(true);
        _allDetails[1].SetActive(true);
        _completeEvent.OnWieldComplete += NextDetail;        
    }

    private void OnDestroy()
    {
        _completeEvent.OnWieldComplete -= NextDetail;

    }

    private void Update()
    {
        if (!isMove) return;        
        transform.Translate(0, 0, 1 * Time.deltaTime);
    }

    private void NextDetail()
    {
        _countToOpenDetail++;
        try
        {
            if (!_allDetails[_numberDetailToOpen].activeSelf && _countToOpenDetail == _wieldingDetails[_numberDetailToOpen - 2].wieldDetails.Count)
            {
                ResetTextInfo();
                _allDetails[_numberDetailToOpen].SetActive(true);//activate next detailSet

                _countToOpenDetail = 0;
                _numberDetailToOpen++;

                OnChangeDetail?.Invoke(_wieldingDetails[_numberDetailToOpen - 2].wieldDetails[0]);
                InitTextInfo();//Set nextDetailSet

                return;
            }
        }
        catch (Exception)
        {
            OnComplete?.Invoke();
            ResetTextInfo();
            isMove = true;
            OnChangeDetail?.Invoke(null);
        }
    }

    public List<WieldDetails> GetCurrentDetails()
    {
        List<WieldDetails> details = new List<WieldDetails>();

        details = _wieldingDetails[_numberDetailToOpen - 2].wieldDetails;

        return details;
    }

    private void CreatePercentInfo(float percent, WieldDetails wieldDetail)
    {
        var percentInfoContainer = GameObject.FindGameObjectWithTag("percentInfoContainer").GetComponent<Transform>();
        var percentInfo = Instantiate(_percentInfoPrefab, percentInfoContainer);
        
        _selectedDetails.Add(percentInfo);

        var percentInfoUi = percentInfo.GetComponent<PercentInfoUiController>();
        percentInfoUi.InitPercentInfoUiController(_detailNumber, percent);

        wieldDetail.DetailNumber = _detailNumber;

        _detailNumber++;
    }

    private void UpdateText(float percent, int detailNumber)
    {
        _selectedDetails[detailNumber].GetComponent<PercentInfoUiController>().UpdatePercentageDetail(percent);
    }

    private void InitTextInfo()
    {
        foreach (var detail in GetCurrentDetails())
        {
            CreatePercentInfo(detail.Percent, detail); 
            detail.Wielding += UpdateText;
        }
    }

    private void OnResetTextInfo()
    {
        foreach (var detail in GetCurrentDetails())
        {
            detail.OnActivate -= CreatePercentInfo;
            detail.Wielding -= UpdateText;
        }
    }
    private void ResetTextInfo()
    {
        OnResetTextInfo();
        foreach (var detail in _selectedDetails)
        {
            Destroy(detail);
        }
    }
}



[Serializable]
public class WieldDetailsContainer
{
    [Tooltip("GroupOfDetails"), BoxGroup("Details")]
    public List<WieldDetails> wieldDetails;
}

