using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 pos;
    float posZ;

    public void Awake()
    {
        posZ = transform.position.z;
    }

    private void Update()
    {
        pos = player.position;
        pos.z = posZ;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}
