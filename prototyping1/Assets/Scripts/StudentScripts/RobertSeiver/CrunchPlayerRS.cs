using UnityEngine;

public class CrunchPlayerRS : MonoBehaviour
{
    public AudioClip[] Crunches;
    private AudioSource SFX;
    
    // Start is called before the first frame update
    private void Start()
    {
        SFX = GetComponent<AudioSource>();
        SFX.clip = Crunches[Random.Range(0, Crunches.Length)];
        SFX.pitch = Random.Range(0.5f, 1.0f);
        SFX.Play();
        Destroy(gameObject, 3.0f);
    }
}
