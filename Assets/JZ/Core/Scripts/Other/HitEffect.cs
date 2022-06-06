using System.Collections;
using UnityEngine;

namespace JZ.CORE
{
    /// <summary>
    /// Creates a hit effect on the attached sprite renderer
    /// </summary>
    public class HitEffect : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer = null;
        [SerializeField] private float flashDuration = 0.06f;
        [SerializeField, Min(1)] private int numberOfFlashes = 1;
        private Material hitEffectMaterial = null;
        private Material baseMaterial = null;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            baseMaterial = spriteRenderer.material;
            hitEffectMaterial = Resources.Load<Material>("Hit Effect");
        }

        public void StartHitEffect()
        {
            StartCoroutine(HitEffectRoutine());
        }

        private IEnumerator HitEffectRoutine()
        {
            for(int ii = 0; ii < numberOfFlashes; ii++)
            {
                spriteRenderer.material = hitEffectMaterial;
                yield return new WaitForSeconds(flashDuration);
                spriteRenderer.material = baseMaterial;
                yield return new WaitForSeconds(flashDuration);
            }

        }

        public float GetEffectDuration() { return 2 * flashDuration * numberOfFlashes; }
    }
}