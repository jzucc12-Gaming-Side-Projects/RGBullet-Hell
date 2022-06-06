using UnityEngine;

namespace JZ.CORE.POOL
{
    /// <summary>
    /// Audio source object pool for sound effects
    /// </summary>
    public class SFXPool : ObjectPool<AudioSource>
    {
        public SFXPool(int _poolSize, AudioSource _poolObject, Transform _poolContainer) : base(_poolSize, _poolObject, _poolContainer)
        {
        }


        protected override bool IsInUse(AudioSource _object)
        {
            return _object.isPlaying;
        }

        protected override void Deactivate(AudioSource _object)
        {
            _object.Stop();
        }
    }
}
