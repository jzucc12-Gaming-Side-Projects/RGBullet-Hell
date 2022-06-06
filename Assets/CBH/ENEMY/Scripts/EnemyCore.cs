using System;
using System.Collections.Generic;
using System.Linq;
using CBH.SHAPE;
using UnityEditor;
using UnityEngine;

namespace CBH.ENEMY
{
    public class EnemyCore : MonoBehaviour
    {
        #region //Variables
        [Tooltip("Is comprised of multiple enemy types")] [SerializeField] private bool isCombo = false;
        [HideInInspector, SerializeField] private EnemyCore[] comboParts = new EnemyCore[0];
        private EnemyType[] types => GetComponents<EnemyType>();
        private List<EnemyCore> keptComboParts = new List<EnemyCore>();
        #endregion

        #region //Events
        public event Action<EnemyCore> OnCoreDestroyed;
        #endregion


        #region //Monobehaviour
        private void OnEnable()
        {
            foreach(var type in types)
            {
                type.OnDeath += OnTypeDeath;
            }
        }

        private void OnDisable()
        {
            foreach(var type in types)
            {
                type.OnDeath -= OnTypeDeath;
            }
        }

        private void OnDestroy() 
        {
            OnCoreDestroyed?.Invoke(this);    
        }
        #endregion

        #region //Type death
        private void OnTypeDeath(EnemyType _type)
        {
            if (isCombo)
                SplitSelf(_type.GetShapeType());
            else
                DestroySelf();
        }

        public void DestroySelf()
        {   
            Destroy(gameObject);
        }
        #endregion

        #region //Splitting
        //Splits combo enemy into its remaining combo parts
        private void SplitSelf(ShapeTypeSO _destroyedShape)
        {
            //Ensures enemy parts don't spawn on one another
            Vector2 spawnPoint = transform.position;
            Vector2 spawnOffset = new Vector2(2,0);
            
            //Determine which parts to keep
            foreach(var enemy in comboParts)
            {
                //Keep all parts if in inverted mode
                //Ohterwise skip the parts matching the one the player destroyed
                if(!GameSettings.inversion && HasShape(enemy, _destroyedShape)) continue;

                enemy.gameObject.SetActive(true);
                keptComboParts.Add(enemy);
                enemy.transform.parent = transform.parent;
                enemy.transform.position = spawnPoint;
                spawnPoint += spawnOffset; 
            }
            DestroySelf();
        }
        private bool HasShape(EnemyCore _comparison, ShapeTypeSO _shape)
        {
            var shapes = _comparison.GetShapes();
            return shapes.Contains(_shape);
        }
        
        private IEnumerable<ShapeTypeSO> GetShapes()
        {
            foreach(var type in types)
                yield return type.GetShapeType();
        }
        #endregion
    
        #region //Getters
        public bool IsCombo()
        {
            return isCombo;
        }

        public IEnumerable<EnemyCore> GetKeptComboParts()
        {
            return keptComboParts;
        }
        #endregion
    }

    #region //Custom Editor
#if UNITY_EDITOR
        [CustomEditor(typeof(EnemyCore), true)]
        public class EnemyCoreEditor : Editor
        {
            private SerializedProperty isComboProperty = null;
            private SerializedProperty comboPartsProperty = null;

            protected virtual void OnEnable()
            {
                isComboProperty = serializedObject.FindProperty("isCombo");
                comboPartsProperty = serializedObject.FindProperty("comboParts");
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                serializedObject.Update();
                if(isComboProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(comboPartsProperty);
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    #endregion
}