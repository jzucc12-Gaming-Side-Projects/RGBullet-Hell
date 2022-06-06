using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using JZ.UI.MENU.MEMBER;
using JZ.INPUT;
using JZ.AUDIO;
using JZ.SCENE;

namespace JZ.UI.MENU
{
    /// <summary>
    /// Menus that are traversed with an on-screen cursor
    /// Such as a main menu or pause menu
    /// Members within a list menu must contain the "Menu Member" class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListMenuUI<T> : MenuUI where T : MenuNavigator
    {
        #region//Cached Components
        [Header("Components")]
        [SerializeField] private GameObject menuMemberContainer = null;
        [SerializeField] private SoundPlayer sfxPlayer = null;
        protected List<MenuMember> members = new List<MenuMember>();
        protected MenuMember activeMember => members[currentLocation];
        protected T navigator = null;
        #endregion

        #region//Options
        [Header("Options")]
        [SerializeField] private bool recognizeMouse = true;
        [SerializeField] private bool singleActivation = false;
        [SerializeField] private bool resetOnOpen = false;
        #endregion

        #region //State Variables
        private int currentLocation = 0;
        private bool hasOpened = false;
        private int itemCount => members.Count;
        #endregion


        #region //Monobehaviour
        //Protected
        protected virtual void Awake()
        {
            SetUpMembers();
        }

        //Private
        private void Start()
        {
            navigator.SetPosition(currentLocation);
            SetPosition(currentLocation);
        }

        private void OnEnable()
        {
            HasOpenedCheck();
            MemberButtonActivate();
            navigator.Activate();
            SceneTransitioner.StartTransitionOut += TransitionShutDown;

            if(resetOnOpen)
            {
                navigator.SetPosition(0);
                SetPosition(0, false);
            }
        }

        private void OnDisable()
        {
            MemberButtonDeactivate();
            navigator.Deactivate();
            SceneTransitioner.StartTransitionOut -= TransitionShutDown;
        }

        protected virtual void Update()
        {
            navigator.Navigation();
            SetPosition(navigator.GetPosition());

            if(navigator.IsItemSelected())
            {
                navigator.ResetItem();
                Button activeButton = activeMember.GetComponent<Button>();
                if(activeButton != null)
                    activeButton.onClick?.Invoke();
            }
        }
        #endregion

        #region //Set up
        private void SetUpMembers()
        {
            var allMembers = menuMemberContainer.GetComponentsInChildren<MenuMember>();
            members = allMembers.Where(x => x.transform.parent == menuMemberContainer.transform).ToList();
            foreach(var member in members)
            {
                if(!member.GetComponent<Selectable>()) continue;
                member.GetComponent<Selectable>().interactable = recognizeMouse;
            }
        }

        private void HasOpenedCheck()
        {
            if(hasOpened)
            {
                navigator.ResetItem();
                if(singleActivation)
                    gameObject.SetActive(false);
            }

            hasOpened = true;
        }

        private void MemberButtonActivate()
        {
            foreach(var member in members)
            {
                if(recognizeMouse)
                    member.PointerEnter += MouseHover;

                if(member.GetComponent<Button>())
                    member.GetComponent<Button>().onClick.AddListener(PlaySelect);
            }
        }

        private void MemberButtonDeactivate()
        {
            foreach(var member in members)
            {
                if(recognizeMouse)
                    member.PointerEnter -= MouseHover;
                
                if(member.GetComponent<Button>())
                    member.GetComponent<Button>().onClick.RemoveListener(PlaySelect);
            }
        }
        #endregion

        #region //Position Updating
        private void SetPosition(int _newPosition, bool _playSFX = true)
        {
            if(_playSFX && _newPosition != currentLocation)
                PlayMove();

            if(activeMember != null) activeMember.Hover(false);
            currentLocation = _newPosition;
            if(activeMember != null) activeMember.Hover(true);
        }

        private void MouseHover(MenuMember _trigger)
        {
            for(int ii = 0; ii < members.Count; ii++)
            {
                if(_trigger != members[ii]) continue;
                navigator.SetPosition(ii);
                SetPosition(ii);
                break;
            }
        }
        #endregion
    
        #region //Audio
        private void PlayMove()
        {
            sfxPlayer.Play("Menu Move");
        }

        private void PlaySelect()
        {
            sfxPlayer.Play("Menu Select");
        }
        #endregion
    
        #region //Control Locking
        public override void LockControl(bool _lock)
        {
            if(_lock)
                navigator.Deactivate();
            else
                navigator.Activate();
        }

        private void TransitionShutDown()
        {
            LockControl(true);
        }
        #endregion
    }
}