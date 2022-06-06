using UnityEngine;
using CBH.PLAYER.INPUT;

namespace CBH.PLAYER.ABILITY
{
    /// <summary>
    /// Holds common components for all player abilites
    /// </summary>
    public abstract class PlayerAbilityDataContainer : MonoBehaviour
    {
        protected Rigidbody2D rb = null;
        protected PlayerInputSystem inputSystem = null;
        protected Camera cam = null;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputSystem = GetComponent<PlayerInputSystem>();
            cam = FindObjectOfType<Camera>();
        }
    }
}
