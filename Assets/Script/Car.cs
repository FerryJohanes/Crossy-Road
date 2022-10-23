using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    int extent;
    private void Update()
    {
        //transform.position += Vector3.forward + Time.deltaTime * speed;
        transform.Translate(Vector3.forward * Time.deltaTime * Random.Range(1.5f, 3f));

        if (this.transform.position.x < - (extent + 1) ||
            this.transform.position.x > extent + 1)
        Destroy(this.gameObject);
    }

    public void Setup(int extent)
    {
        this.extent = extent;
    }
}
