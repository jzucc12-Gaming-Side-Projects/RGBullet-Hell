using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT.UI
{
    /// <summary>
    /// Control display that uses input actions and recognizes rebinding
    /// </summary>
    public class ControlDisplayV2 : RebindableControlDisplay
    {
        #region //Binding Display
        [SerializeField] protected int controlIndex = 0;

        [Header("Binding PC Display")]
        [SerializeField] private bool useBindingTextPC = false;
        [SerializeField, ShowIf("useBindingTextPC")] private bool useCompositePC = false;
        [SerializeField, ShowIf("useBindingTextPC")] private InputBinding.DisplayStringOptions optionsPC;

        [Header("Binding Gamepad Display")]
        [SerializeField] private bool useBindingTextGamepad = false;
        [SerializeField, ShowIf("useBindingTextGamepad")] private bool useCompositeGamepad = false;
        [SerializeField, ShowIf("useBindingTextGamepad")] private InputBinding.DisplayStringOptions optionsGamepad;
        #endregion


        #region //Update display
        protected override void UpdateDisplay()
        {
            if(UseTextCheck() || !ShowImage())
                ShowText();
        }

        private bool ShowImage()
        {
            Sprite sprite = Resources.Load<Sprite>(GetImagePath());
            if(sprite != null)
            {
                myImage.enabled = true;
                myText.enabled = false;
                myImage.sprite = sprite;
                return true;
            }

            return false;
        }

        private void ShowText()
        {
            myImage.enabled = false;
            myText.enabled = true;
            myText.text = action.GetBindingDisplayString(GetBindingIndex(), GetOptions());
        }
        #endregion

        #region //Get display information
        //Generates image path from resources folder
        private string GetImagePath()
        {
            //Device check
            string platform;
            if (isGamepad)
                platform = $"{deviceType}";
            else
                platform = "PC";

            //Control check
            string control = GetBinding().ToDisplayString();

            //Removes slashes due to use in file paths
            if(control.Contains("/"))
                control = control.Replace('/', '_');
            
            //Generate path
            string path = $"Input Images/{platform}/{control.ToUpper()}";
            return path;
        }

        //Checks if a given binding matches the current control scheme
        private bool CheckBindingScheme(int _index)
        {
            if(string.IsNullOrEmpty(action.bindings[_index].groups)) return false;
            string[] schemes = action.bindings[_index].groups.Split(';');

            foreach(var scheme in schemes)
            {
                if(isGamepad ^ scheme == "Gamepad") continue;
                return true;
            }
            return false;
        }

        private bool UseTextCheck()
        {
            if(isGamepad && useBindingTextGamepad) return true;
            if(!isGamepad && useBindingTextPC) return true;
            return false;
        }

        private bool UseCompositeCheck()
        {
            if(isGamepad && useCompositeGamepad) return true;
            if(!isGamepad && useCompositePC) return true;
            return false;
        }

        private InputBinding.DisplayStringOptions GetOptions()
        {
            if(isGamepad) return optionsGamepad;
            else return optionsPC;
        }
        #endregion
    
        #region //Control Info
        public override InputActionAsset GetAsset()
        {
            if(Application.isPlaying)
                return InputManager.GetAsset(actionRef.asset.name);
            else
                return actionRef.asset;
        }

        public override InputAction GetAction()
        {
            return actionAsset.FindAction(actionRef.action.name);
        }

        public override int GetBindingIndex()
        {
            if(UseCompositeCheck())
            {
                for(int ii = 0; ii < action.bindings.Count; ii++)
                {
                    if(!action.bindings[ii].isComposite) continue;
                    if(!CheckBindingScheme(ii+1)) continue;
                    return ii;
                }
            }
            else
            {
                for(int ii = 0; ii < action.bindings.Count; ii++)
                {
                    if(!CheckBindingScheme(ii)) continue;
                    return ii + controlIndex;
                }
            }

            Debug.LogError("Binding not found");
            return -1;
        }

        public override InputBinding GetBinding()
        {
            return action.bindings[GetBindingIndex()];
        }
        #endregion
    }
}