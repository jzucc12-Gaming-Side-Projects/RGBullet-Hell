using UnityEngine;

namespace CBH.SHAPE
{
    /// <summary>
    /// Shape types are the different "states" the player, enemies, projectiles, etc can be in
    /// They link a color to a specific shape
    /// </summary>
    [CreateAssetMenu(fileName = "ShapeTypeSO", menuName = "CBH/ShapeTypeSO", order = 0)]
    public class ShapeTypeSO : ScriptableObject 
    {
        [SerializeField] private Sprite baseSprite = null;
        [SerializeField] private Sprite projectileSprite = null;
        [SerializeField] private ColorModeUser colors = new ColorModeUser();


        public Sprite GetBaseSprite()
        {
            return baseSprite;
        }

        public Sprite GetProjectileSprite()
        {
            return projectileSprite;
        }

        public ColorModeUser GetColorModeUser()
        {
            return colors;
        }

        public Color GetColor()
        {
            return colors.GetActiveColor();
        }

        public bool IsSameAs(ShapeTypeSO _comparison)
        {
            return this == _comparison;
        }
    }
}