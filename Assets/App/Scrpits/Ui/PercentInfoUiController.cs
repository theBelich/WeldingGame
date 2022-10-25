using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PercentInfoUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _percentageDetail;
    private int _numberDetail;

    public void InitPercentInfoUiController(int numberDetail, float percent)
    {
        _percentageDetail.text = $"No. {numberDetail} {percent}%";
        _numberDetail = numberDetail;
    }
    public void UpdatePercentageDetail(float percent)
    {
        _percentageDetail.text = $"No.{_numberDetail}| {percent}%";
    }
}
