using UnityEngine;
using CBH.SHAPE;

namespace CBH.PROJECTILE
{
    public class PlayerProjectile : BaseProjectile
    {
        [SerializeField] private TrailRenderer trailerRenderer = null;


        #region //Monobehaviour
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        #endregion

        #region //Collision
        protected override void Collision(IShapeCollision[] _targets, int _damage)
        {
            if(!GameSettings.inversion)
            {
                NormalCollision(_targets,  _damage);
            }
            else
            {
                InvertedCollision(_targets,  _damage);
            }
        }

        private void NormalCollision(IShapeCollision[] _targets, int _damage)
        {
            //Check projectile shape type against each enemy type
            for (int ii = 0; ii < _targets.Length; ii++)
            {
                var target = _targets[ii];
                if (myShape.IsSameAs(target.GetShapeType()))
                {
                    target.CollisionWithSameType(_damage);
                    break;
                }
                else if (ii == _targets.Length - 1)
                {
                    target.CollisionWithDifferentType(_damage);
                }
            }
        }

        private void InvertedCollision(IShapeCollision[] _targets, int _damage)
        {
            //Check projectile shape type against each enemy type
            bool onlyDifferent = true;
            for (int ii = 0; ii < _targets.Length; ii++)
            {
                var target = _targets[ii];
                if (myShape.IsSameAs(target.GetShapeType()))
                {
                    onlyDifferent = false;
                    break;
                }
            }

            //Only register as different if projectile does not match any enemy type
            if(onlyDifferent)
            {
                foreach(var target in _targets)
                    target.CollisionWithDifferentType(_damage);
            }
            else
            {
                _targets[0].CollisionWithSameType(_damage);
            }
        }
        #endregion

        #region //Start up
        //Public
        public override void OrientProjectile(Transform _spawnPoint)
        {
            base.OrientProjectile(_spawnPoint);
            trailerRenderer.Clear();
        }

        //Protected
        protected override void SetAppearance()
        {
            base.SetAppearance();
            trailerRenderer.endColor = myShape.GetColor();
        }
        #endregion
    }
}