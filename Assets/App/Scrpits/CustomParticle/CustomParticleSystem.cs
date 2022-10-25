using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleSystem : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private Vector3 _particleSize;
    [SerializeField] private float _maxLifeTime = 1;
    [SerializeField] private int _maxParticles = 100;

    private List<GameObject> _particles = new List<GameObject>();

    /// <summary>
    /// spawns Particle onScene
    /// </summary>
    public void SpawnParticle()
    {
        if (_particles.Count > _maxParticles) return;

        var particle = Instantiate(_particlePrefab);
        particle.transform.localScale = _particleSize;
        _particles.Add(particle);
        StartCoroutine(LateDestroy(particle));
    }
    /// <summary>
    /// spawns particle as child this transform
    /// </summary>
    /// <param name="parentTransform"></param>
    public void SpawnParticle(Transform parentTransform)
    {
        if (_particles.Count > _maxParticles) return;

        var particle = Instantiate(_particlePrefab, parentTransform);
        particle.transform.localScale = _particleSize;
        _particles.Add(particle);
        StartCoroutine(LateDestroy(particle));
    }

    /// <summary>
    /// spawns Particle on vector coordinates
    /// </summary>
    /// <param name="vector"></param>
    public void SpawnParticle(Vector3 vector)
    {
        if (_particles.Count > _maxParticles) return;

        var particle = Instantiate(_particlePrefab, vector, Quaternion.identity);
        particle.transform.localScale = _particleSize;
        _particles.Add(particle);
        StartCoroutine(LateDestroy(particle));
    }

    public IEnumerator LateDestroy(GameObject objectToDestroy)
    {
        float random = UnityEngine.Random.Range(0.5f, _maxLifeTime);
        yield return new WaitForSeconds(random);
        _particles.Remove(objectToDestroy);
        Destroy(objectToDestroy);
    }
    /// <summary>
    /// spawns under mousePosition
    /// (Position gets from MainCamera)
    /// </summary>
    public void SpawnParticleUnderMouse()
    {
        RaycastHit hit;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            return;
        SpawnParticle(hit.point);
    }

}
