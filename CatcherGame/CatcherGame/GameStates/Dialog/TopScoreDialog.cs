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
            base.isLoadContent = true;
        }
        public override void Update()
        {
            TouchCollection tc = base.currentState.GetCurrentFrameTouchCollection();
            bool isClickClose = false;
            if (tc.Count > 0){
                //所有當下的觸控點去判斷有無點到按鈕
                foreach (TouchLocation touchLocation in tc) {
                    if (!isClickClose)
                        isClickClose = closeButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                }
                
                //遊戲邏輯判斷
                if(isClickClose)
                    base.CloseDialog(); //透過父類別來關閉
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
