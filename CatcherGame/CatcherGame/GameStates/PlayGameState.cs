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
using CatcherGame.FontManager;

namespace CatcherGame.GameStates
{
    public class PlayGameState  :GameState
    {
        const int FIREMAN_INIT_X = 300;
        const int FIREMAN_INIT_Y = 355;
       
        Button pauseButton;
        Button leftMoveButton;
        Button rightMoveButton;

         List<int> willRemoveObjectId;

        int objIdCount;
        FiremanPlayer player;
        int lostPeopleNumber;
        int savedPeopleNumber;

        //文字資源
        SpriteFont savedPeopleNumberFont;
        SpriteFont lostPeopleNumberFont;

        //純粹圖片,無任何邏輯運算
        TextureLayer smokeTexture;
        TextureLayer lifeTexture;
        TextureLayer scoreTexture;
        List<DropObjects> FallingObjects;
        People oldLady; //test
        People fatDance; //test2
        People littleGirl;
        People manStubble;
        People naughtyBoy;
        People oldMan;
        People roxanne; 

        public PlayGameState(MainGame gMainGame) 
            :base(gMainGame)
        {
            dialogTable = new Dictionary<DialogStateEnum, GameDialog>();
            dialogTable.Add(DialogStateEnum.STATE_PAUSE, new PauseDialog(this));
            FallingObjects = new List<DropObjects>();
            willRemoveObjectId = new List<int>();
            base.x = 0; base.y = 0;
            base.backgroundPos = new Vector2(base.x, base.y);
        }


        public override void BeginInit()
        {
           
            objIdCount = 0;
            lostPeopleNumber = 3;
            savedPeopleNumber = 0;
            pauseButton = new Button(this, objIdCount++, 0, 0);
            leftMoveButton = new Button(this, objIdCount++, 0, 355);
            rightMoveButton = new Button(this, objIdCount++, 700, 355);

            player = new FiremanPlayer(this, objIdCount++, FIREMAN_INIT_X, FIREMAN_INIT_Y);
            player.SaveNewPerson +=player_SaveNewPerson;

            smokeTexture = new TextureLayer(this,objIdCount++, 0, 0);
            lifeTexture = new TextureLayer(this,objIdCount++, 0, 0);
            scoreTexture = new TextureLayer(this, objIdCount++, 0, 0);
            oldLady = new People(this, objIdCount++, 170, 0, 3, 0, 3, 1);
            fatDance = new People(this, objIdCount++, 200, -50, 2, 0, 3, 0);

            littleGirl = new People(this, objIdCount++, 250, -50, 4, 0, 3, 1);
            manStubble = new People(this, objIdCount++, 270, -20, 4, 0, 3, 0);
            naughtyBoy = new People(this, objIdCount++, 320, -10, 4, 0, 3, 1);
            oldMan = new People(this, objIdCount++, 360, -10, 4, 0, 3, 1);
            roxanne = new People(this, objIdCount++, 400, -10, 6, 0, 6, 1);

            //test
            AddGameObject(oldLady);
            FallingObjects.Add(oldLady);

            //test2
            AddGameObject(fatDance);
            FallingObjects.Add(fatDance);

            AddGameObject(littleGirl);
            FallingObjects.Add(littleGirl);

            //test2
            AddGameObject(manStubble);
            FallingObjects.Add(manStubble);

            //test2
            AddGameObject(naughtyBoy);
            FallingObjects.Add(naughtyBoy);

            //test2
            AddGameObject(oldMan);
            FallingObjects.Add(oldMan);

            //test2
            AddGameObject(roxanne);
            FallingObjects.Add(roxanne);

            //加入遊戲元件
            AddGameObject(player);
            
            AddGameObject(leftMoveButton);
            AddGameObject(rightMoveButton);
            AddGameObject(pauseButton);
           
            

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

        private void player_SaveNewPerson(int newValue)
        {
            savedPeopleNumber = newValue;
        }

        //釋放遊戲中的所有資料
        public void Release() {
            FallingObjects.Clear();
            smokeTexture.Dispose();
            lifeTexture.Dispose();
            scoreTexture.Dispose();
            willRemoveObjectId.Clear();
            //指向NULL
            savedPeopleNumberFont = null;
            lostPeopleNumberFont = null;
            //取消事件訂閱
            player.SaveNewPerson -= player_SaveNewPerson;
            foreach (GameObject obj in gameObjects) {
                obj.Dispose();
            }
            gameObjects.Clear();
            base.isInit  = false;
        }

        public void SubtractCanLostPeopleNumber() {
            this.lostPeopleNumber--;
            if (this.lostPeopleNumber <= 0) {
                //Release();
                //遊戲結束
                Debug.WriteLine("Game Over...");
                //切換到遊戲結束的畫面
            }
        }

        public override void LoadResource()
        {
            //載入文字
            savedPeopleNumberFont = base.GetSpriteFontFromKeyByGameState(SpriteFontKeyEnum.PLAY_SAVED_PEOPLE_FONT);
            lostPeopleNumberFont = base.GetSpriteFontFromKeyByGameState(SpriteFontKeyEnum.PLAT_LOST_PEOPLE_FONT);

            //載入圖片
            base.background = base.GetTexture2DList(TexturesKeyEnum.PLAY_BACKGROUND)[0];
            pauseButton.LoadResource(TexturesKeyEnum.PLAY_PAUSE_BUTTON);
            leftMoveButton.LoadResource(TexturesKeyEnum.PLAY_LEFT_MOVE_BUTTON);
            rightMoveButton.LoadResource(TexturesKeyEnum.PLAY_RIGHT_MOVE_BUTTON);
            player.LoadResource(TexturesKeyEnum.PLAY_FIREMAN);
            

            smokeTexture.LoadResource(TexturesKeyEnum.PLAY_SMOKE);
            lifeTexture.LoadResource(TexturesKeyEnum.PLAY_LIFE);
            scoreTexture.LoadResource(TexturesKeyEnum.PLAY_SCORE);
            //test
            oldLady.LoadResource(TexturesKeyEnum.PLAY_FLYOLDELADY_FALL);
            oldLady.LoadResource(TexturesKeyEnum.PLAY_FLYOLDELADY_CAUGHT);
            oldLady.LoadResource(TexturesKeyEnum.PLAY_FLYOLDELADY_WALK);

            //test2
            fatDance.LoadResource(TexturesKeyEnum.PLAY_FATDANCE_FALL);
            fatDance.LoadResource(TexturesKeyEnum.PLAY_FATDANCE_CAUGHT);
            fatDance.LoadResource(TexturesKeyEnum.PLAY_FATDANCE_WALK);

            littleGirl.LoadResource(TexturesKeyEnum.PLAY_LITTLEGIRL_FALL);
            littleGirl.LoadResource(TexturesKeyEnum.PLAY_LITTLEGIRL_CAUGHT);
            littleGirl.LoadResource(TexturesKeyEnum.PLAY_LITTLEGIRL_WALK);

            manStubble.LoadResource(TexturesKeyEnum.PLAY_MANSTUBBLE_FALL);
            manStubble.LoadResource(TexturesKeyEnum.PLAY_MANSTUBBLE_CAUGHT);
            manStubble.LoadResource(TexturesKeyEnum.PLAY_MANSTUBBLE_WALK);

            naughtyBoy.LoadResource(TexturesKeyEnum.PLAY_NAUGHTYBOY_FALL);
            naughtyBoy.LoadResource(TexturesKeyEnum.PLAY_NAUGHTYBOY_CAUGHT);
            naughtyBoy.LoadResource(TexturesKeyEnum.PLAY_NAUGHTYBOY_WALK);

            oldMan.LoadResource(TexturesKeyEnum.PLAY_OLDMAN_FALL);
            oldMan.LoadResource(TexturesKeyEnum.PLAY_OLDMAN_CAUGHT);
            oldMan.LoadResource(TexturesKeyEnum.PLAY_OLDMAN_WALK);

            roxanne.LoadResource(TexturesKeyEnum.PLAY_ROXANNE_FALL);
            roxanne.LoadResource(TexturesKeyEnum.PLAY_ROXANNE_CAUGHT);
            roxanne.LoadResource(TexturesKeyEnum.PLAY_ROXANNE_WALK);


            //設定消防員的移動邊界(包含角色掉落的邊界也算在內)
            base.rightGameScreenBorder = rightMoveButton.X;
            base.leftGameScreenBorder = leftMoveButton.Width;

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
                player.CheckIsCaught(FallingObjects);

                //如果有要移除的元件,執行移除方法
                if (willRemoveObjectId.Count > 0) {
                    RemoveGameObjectFromList();
                }
            }
            base.Update();
        }
        public override void Draw()
        {
            // 繪製主頁背景
            gameSateSpriteBatch.Draw(base.background, base.backgroundPos, Color.White);
            base.Draw();
            smokeTexture.Draw(this.GetSpriteBatch());
            lifeTexture.Draw(this.GetSpriteBatch());
            scoreTexture.Draw(this.GetSpriteBatch());
            //繪製文字資源
            //座標位置有待調整為動態
            gameSateSpriteBatch.DrawString(savedPeopleNumberFont, savedPeopleNumber.ToString(),new Vector2(40,130) ,Color.White);
            gameSateSpriteBatch.DrawString(lostPeopleNumberFont, lostPeopleNumber.ToString(),new Vector2(765,10), Color.White);
        }


        /// <summary>
        /// 將 id 放入準備要被刪除的 list
        /// </summary>
        /// <param name="id"></param>
        public void RemoveGameObject(int id)
        {
            willRemoveObjectId.Add(id);
        }

        /// <summary>
        /// 真正將 GameObject 刪除
        /// </summary>
        private void RemoveGameObjectFromList()
        {
            //有空在修改成LINQ語法...
            foreach (int removeId in willRemoveObjectId)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject.Id == removeId)
                    {
                        gameObject.Dispose(); //釋放資源
                        gameObjects.Remove(gameObject);
                        break;
                    }
                }
            }
            willRemoveObjectId.Clear();
        }
    }
}
