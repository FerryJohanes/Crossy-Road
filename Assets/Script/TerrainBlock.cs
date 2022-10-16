using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBlock : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject repeat;

    private int extent;

    public int Extent { get => extent;}

    public void Build(int extent)
    {
        this.extent = extent;

        main.transform.localScale = new Vector3(
            x: extent*2+1,
            y: main.transform.localScale.y,
            z: main.transform.localScale.z
        );

        for (int x = -extent; x <= extent; x++)
        {
            if(x==0)
                continue;

            var r = Instantiate(repeat);
            r.transform.SetParent(this.transform);
            r.transform.localPosition = new Vector3(x,0,0);
        }
    }
}
