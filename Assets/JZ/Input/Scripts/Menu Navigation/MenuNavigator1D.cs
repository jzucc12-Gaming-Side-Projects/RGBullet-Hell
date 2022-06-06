namespace JZ.INPUT
{
    /// <summary>
    /// Menu navigation for horizontal or vertical menus
    /// </summary>
    public class MenuNavigator1D : MenuNavigator
    {
        #region //Menu variables
        private bool loop = false;
        private bool verticalLayout = true;
        #endregion

        #region //State variables
        private int currentPosition = 0;
        #endregion


        #region //Constructor
        public MenuNavigator1D(int _itemCount, bool _loop, bool _verticalLayout) : base(_itemCount) 
        { 
            loop = _loop;
            verticalLayout = _verticalLayout;
        }
        #endregion

        #region //Getters
        public int GetOffAxisValue()
        {
            return (verticalLayout ? im.GetXNav() : im.GetYNav());
        }

        public override int GetPosition()
        {
            return currentPosition;
        }
        #endregion

        #region//Setters      
        public override void SetPosition(int _newPos)
        {
            currentPosition = _newPos;
        }

        public override void ResetArrow()
        {
            currentPosition = 0;
        }
        #endregion

        #region //Navigation
        public override void Navigation()
        {
            if(verticalLayout && !im.YNavPressed()) return;
            if(!verticalLayout && !im.XNavPressed()) return;

            int startArrow = GetPosition();
            int move = (verticalLayout ? -im.GetYNav() : im.GetXNav());
            int newPos = startArrow + move;
            im.ExpendAllDir();

            if(newPos < 0)
                currentPosition = (loop ? itemCount - 1 : 0);
            else if(newPos >= itemCount)
                currentPosition = (loop ? 0 : itemCount - 1);
            else
                currentPosition = newPos;
        }
        #endregion
    }
}