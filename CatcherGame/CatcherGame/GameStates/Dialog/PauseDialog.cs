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
    public class PauseDialog : GameDialog
    {
        Button continueGameButton;
        Button exitGameButton;
        
        public PauseDialog(GameState pCurrentState)
            : base(pCurrentState) 
        { 
        }

        public override void BeginInit()
        {

            backgroundPos = new Vector2(0, 0);
            
            //在Pause Dialog中沒有使用到close按鈕 所以註解掉!
            //closeButton = new Button(base.currentState, base.countId++, 0, 0);
            exitGameButton = new Button(base.currentState, base.countId++, 0, 0);
            continueGameButton = new Button(base.currentState, base.countId++, 0, 0);

            //在Pause Dialog中沒有使用到close按鈕 所以註解掉!
            //AddGameObject(closeButton);

            AddGameObject(continueGameButton);
            AddGameObject(exitGameButton);
            base.isInit = true;
        }
        public override void LoadResource()
        {
            background = currentState.GetTexture2DList(TextureManager.TexturesKeyEnum.PAUSE_DIALOG_BACK)[0];
            continueGameButton.LoadResource(TextureManager.TexturesKeyEnum.PAUSE_CONTINUE);
            exitGameButton.LoadResource(TextureManager.TexturesKeyEnum.PAUSE_EXIT);
            //在Pause Dialog中沒有使用到close按鈕 所以註解掉!
            //base.LoadResource(); //載入CloseButton 圖片資源
            base.isLoadContent = true;
        }
        public override void Update()
        {

            if (!base.currentState.IsEmptyQueue())
            {
                TouchLocation touchLocation = base.currentState.GetTouchLocation();
                if (touchLocation.State == TouchLocationState.Released)
                {
                    //若有觸及到繼續遊戲按鈕
                    if (continueGameButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y))
                    {
                        base.CloseDialog(); //透過父類別來關閉視窗
                    }
                    else if(exitGameButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y))
                    {
                        base.CloseDialog(); //透過父類別來關閉視窗
                        base.currentState.SetNextGameSateByMain(GameStateEnum.STATE_MENU); //切換回選單
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
