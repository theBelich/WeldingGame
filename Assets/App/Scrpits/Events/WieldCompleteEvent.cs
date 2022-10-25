using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WieldCompleteEvent : MonoBehaviour
{
    public Action OnWieldComplete { get; set; }
    public Action OnWieldFinished { get; set; }
    public void Wielded()
    {
        OnWieldComplete?.Invoke();
    }

    public void Finished()
    {
        OnWieldFinished?.Invoke();
    }
}
