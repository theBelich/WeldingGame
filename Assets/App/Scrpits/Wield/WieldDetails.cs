using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WieldDetails : MonoBehaviour
{
    private WieldCompleteEvent completeEvent;
    private bool isWielded = true;

    public float Percent { get; private set; } = 0;
    public int DetailNumber;

    public Action<float, int> Wielding { get; set; }
    public Action<float, WieldDetails> OnActivate{ get; set; }

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.mainTexture = new Texture2D(100, 100, TextureFormat.RGBA32, true);
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        completeEvent = GameObject.FindGameObjectWithTag("EventContainer").GetComponent<WieldCompleteEvent>();
    }

    private void Update()
    {
        if (Percent > 100)
        {
            isWielded = false;
            completeEvent.Wielded();
            gameObject.GetComponent<Renderer>().material.mainTexture = new Texture2D(100, 100, TextureFormat.RGBA32, true);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            this.enabled = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && isWielded)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                // Do something with mouse input
                Percent += 0.5f;
                Wielding?.Invoke(Percent, DetailNumber);
                //Debug.LogError("Wield");
            }
        }
    }

    private void OnEnable()
    {
        OnActivate?.Invoke(Percent, this);
    }
}
