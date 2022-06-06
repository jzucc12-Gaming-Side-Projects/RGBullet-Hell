using UnityEngine;

namespace JZ.CORE
{
    /// <summary>
    /// General player wins end game
    /// </summary>
    public class Victory : EndGame
    {
        [SerializeField] private GameObject normalPopUp = null;
        [SerializeField] private GameObject invertedPopUp = null;


        public void InitiateVictory()
        {
            InitiateEndGame();

            if(GameSettings.inversion)
                SetPlayerPrefs(PlayerPrefKeys.victoryInverted, invertedPopUp);
            else
                SetPlayerPrefs(PlayerPrefKeys.victory, normalPopUp);
        }

        private void SetPlayerPrefs(string _key, GameObject _popUp = null)
        {
            if(PlayerPrefs.GetInt(_key, 0) == 1) return;

            if(_popUp != null) _popUp.SetActive(true);
            PlayerPrefs.SetInt(_key, 1);
        }
    }
}