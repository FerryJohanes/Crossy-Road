using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    
    Player player;

    void Update()
    {
        if (this.transform.position.z <= player.CurrentTravel - 12)
            GameObject.Destroy(this.gameObject);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
        if(this.transform.position.z <= player.CurrentTravel && player.gameObject.activeInHierarchy)
        {
            player.gameObject.SetActive(false);
        }
    }

    public void SetUpTarget(Player target)
    {
        this.player = target;
    }
}
