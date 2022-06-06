using JZ.AUDIO;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.SCENE
{
    /// <summary>
    /// Single time appearance start menu
    /// Closes on any button press
    /// </summary>
    public class StartMenu : MonoBehaviour
    {
        private static bool started = false;
        [SerializeField] private Button progressButton = null;


        private void Start() 
        {
            if(!started) 
            {
                FindObjectOfType<Button>().onClick.AddListener(StartSFX);
                return;
            }
            FindObjectOfType<Button>().onClick?.Invoke();
        }

        private void Update()
        {
            if(started) return;
            
            if(JZ.INPUT.Utils.AnyKeyOrButton())
            {
                started = true;
                progressButton.onClick?.Invoke();
            }
        }

        private void StartSFX()
        {
            GetComponent<SoundPlayer>().Play("Start");
        }
    }
}
