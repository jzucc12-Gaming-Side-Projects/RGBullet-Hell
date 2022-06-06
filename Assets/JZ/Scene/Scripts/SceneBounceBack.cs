using System.Collections;
using JZ.SCENE.BUTTON;
using UnityEngine;

namespace JZ.SCENE
{
    /// <summary>
    /// Activates scene transition after inactivity on current scene for too long
    /// </summary>
    public class SceneBounceBack : MonoBehaviour
    {
        [SerializeField] private float waitTimerInSeconds = 60f;
        [SerializeField] private SceneButtonFunction transitionButton;

        private void Awake() 
        {
            StartCoroutine(WaitToBouncBack());
        }

        private IEnumerator WaitToBouncBack()
        {
            float currTimer = 0;
            yield return new WaitUntil(() =>
            {
                if(JZ.INPUT.Utils.AnyKeyOrButton())
                    currTimer = 0;
                else
                    currTimer += Time.deltaTime;

                return currTimer >= waitTimerInSeconds;
            });

            transitionButton.OnClick();
        }
    }
}
