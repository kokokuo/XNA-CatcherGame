using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatcherGame.GameStates;
using CatcherGame.GameObjects;

namespace CatcherGame.GameStates.Dialog
{
    public class TopScoreDialog : GameDialog
    {
        
        public TopScoreDialog(GameState pCurrentState)
            : base(pCurrentState) 
        { 
        }

        public override void BeginInit()
        {
            backgroundPos = new Vector2(0,0);
            closeButton = new Button(base.currentState, base.countId++, 0, 0);
            AddGameObject(closeButton);

            base.isInit = true;
        }
        public override void LoadResource()
        {
            background = currentState.GetTexture2DList(TextureManager.TexturesKeyEnum.TOP_SCORE_DIALOG_BACK)[0];
            base.LoadResource(); //載入CloseButton 圖片資源
        }
        public override void Update()
        {
           
            if (!base.currentState.IsEmptyQueue())
            {
                TouchLocation touchLocation = base.currentState.GetTouchLocation();
                if (touchLocation.State == TouchLocationState.Released)
                {
                    //若有觸及到關閉按鈕
                    if (closeButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y))
                    {
                        base.CloseDialog(); //透過父類別來關閉
                    }
                }
            }
            base.Update(); //更新遊戲元件
        }
        public override void Draw()
        {
            gameSateSpriteBatch.Draw(background, backgroundPos, Color.White);
            base.Draw(); //繪製遊戲元件
        }
    }
}
