using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] int extent;
    [SerializeField] int frontDistance = 10;

    private void Start()
    {
        for(int z = 0; z < frontDistance; z++)
        {
            var go = Instantiate(road, new Vector3(0,0,z), Quaternion.identity);
            var tb = go.GetComponent<TerrainBlock>();
            tb.Build(extent);
        }
    }
}
