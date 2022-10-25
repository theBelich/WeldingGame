using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.gameObject.GetComponent<DetailController>().isMove = false;
        }
        catch (Exception)
        {
            
        }
    }
}
