using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatcherGame.GameStates.Screen;
using CatcherGame.GameObjects;

namespace CatcherGame.GameStates.Dialog
{
    public class TopScoreDialog : GameDialog
    {
        
        public TopScoreDialog(GameScreen pCurrentScreen)
            : base(pCurrentScreen) 
        { 
        }

        public override void BeginInit()
        {
            backgroundPos = new Vector2(0,0);
            closeButton = new Button(base.currentScreen, base.countId++, 0, 0);
            AddGameObject(closeButton);

            base.isInit = true;
        }
        public override void LoadResource()
        {
            background = currentScreen.GetTexture2DList(TextureManager.TexturesKeyEnum.TOP_SCORE_DIALOG_BACK)[0];
            base.LoadResource();
        }
        public override void Update()
        {
            if (!currentScreen.GetIsTouchDataQueueEmpty())
            {
                TouchLocation touchLocation;
                touchLocation = currentScreen.GetTouchData();

                if (touchLocation.State == TouchLocationState.Released)
                {
                    if (closeButton.IsClick(touchLocation.Position.X, touchLocation.Position.Y))
                    {
                        base.CloseDialog();
                    }
                }
            }
            base.Update();
        }
        public override void Draw()
        {
            gameSateSpriteBatch.Draw(background, backgroundPos, Color.White);
            base.Draw();
        }
    }
}
