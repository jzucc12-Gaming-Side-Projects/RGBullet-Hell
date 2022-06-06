using System;
using System.Collections;
using UnityEngine;

namespace JZ.CORE
{
    /// <summary>
    /// Generates camera shaking
    /// </summary>
    public class ScreenShake : MonoBehaviour
    {
        private static event Action<float, float> onShake = null;


        #region //Monobheaviour
        private void OnEnable() 
        {
            onShake += Shake;
        }

        private void OnDisable()
        {
            onShake -= Shake;
        }
        #endregion

        #region //Shaking
        public static void CallShake(float _duration, float _magnitude)
        {
            onShake?.Invoke(_duration, _magnitude);
        }

        public void Shake(float _duration, float _magnitude)
        {
            StopAllCoroutines();
            StartCoroutine(ShakeRoutine(_duration, _magnitude));
        }

        private IEnumerator ShakeRoutine(float _duration, float _magnitude)
        {
            float currentCount = 0;
            Vector3 originalPosition = transform.localPosition;

            while(currentCount < _duration)
            {
                yield return null;
                currentCount += Time.deltaTime;
                float xShake = UnityEngine.Random.Range(-_magnitude, _magnitude);
                float yShake = UnityEngine.Random.Range(-_magnitude, _magnitude);
                transform.localPosition = new Vector3(xShake, yShake, originalPosition.z);
            }

            transform.localPosition = originalPosition;
        }
        #endregion
    }
}
