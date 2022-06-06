using System.Collections.Generic;
using UnityEngine;

namespace JZ.CORE.POOL
{
    /// <summary>
    /// Monobehaviour containing its own audiosource object pool
    /// </summary>
    public class SFXPoolContainer : ObjectPoolContainer<AudioSource>
    {
        public override AudioSource GetOjbectFromPool()
        {
            var source = base.GetOjbectFromPool();
            source.volume *= GameSettings.GetAdjustedVolume(VolumeType.sfx);
            return source;
        }

        public override IEnumerable<AudioSource> GetObjectsFromPool(int _count)
        {
            var sources = base.GetObjectsFromPool(_count);
            foreach(var source in sources)
                source.volume *= GameSettings.GetAdjustedVolume(VolumeType.sfx);
            return sources;
        }
    }
}
