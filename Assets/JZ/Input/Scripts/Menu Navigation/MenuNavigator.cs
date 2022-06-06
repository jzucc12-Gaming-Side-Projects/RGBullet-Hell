namespace JZ.INPUT
{
    /// <summary>
    /// Parent class for menu navigation handling
    /// </summary>
    public abstract class MenuNavigator
    {
        #region //Cached Components
        protected MenuingInputSystem im = null;
        #endregion

        #region //Size Variables
        protected int itemCount = 0;
        #endregion


        #region //Constructor
        public MenuNavigator(int _itemCount)
        {
            itemCount = _itemCount;
            im = new MenuingInputSystem(new MenuingInputs());
        }
        #endregion

        #region //Activation
        public void Activate()
        {
            im.Activate();
        }

        public void Deactivate()
        {
            im.Deactivate();
        }
        #endregion

        #region //Getters
        public abstract int GetPosition();

        public bool IsItemSelected()
        {
            return im.MenuSelectPressed();
        }
        #endregion

        #region//Navigation
        //Public
        public abstract void SetPosition(int _newPos);

        public abstract void Navigation();
        #endregion

        #region //Resetting
        public void ResetNavigator()
        {
            ResetArrow();
            ResetItem();
        }

        public void ResetItem()
        {
            im.ExpendMenuSelect();
        }

        public abstract void ResetArrow();
        #endregion
    }
}