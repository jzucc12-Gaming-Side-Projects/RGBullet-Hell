using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace JZ.INPUT
{
    /// <summary>
    /// Extra functions relating to player input
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Converts a string into its corresponding key codes
        /// </summary>
        public static KeyCode[] KeyCodesFromString(string _string)
        {
            KeyCode[] keys = new KeyCode[_string.Length];
            for(int ii = 0; ii < keys.Length; ii++)
            {
                keys[ii] = (KeyCode)Enum.Parse(typeof(KeyCode), _string[ii].ToString());
            }

            return keys;
        }

        /// <summary>Checks if a key combo is being pressed
        /// <para>Returns true the frame the last key in the combo is pressed</para>
        /// </summary>
        public static bool CheckKeyCombo(string _keyCombo)
        {
            KeyCode[] combo = KeyCodesFromString(_keyCombo);
            for(int ii = 0; ii < combo.Length - 1; ii++)
                if(!Input.GetKey(combo[ii])) return false;

            return Input.GetKeyDown(combo[combo.Length - 1]);
        }

        /// <summary>Checks if a key combo is being pressed
        /// <para>Returns true the frame the last key in the combo is pressed</para>
        /// </summary>
        public static bool CheckKeyCombo(KeyCode[] _keyCombo)
        {
            for(int ii = 0; ii < _keyCombo.Length - 1; ii++)
                if(!Input.GetKey(_keyCombo[ii])) return false;

            return Input.GetKeyDown(_keyCombo[_keyCombo.Length - 1]);
        }

        /// <summary>Returns true if a controller button is pressed
        /// </summary>
        public static bool AnyControllerButton()
        {
            if(Gamepad.current == null) return false;

            foreach(InputControl control in Gamepad.current.allControls)
            {
                if(!(control is ButtonControl)) continue;
                if(!control.IsPressed() || control.synthetic) continue;
                return true;
            }
            return false;
        }

        /// <summary>Returns true if a controller or computer button is pressed
        /// </summary>
        public static bool AnyKeyOrButton()
        {
            return Input.anyKey || AnyControllerButton();
        }
    }
}

