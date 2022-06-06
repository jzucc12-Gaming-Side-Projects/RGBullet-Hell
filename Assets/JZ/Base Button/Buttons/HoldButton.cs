using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JZ.BUTTONS
{
    /// <summary>
    /// Extension of button that requires the button to be held
    /// </summary>
    public class HoldButton : Button
    {
        #region //Variables
        [SerializeField] private float timeToHold = 2f;
        private float currHoldtimer = 0;
        private IEnumerator holdingRoutine = null;
        #endregion

        #region //Pointer events
        public override void OnPointerDown(PointerEventData eventData)
        {
            eventData.selectedObject = null;
            holdingRoutine = HoldCount(eventData);
            StartCoroutine(holdingRoutine);
        }

        public override void OnPointerClick(PointerEventData eventData) { } 

        public override void OnPointerUp(PointerEventData eventData)
        {
            currHoldtimer = 0;
            StopCoroutine(holdingRoutine);
        }
        #endregion

        #region //Hold methods
        private IEnumerator HoldCount(PointerEventData data)
        {
            while(currHoldtimer < timeToHold)
            {
                currHoldtimer += Time.deltaTime;
                yield return null;
            }
            onClick.Invoke();
        }

        public float GetProgressPercentage() => currHoldtimer / timeToHold;
        #endregion
    
        public float GetHoldTimer() { return timeToHold; }
    }
}