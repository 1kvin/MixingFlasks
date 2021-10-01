using UnityEngine;
using UnityEngine.Serialization;

public class LiquidMixSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip liquidMixSound;
    [SerializeField] private AudioClip liquidFullMixSound;

    public void PlayLiquidMix()
    {
        audioSource.PlayOneShot(liquidMixSound);
    }
    public void PlayFullLiquidMix()
    {
        audioSource.PlayOneShot(liquidFullMixSound);
    }
}
