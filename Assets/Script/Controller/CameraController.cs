using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject Player = null;
    Vector3 delta = new Vector3(3.783489f, 5.363122f, 3.303921f);
    RaycastHit onHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Physics.Raycast(Player.transform.position, delta, out onHit, delta.magnitude, LayerMask.GetMask("Wall"))== true)
        {
            float length = (onHit.point - Player.transform.position).magnitude * 0.8f;
            transform.position = Player.transform.position + delta.normalized * length;
        }
        else
        {
            transform.position = Player.transform.position + delta;
        }
        transform.LookAt(Player.transform);
    }
}