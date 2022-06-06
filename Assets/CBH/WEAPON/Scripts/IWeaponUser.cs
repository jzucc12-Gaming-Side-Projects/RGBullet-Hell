using System.Collections;
using CBH.PROJECTILE;
using UnityEngine;

namespace CBH.WEAPON
{
    /// <summary>
    /// Implementers provide the weapon necessary information about the user
    /// without the weapon caring about who is using it
    /// </summary>
    public interface IWeaponUser
    {
        BaseProjectile GetProjectile();
        GameObject GetUser();
        Coroutine Routine(IEnumerator _e);
    }
}