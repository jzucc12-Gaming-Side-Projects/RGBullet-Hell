using System.Collections.Generic;
using System.Linq;
using CBH.SHAPE;
using CBH.WEAPON.PLAYER;
using UnityEngine;

namespace CBH.PLAYER.UI
{
    /// <summary>
    /// UI for the palyer's entire health pool
    /// </summary>
    public class PlayerHealthUI : MonoBehaviour
    {
        #region //Variables
        private PlayerBody playerBody = null;
        private WeaponCache weaponCache = null;
        private PlayerHealthEntryUI[] entries = new PlayerHealthEntryUI[0];
        private Dictionary<ShapeTypeSO, PlayerHealthEntryUI> entryDictionary = new Dictionary<ShapeTypeSO, PlayerHealthEntryUI>();
        #endregion

        #region //Monobehaviour
        private void Awake()
        {
            entries = GetComponentsInChildren<PlayerHealthEntryUI>();
            weaponCache = FindObjectOfType<WeaponCache>();
            playerBody = FindObjectOfType<PlayerBody>();
            foreach(var entry in entries)
                entry.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            playerBody.OnDamageTaken += ChangeHealth;
        }

        private void OnDisable()
        {
            playerBody.OnDamageTaken -= ChangeHealth;
        }

        private void Start()
        {
            ShapeTypeSO[] shapes = weaponCache.GetShapes().ToArray();
            for(int ii = 0; ii < shapes.Length; ii++)
            {
                entries[ii].gameObject.SetActive(true);
                ColorModeUser colors = shapes[ii].GetColorModeUser();
                entries[ii].SetupEntry(playerBody.GetMaxHealthPerShape(), colors);
                entryDictionary.Add(shapes[ii], entries[ii]);
            }
        }
        #endregion

        private void ChangeHealth(ShapeTypeSO _shape, int _newHealth)
        {
            entryDictionary[_shape].ChangeHealth(_newHealth);
        }
    }
}