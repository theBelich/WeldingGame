using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeNumbersController : MonoBehaviour
{
    [SerializeField] private GameObject _pipePrefab;
    [SerializeField] private Transform _spawnPipePosition;

    public Action OnNumbersChanged;
    public Action<DetailController> OnSpawn { get; set; }

    private DetailController _activeController;
    private int pipeCount = 0;

    public int maxPipeCount { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        SpawnNextPipe();
        OnNumbersChanged += RemovePipe;
        maxPipeCount = UnityEngine.Random.Range(3, 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RemovePipe()
    {
        Destroy(transform.GetChild(0).gameObject);
        pipeCount--;
    }

    private void SpawnNextPipe()
    {
        StartCoroutine(SlowSpawn());
    }

    private IEnumerator SlowSpawn()
    {
       yield return new WaitForSeconds(0.1f);
        var pipe = Instantiate(_pipePrefab, _spawnPipePosition);
        var controller = pipe.GetComponent<DetailController>();
        controller.OnComplete += SpawnNextPipe;
        OnSpawn?.Invoke(controller);
        _activeController = controller;
    }

    public DetailController GetActiveController()
    {
        return _activeController;
    }
}
