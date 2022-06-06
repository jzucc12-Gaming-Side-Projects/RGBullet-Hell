using UnityEngine;

namespace JZ.INPUT
{
    /// <summary>
    /// Menu navigation for grid based menus
    /// </summary>
    public class MenuNavigator2D : MenuNavigator
    {
        #region //Menu variables
        private Vector2Int layout = new Vector2Int(0,0);
        private bool loopX = false;
        private bool loopY = false;
        private int columns => layout.x;
        private int rows => layout.y;
        #endregion

        #region //State variables
        private int xPos = 0;
        private int yPos = 0;
        #endregion


        #region //Constructors
        public MenuNavigator2D(int _itemCount, Vector2Int _layout, bool _loopX, bool _loopY) : base(_itemCount) 
        { 
            layout = _layout;
            loopX = _loopX;
            loopY = _loopY;
        }
        #endregion
        
        #region //Getters
        public override int GetPosition()
        {
            return xPos + columns * yPos;
        }

        private int GetTestPosition(int _testXValue, int _testYValue)
        {
            return _testXValue + columns * _testYValue;
        }
        #endregion

        #region //Setters
        public override void SetPosition(int _newPos)
        {
            xPos = _newPos % columns;
            yPos = Mathf.FloorToInt(_newPos/columns);
        }

        public override void ResetArrow()
        {
            xPos = 0;
            yPos = 0;
        }
        #endregion

        #region //Navigation
        //Public
        public override void Navigation()
        {
            xPos = XNavigate(xPos);
            yPos = YNavigate(yPos);
        }

        //Private
        private int XNavigate(int _startColumn)
        {
            if (!im.XNavPressed()) return _startColumn;
            int xMove = im.GetXNav();
            int newColumn = _startColumn + xMove;
            im.ExpendXDir();

            if (newColumn < 0)
                newColumn = (loopX ? columns - 1 : 0);
            else if (newColumn >= columns)
                newColumn = (loopX ? 0 : columns - 1);

            if(GetTestPosition(newColumn, yPos) >= itemCount)
            {
                if(!loopX) newColumn = _startColumn;
                else if(xMove > 0) newColumn = 0;
                else newColumn = (itemCount -1) % columns;
            }

            return newColumn;
        }

        private int YNavigate(int _startRow)
        {
            if (!im.YNavPressed()) return _startRow;
            int yMove = -im.GetYNav();
            int newRow = _startRow + yMove;
            im.ExpendYDir();

            if(newRow < 0)
                newRow = (loopY ? rows - 1 : 0);
            else if (newRow >= rows)
                newRow = (loopY ? 0 :rows - 1);

            if(GetTestPosition(xPos, newRow) >= itemCount)
            {
                if(!loopY) newRow = _startRow;
                else if(yMove > 0) newRow = 0;
                else newRow = rows - 2;
            }

            return newRow;
        }
        #endregion
    }
}