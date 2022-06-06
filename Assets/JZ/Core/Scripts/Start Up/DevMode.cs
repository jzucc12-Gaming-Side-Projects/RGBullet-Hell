using UnityEngine;

/// <summary>
/// Sets whether or not the game is in dev mode
/// </summary>
public class DevMode : MonoBehaviour
{
    [SerializeField] private bool devMode = false;

    private void Awake()
    {
        int devModeValue = devMode ? 1 : 0;
        PlayerPrefs.SetInt(PlayerPrefKeys.devModeKey, devModeValue);
    }
}
