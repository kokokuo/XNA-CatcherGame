using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using CatcherGame;
using CatcherGame.Sprite;
using CatcherGame.GameStates;

using CatcherGame.TextureManager;
namespace CatcherGame.GameObjects
{
    public delegate void ValueUpdateEventHandler(int newValue);

    public class FiremanPlayer : GameObject
    {
        public event ValueUpdateEventHandler SaveNewPerson;
      

        AnimationSprite walkAnimation;
        //移動步伐
        const int LEFT_MOVE_STEP = -5;
        const int RIGHT_MOVE_STEP = 5;
        bool isWalking; //是否移動
        Net savedNet; //網子類別(Has)
        float init_x, init_y;

        int savePeopleNumber;
      
        public FiremanPlayer(GameState currentGameState, int id, float x, float y)
            : base(currentGameState, id, x, y)
        {
            Init();
            //取得網子

            //數值待解決(改為依裝置吃尺寸去調整)
             savedNet = new Net(currentGameState, id, x + 73, y + 85, this);
             savedNet.LoadResource(TexturesKeyEnum.PLAY_NET);
        }

        protected override void Init()
        {
            isWalking = false;
            this.init_x = this.x = x;
            this.init_y = this.y = y;
            walkAnimation = new AnimationSprite(new Vector2(this.x, this.y), 300);

            savePeopleNumber = 0;
           
        }

        /// <summary>
        /// 累加新的Person
        /// </summary>
        public void AddSavedPerson(){
            savePeopleNumber++;
            //觸發事件
            if (SaveNewPerson != null){
                SaveNewPerson.Invoke(savePeopleNumber);
            }
        }
       
        /// <summary>
        /// 確認掉落的所有元件有無接觸到網子
        /// </summary>
        /// <param name="fallingObjs"></param>
        public void CheckIsCaught(List<DropObjects> fallingObjs) {
            savedNet.CheckCollision(fallingObjs);    
        }

        public override void LoadResource(TextureManager.TexturesKeyEnum key)
        {
            SetTexture2DList(key);
        }

        /// <summary>
        /// 設定載入的圖片組,使用給予Key方式設定載入
        /// </summary>
        /// <param name="key"></param>
        private void SetTexture2DList(TexturesKeyEnum key)
        {
            walkAnimation.SetTexture2DList(base.gameState.GetTexture2DList(key));
            this.Height = walkAnimation.GetCurrentFrameTexture().Height;
            this.Width = walkAnimation.GetCurrentFrameTexture().Width;
        }


       

        public override void Update()
        {
            //如果正在移動則更新圖像動畫
            if (isWalking){
                walkAnimation.SetToLeftPos(this.x, this.y);
                walkAnimation.UpdateFrame(base.gameState.GetTimeSpan());
                //設定現在的圖片長寬為遊戲元件的長寬
                this.Height = walkAnimation.GetCurrentFrameTexture().Height;
                this.Width = walkAnimation.GetCurrentFrameTexture().Width;
            
            }
            savedNet.Update();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            walkAnimation.Draw(spriteBatch);
            savedNet.Draw(spriteBatch);
        }

        //設定站立
        public void SetStand() {
            isWalking = false;
        }
        //設定消防員往左移動
        public void MoveLeft(float leftGameScreenBorder)
        {
            //檢查如果要移動是否會超處邊界(以網子為基準) 不會才給予下一步的移動座標
            if ((this.savedNet.X + LEFT_MOVE_STEP) >= leftGameScreenBorder)
            {
                //Debug.WriteLine("Can Move Left Way");
                this.x += LEFT_MOVE_STEP;
                this.savedNet.X += LEFT_MOVE_STEP; //網子跟著移動
                isWalking = true;
                
            }
        }

        //設定消防員往右移動
        public void MoveRight(float rightGameScreenBorder)
        {
            //檢查如果要移動是否會超處邊界(以網子為基準) 不會才給予下一步的移動座標
            if ((this.savedNet.X + this.savedNet.Width) + RIGHT_MOVE_STEP <= rightGameScreenBorder)
            {
                //Debug.WriteLine("Can Move Right Way");
                this.x += RIGHT_MOVE_STEP;
                this.savedNet.X += RIGHT_MOVE_STEP; //網子跟著移動
                isWalking = true;
                
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!base.disposed)
            {

                if (disposing)
                {
                    if (walkAnimation != null)
                    {
                        walkAnimation.Dispose();
                    }
                    if (savedNet != null) {
                        savedNet.Dispose();
                    }
                    Console.WriteLine("FirePlayer disposed.");
                }
            }
            disposed = true;
        }
    }
}
