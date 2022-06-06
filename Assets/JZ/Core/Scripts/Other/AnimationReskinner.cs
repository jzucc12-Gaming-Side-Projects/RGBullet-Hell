using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.CORE
{
    /// <summary>
    /// Allows for an existing animation to be used with different sprites
    /// New spritesheet must have the same number of frames as the old one
    /// New spritesheet must follow the naming scheme of XXXX_#
    /// </summary>
    public class AnimationReskinner : MonoBehaviour
    {
        #region //Variables
        [SerializeField] private string resourcePath = "";
        [SerializeField] private string fileName = "";
        [SerializeField] private bool isImage = false;
        [SerializeField] private bool reset = false;
        private Sprite[] subSprites = new Sprite[0];
        private int places = 0;
        #endregion
        

        #region //Monobehaviour
        private void Awake()
        {
            subSprites = Resources.LoadAll<Sprite>(resourcePath + fileName);
            places = subSprites.Length.ToString().Count();
        }

        private void LateUpdate()
        {
            if(reset)
            {
                reset = false;
                Awake();
            }
            
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(resourcePath)) return;
            

            if (isImage)
                Replace(subSprites, GetComponentInChildren<Image>());
            else
                Replace(subSprites, GetComponentInChildren<SpriteRenderer>());
        }
        #endregion

        #region //Replacement
        private void Replace(Sprite[] _subSprites, Image _renderer)
        {
            int id = GetID(_renderer.sprite.name);
            _renderer.sprite = _subSprites[id];
        }

        private void Replace(Sprite[] _subSprites, SpriteRenderer _renderer)
        {
            int id = GetID(_renderer.sprite.name);
            _renderer.sprite = _subSprites[id];
        }

        private int GetID(string _spriteName)
        {
            string chars = _spriteName.Substring(_spriteName.Length - places);
            string[] sections = chars.Split('_');
            return int.Parse(sections.Last());
        }
        #endregion
    }
}