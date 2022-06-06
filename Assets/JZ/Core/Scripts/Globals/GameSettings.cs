using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Hub for all game settings
/// </summary>
public static class GameSettings
{
    public static bool InDevMode()
    {
        int devModeValue = PlayerPrefs.GetInt(PlayerPrefKeys.devModeKey, 0);
        return devModeValue == 1;
    }

    #region //Audio
    public const string masterVolKey = "Master Volume";
    public const string musicVolKey = "Music Volume";
    public const string sfxVolKey = "SFX Volume";

    //Master, Music, SFX
    private static float[] volumes = { 0.5f, 0.5f, 0.5f };
    private static float[] defaultVolumes = { 0.5f, 0.5f, 0.5f };
    public static float GetVolume(VolumeType _type) { return volumes[(int)_type]; }
    public static float GetDeffaultVolume(VolumeType _type) { return defaultVolumes[(int)_type]; }
    public static void SetVolume(VolumeType _type, float _newVol) { volumes[(int)_type] = _newVol; }
    public static float GetAdjustedVolume(VolumeType _type) 
    { 
        float master = volumes[(int)VolumeType.master];
        float specific = volumes[(int)_type];
        return 2 * master * specific;
    }
    #endregion
    
    #region //Color
    public static event Action OnColorModeChanged;
    private static ColorMode colorMode = ColorMode.standard;
    public static ColorMode GetColorMode() { return colorMode; }
    public static void SetColorMode(ColorMode _mode) 
    { 
        colorMode = _mode; 
        OnColorModeChanged?.Invoke();
    }
    #endregion

    #region //Game specific
    private static float gameSpeedScale = 1f;
    public static float GetGameSpeed() { return gameSpeedScale; }
    public static void SetGameSpeed(float _scale) { gameSpeedScale = _scale; }
    public static IEnumerator GameSpeedScaledTimer(float _duartion)
    {
        float currTime = 0;
        while(currTime < _duartion)
        {
            yield return null;
            currTime += Time.deltaTime * gameSpeedScale;
        }
    }
    public static bool inversion = false;
    public static bool timed = false;
    #endregion
}

public enum VolumeType
{
    master = 0,
    music = 1,
    sfx = 2
}