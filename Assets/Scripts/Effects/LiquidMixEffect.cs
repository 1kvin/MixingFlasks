using Scripts.Entity.Flask;
using Scripts.GameLogic.Actions;
using UnityEngine;

namespace Scripts.Effects
{
    public class LiquidMixEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem filledFlaskEffectPrefab;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip liquidMixSound;
        [SerializeField] private AudioClip liquidFullMixSound;

        public void Play(FlaskGameObject flaskGameObject)
        {
            if (flaskGameObject.flask.IsHomogeneous() && flaskGameObject.flask.IsFull)
            {
                PlayFullLiquidMix();
                var filledFlaskEffect = Instantiate(filledFlaskEffectPrefab, flaskGameObject.transform.position, transform.rotation);
                var filledFlaskEffectMain = filledFlaskEffect.main;
                filledFlaskEffectMain.startColor = flaskGameObject.flask.GetTopLiquid().Color;
            }
            else
            {
                PlayLiquidMix();
            }
        }

        private void PlayLiquidMix()
        {
            audioSource.PlayOneShot(liquidMixSound);
        }

        private void PlayFullLiquidMix()
        {
            audioSource.PlayOneShot(liquidFullMixSound);
        }
    }
}