using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatcherGame.GameObjects;
using CatcherGame.TextureManager;
using CatcherGame.GameStates.Dialog;


namespace CatcherGame.GameStates
{
    public class PlayGameState  :GameState
    {
        Button pauseButton;
        Button leftMoveButton;
        Button rightMoveButton;
        Texture2D playBackground;
        Vector2 backgroundPos;
        int objIdCount;
        FiremanPlayer player;
        TouchLocation preTouchLocation;
        float rightGameScreenBorder;
        float leftGameScreenBorder;
        public PlayGameState(MainGame gMainGame) 
            :base(gMainGame)
        {
            dialogTable = new Dictionary<DialogStateEnum, GameDialog>();
        }

        public override void BeginInit()
        {
            base.x = 0; base.y = 0;
            backgroundPos = new Vector2(base.x, base.y);

            objIdCount = 0;

            pauseButton = new Button(this, objIdCount++, 0, 0);
            leftMoveButton = new Button(this, objIdCount++, 0, 355);
            rightMoveButton = new Button(this, objIdCount++, 700, 355);
            player = new FiremanPlayer(this, objIdCount++, 300, 355);
            
            //加入遊戲元件
            AddGameObject(player); 
            AddGameObject(leftMoveButton);
            AddGameObject(rightMoveButton);
            AddGameObject(pauseButton);
           

            

            base.isInit = true;
        }

        public override void LoadResource()
        {
            //載入圖片
            playBackground = base.GetTexture2DList(TexturesKeyEnum.PLAY_BACKGROUND)[0];
            pauseButton.LoadResource(TexturesKeyEnum.PLAY_PAUSE_BUTTON);
            leftMoveButton.LoadResource(TexturesKeyEnum.PLAY_LEFT_MOVE_BUTTON);
            rightMoveButton.LoadResource(TexturesKeyEnum.PLAY_RIGHT_MOVE_BUTTON);
            player.LoadResource(TexturesKeyEnum.PLAY_FIREMAN);


        }



        public override void Update()
        {
            //設定消防員的移動邊界
            rightGameScreenBorder = rightMoveButton.X;
            leftGameScreenBorder = leftMoveButton.Width;

            if (!base.hasDialogShow)
            {
                //檢查是否觸控的資料Queue為空
                if (!base.IsEmptyQueue())
                {
                    //Deueue區出處控的資料
                    TouchLocation touchLocation = base.GetTouchLocation();
                    if (touchLocation.State != TouchLocationState.Released)
                    {
                        if (leftMoveButton.IsBoundingBoxClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y))
                        {
                            //Debug.WriteLine("Click Left Button");
                            player.MoveLeft(leftGameScreenBorder);
                        }
                        else if (rightMoveButton.IsBoundingBoxClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y))
                        {
                            //Debug.WriteLine("Click Right Button");
                            player.MoveRight(rightGameScreenBorder);
                        }
                        else
                        {
                            player.SetStand();
                        }
                    }
                }
            }
            base.Update();
        }
        public override void Draw()
        {
            // 繪製主頁背景
            gameSateSpriteBatch.Draw(playBackground, backgroundPos, Color.White);
            base.Draw();
        }
    }
}
