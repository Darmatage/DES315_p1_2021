using UnityEngine;

public class PlaySFXOnCollisionRS : MonoBehaviour
{
    public AudioClip[] SFX;
    private AudioSource[] sources;

    private void Start()
    {
        sources = new AudioSource[SFX.Length];
        for (int i = 0; i < SFX.Length; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].clip = SFX[i];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (AudioSource source in sources)
                source.Play();
        }
    }
}
