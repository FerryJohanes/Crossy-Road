using UnityEngine;

public class EleMad : MonoBehaviour
{
    [SerializeField] AudioSource Eleaudio;
    bool randplay;
    void Start()
    {
        randplay = Random.value > 0.8f ? true : false;
        if (randplay == true)
        {
            Eleaudio.PlayDelayed(Random.Range(1f, 6f));
        }
    }
}
