using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoving : MonoBehaviour
{
    public float speed;
    public float strength;

    private float offsetZ;
    // Start is called before the first frame update
    void Start()
    {
        offsetZ = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = Mathf.Sin(Time.time * speed) * strength + offsetZ;
        transform.position = pos;
    }
}
