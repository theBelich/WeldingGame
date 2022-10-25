using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PipeNumbersController _pipeNumbersController;
    [SerializeField] private List<Transform> _transformList;

    private int zoom = 9;
    private int normal = 60;
    private int smooth = 5;
    private bool isZoomed = false;
    
    void Start()
    {
        _transformList.Add(_camera.transform);
        _pipeNumbersController.OnSpawn += UpdateTransformList;
        
    }

    private void UpdateTransformList(DetailController controller)
    {
        StartCoroutine(SlowUpdateTransformList(controller));
    }

    private IEnumerator SlowUpdateTransformList(DetailController controller)
    {
        yield return new WaitForSeconds(3f);
        _transformList.Add(controller.GetCurrentDetails()[0].transform);
        isZoomed = true;
        controller.OnChangeDetail += UpdateTransformList;
    }

    private void UpdateTransformList(WieldDetails details)
    {
        if (details == null)
        {
            isZoomed = false;
            //_transformList.RemoveRange(1, _transformList.Count - 1);
            //_camera.transform.position = _transformList[0].position;
        }
        else
        {
            _transformList.Add(details.transform);
            _camera.transform.LookAt(_transformList[_transformList.Count - 1 ]);
            
        }
    }

    private void Update()
    {

        if (isZoomed)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, zoom, Time.deltaTime * smooth);
            _camera.transform.LookAt(_transformList[_transformList.Count - 1]);
        }
        else
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, normal, Time.deltaTime * smooth);
            transform.rotation = _transformList[0].rotation;
        }
        //_camera.transform.LookAt(_transformList[_transformList.Count - 1]);
    }
}
