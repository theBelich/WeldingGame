using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wield : MonoBehaviour
{
    //[SerializeField] private GameObject _particle;
    //[SerializeField] private GameObject _weldParticle;
    [SerializeField] private CustomParticleSystem _particleSystem;

    private Camera cam;
    private bool _particleExist = false;
    private bool _weldExist = false;
    GameObject weld;
    //private List<GameObject> particles = new List<GameObject>();
 
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        WieldDetails weldDetails = hit.transform.GetComponent<WieldDetails>();

        if (rend == null ||
            rend.sharedMaterial == null ||
            rend.sharedMaterial.mainTexture == null ||
            meshCollider == null ||
            meshCollider.gameObject.tag != "Connector" || !weldDetails.enabled)
            return;

        _particleSystem.SpawnParticleUnderMouse();

        //StartCoroutine(SpawnParticle(hit));
        //SpawnWeldParticle(hit);
        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        for (int i = 0; i < 5; i++)
        {
            tex.SetPixel((int)pixelUV.x + i, (int)pixelUV.y + i, Color.black);
        }
        
        tex.Apply();
    }

    private IEnumerator SpawnParticle(RaycastHit hit)
    {
        if (!_particleExist)
        {
            //var particle = Instantiate(_particle, cam.ScreenPointToRay(Input.mousePosition).GetPoint(hit.distance), new Quaternion(0,180,0,0));
            _particleExist = true;
            yield return new WaitForSeconds(0.6f);
            //Destroy(particle);
            _particleExist = false;
        }
    }

    private void SpawnWeldParticle(RaycastHit hit)
    {

        if (Input.GetMouseButtonDown(0))
        {
            //weld = Instantiate(_weldParticle, cam.ScreenPointToRay(Input.mousePosition).GetPoint(hit.distance), Quaternion.identity);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //if (weld != null) Destroy(weld);
        }
        //if (weld != null) weld.transform.Translate(cam.ScreenPointToRay(Input.mousePosition).GetPoint(hit.distance) * Time.deltaTime);
    }
}
