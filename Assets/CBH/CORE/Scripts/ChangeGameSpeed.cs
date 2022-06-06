using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using JZ.CORE;

namespace CBH.CORE
{
    /// <summary>
    /// Used to update the "game speed" setting during gameplay
    /// </summary>
    public class ChangeGameSpeed : MonoBehaviour
    {
        [SerializeField] private Slider slider = null;


        #region //Monobehaviour
        private void Awake()
        {
            GameSettings.SetGameSpeed(slider.value);
        }

        private void OnEnable()
        {
            EndGame.OnEndGame += FreezeAll;
        }

        private void OnDisable()
        {
            EndGame.OnEndGame -= FreezeAll;
        }
        #endregion

        #region //Changing scale
        public void IncrementScale(float _diff)
        {
            slider.value += _diff;
            ChangeScale(slider.value);
        }

        public void ChangeScale(float _newScale)
        {
            GameSettings.SetGameSpeed(_newScale);
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void FreezeAll()
        {
            GameSettings.SetGameSpeed(0);
        }
        #endregion
    }
}