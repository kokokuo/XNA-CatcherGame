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
        const int FIREMAN_INIT_X = 300;
        const int FIREMAN_INIT_Y = 355;
        const int NET_INIT_X = FIREMAN_INIT_X + 73;
        const int NET_INIT_Y = FIREMAN_INIT_Y + 85;

        Button pauseButton;
        Button leftMoveButton;
        Button rightMoveButton;
        Texture2D playBackground;
        Vector2 backgroundPos;
        int objIdCount;
        FiremanPlayer player;
        Net savedNet; //網子

        //純粹圖片,無任何邏輯運算
        TextureLayer smokeTexture;
        TextureLayer lifeTexture;
        TextureLayer scoreTexture;

        //記錄點擊前一個座標
        TouchLocation preTouchLocation;
        
        //遊戲左右邊框
        float rightGameScreenBorder;
        float leftGameScreenBorder;

        public PlayGameState(MainGame gMainGame) 
            :base(gMainGame)
        {
            dialogTable = new Dictionary<DialogStateEnum, GameDialog>();
            dialogTable.Add(DialogStateEnum.STATE_PAUSE, new PauseDialog(this));
        }


        public override void BeginInit()
        {
            base.x = 0; base.y = 0;
            backgroundPos = new Vector2(base.x, base.y);

            objIdCount = 0;
            
            pauseButton = new Button(this, objIdCount++, 0, 0);
            leftMoveButton = new Button(this, objIdCount++, 0, 355);
            rightMoveButton = new Button(this, objIdCount++, 700, 355);

            savedNet = new Net(this, objIdCount++, NET_INIT_X, NET_INIT_Y);
            player = new FiremanPlayer(this, objIdCount++, FIREMAN_INIT_X, FIREMAN_INIT_Y, savedNet);
            
            smokeTexture = new TextureLayer(this,objIdCount++, 0, 0);
            lifeTexture = new TextureLayer(this,objIdCount++, 0, 0);
            scoreTexture = new TextureLayer(this, objIdCount++, 0, 0);

            //加入遊戲元件
            AddGameObject(player);
            AddGameObject(savedNet);
            AddGameObject(leftMoveButton);
            AddGameObject(rightMoveButton);
            AddGameObject(pauseButton);
            AddGameObject(smokeTexture);
            AddGameObject(lifeTexture);
            AddGameObject(scoreTexture);



            //對 對話框做初始化
            foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
            {
                if (!dialog.Value.GetGameDialogHasInit)
                {
                    dialog.Value.BeginInit();
                }
            }
            

            base.isInit = true;
        }

        //重製遊戲中的所有資料
        public void ResetData() { 
            //尚未時做
        }


        public override void LoadResource()
        {
            //載入圖片
            playBackground = base.GetTexture2DList(TexturesKeyEnum.PLAY_BACKGROUND)[0];
            pauseButton.LoadResource(TexturesKeyEnum.PLAY_PAUSE_BUTTON);
            leftMoveButton.LoadResource(TexturesKeyEnum.PLAY_LEFT_MOVE_BUTTON);
            rightMoveButton.LoadResource(TexturesKeyEnum.PLAY_RIGHT_MOVE_BUTTON);
            player.LoadResource(TexturesKeyEnum.PLAY_FIREMAN);
            savedNet.LoadResource(TexturesKeyEnum.PLAY_NET);

            smokeTexture.LoadResource(TexturesKeyEnum.PLAY_SMOKE);
            lifeTexture.LoadResource(TexturesKeyEnum.PLAY_LIFE);
            scoreTexture.LoadResource(TexturesKeyEnum.PLAY_SCORE);

            //載入對話框的圖片資源
            foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
            {
                if (!dialog.Value.GetGameDialogHasLoadContent)
                {
                    //把繪製的元件 gameSateSpriteBatch 傳入進去,讓對話框可以透過此 gameSateSpriteBatch 來繪製
                    dialog.Value.SetSpriteBatch(this.gameSateSpriteBatch);
                    dialog.Value.LoadResource();
                }
            }
        }



        public override void Update()
        {
            //設定消防員的移動邊界
            rightGameScreenBorder = rightMoveButton.X;
            leftGameScreenBorder = leftMoveButton.Width;

            if (!base.hasDialogShow)
            {
                TouchCollection tc = base.GetCurrentFrameTouchCollection();
                bool isMoveRight,isMoveLeft,isClickPause;
                isClickPause = isMoveLeft = isMoveRight = false;
                if (tc.Count > 0)  {
                    //取出點此frame下同時點擊的所有座標,並先對所有座標去做按鈕上的點擊判斷
                    foreach (TouchLocation touchLocation in tc) {
                        if (!isMoveRight)
                            isMoveRight = rightMoveButton.IsBoundingBoxClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y);
                        if (!isMoveLeft)
                            isMoveLeft = leftMoveButton.IsBoundingBoxClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y);
                        if (!isClickPause)
                            isClickPause = pauseButton.IsPixelClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y);
                    }

                    //遊戲邏輯判斷
                    if (isMoveLeft && !isMoveRight)
                    {
                        //Debug.WriteLine("Click Left Button");
                        player.MoveLeft(leftGameScreenBorder);
                    }
                    else if (!isMoveLeft && isMoveRight)
                    {
                        //Debug.WriteLine("Click Right Button");
                        player.MoveRight(rightGameScreenBorder);
                    }
                    else if (!isMoveLeft && !isMoveRight)
                    {
                        player.SetStand(); //設定站立
                    }
                    if(isClickPause){
                        this.SetPopGameDialog(DialogStateEnum.STATE_PAUSE);
                    }

                }
                else {
                    player.SetStand(); //設定站立
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
