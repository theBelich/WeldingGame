using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place This Class on Gameobject. He will serve as Particle
/// </summary>
public class ParticalMovement : MonoBehaviour
{
    [SerializeField] private float _fallSpeed;
    [SerializeField] private float _horizontalSpeed = 1;

    void Update()
    {
        transform.Translate(new Vector3(Random.Range(-0.5f, 0.5f) * _horizontalSpeed, _fallSpeed, Random.Range(-0.5f, 0.5f) * _horizontalSpeed) * Time.deltaTime);    
    }
}
