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
    public class People : DropObjects
    {
        List<AnimationSprite> animationList;
        AnimationSprite pCurrentAnimation;
        bool isSaved,isDead;
        bool isSetDelay;
       
        float savedWalkSpeed; //被接住後離開畫面移動的速度
        const int FALLING_KEY = 0, CAUGHT_KEY = 1, WALK_KEY = 2;
        int walkOrienation;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentGameState">目前的遊戲狀態</param>
        /// <param name="id">遊戲物件的ID</param>
        /// <param name="x">X左上角座標</param>
        /// <param name="y">Y左上角座標</param>
        /// <param name="fallingSpeed">掉落的速度(數值應為正)</param>
        /// <param name="fallingWave">擺動的位移量(數值應為正)</param>
        /// <param name="walkSpeed"></param>
        /// <param name="orienation">移動的方向(0為左邊,1為右邊)</param>
        public People(GameState currentGameState, int id, float x, float y, float fallingSpeed, float fallingWave, float walkSpeed, int orienation)
            : base(currentGameState, id, x, y, fallingSpeed, fallingWave) 
        { 
            Init();
            //設定掉下來的速度與擺動的位移量
            
            this.walkOrienation = orienation;
            //沒有辦法預設參數值,只好這樣做
            if (walkSpeed >= 0)
                this.savedWalkSpeed = 5;
            else
                this.savedWalkSpeed = walkSpeed;

        }
        protected override void Init()
        {
            base.fallingNextXPos = this.x ;
            base.fallingNextYPos = this.y ;
            
            animationList = new List<AnimationSprite>();
            base.isFalling = true;
            base.isCaught = isDead = isSaved = false;
            isSetDelay = false;
           
        }
        /// <summary>
        /// 設定載入的圖片組,使用給予Key方式設定載入
        /// </summary>
        /// <param name="key"></param>
        private void SetTexture2DList(TexturesKeyEnum key)
        {
            AnimationSprite animation = new AnimationSprite(new Vector2(this.x, this.y), 300);
            animation.SetTexture2DList(base.gameState.GetTexture2DList(key));
            animationList.Add(animation);
        }
        /// <summary>
        /// 呼叫此方法設定為接到
        /// </summary>
        public override void SetCaught() {
            base.isCaught = true;
            base.isFalling = false;
            //切換圖片組
            pCurrentAnimation = animationList[CAUGHT_KEY];
        }
        public override void LoadResource(TexturesKeyEnum key)
        {
            SetTexture2DList(key);
            //設定Caught圖片組的第二張圖片延遲一秒 (從接住到站起來這段時間)
            //caught圖片組第一張是接住(Index =0),第二張是站起來,對接住的圖片delay 1秒(1000ms)
            if ( (animationList.Count == 2) && !isSetDelay)
            {
                animationList[CAUGHT_KEY].SetCertainFrameDelayTime(0, 1000);
                isSetDelay = true;
            }
            if (animationList.Count >= 3)
            {
                //設定目前的圖片組是"掉下來"
                pCurrentAnimation = animationList[FALLING_KEY];
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            pCurrentAnimation.Draw(spriteBatch);
        }

        //是否墜落到地板
        private bool IsFallingCollideFloor() {
            if ( (base.y+ base.height) >= base.gameState.GetBackgroundTexture().Height )
            {
                return true;
            }
            else
                return false;
        }
        //墜落搖晃時是否碰到邊界
        private bool IsFallingWaveCollideBorder()
        {
            //超出左邊邊框或超出右邊邊框 (70 與 80是包含了兩隻消防員的人物寬度)
            if ( ((base.x + base.width) >= base.gameState.GetRightGameScreenBorder() -70 )
                || (base.x < base.gameState.GetLeftGameScreenBorder() + 80)) 
            {
                return true;
            }
            else
                return false;
        }

        //拯救到後人物是否離開遊戲畫面
        private bool IsWalkOuterGameScreenBorder()
        {
            //超出左邊邊框或超出右邊邊框
            if (((base.x + base.width) >= base.gameState.GetBackgroundTexture().Width)
                || (base.x < 0) )
            {
                return true;
            }
            else
                return false;
        }


        public override void Update()
        {

            //判斷人物目前的狀態
            if (isFalling) {
                fallingNextYPos = this.y + fallingSpeed;
                //判斷有無落到地板 ,沒有就繼續往下掉
                if (!IsFallingCollideFloor())
                {
                    this.y = fallingNextYPos;
                }
                else {
                    isDead = true;
                }
            }
            else if (isCaught) { 
                //如果播完一輪,代表被接住也站起來=>切換成走路
                if(pCurrentAnimation.GetIsRoundAnimation()){
                    isCaught = false;
                    isSaved = true;
                    //切換到走路的圖片組
                    pCurrentAnimation = animationList[WALK_KEY];
                }
            }
            else if (isSaved) { 
                //向左或向右走
                if (walkOrienation == 0) { 
                    this.x -=savedWalkSpeed;
                }
                else if (walkOrienation == 1) {
                    this.x += savedWalkSpeed;
                }
                //走出遊戲畫面
                if (IsWalkOuterGameScreenBorder()) { 
                    //釋放資料(圖片除外)
                }

            }
            else if (isDead) { 
                //往上飄
            }

            //設定座標
            pCurrentAnimation.SetToLeftPos(base.x, base.y);
            //更新frame
            pCurrentAnimation.UpdateFrame(base.gameState.GetTimeSpan());
            //設定現在的圖片長寬為遊戲元件的長寬
            this.Height = pCurrentAnimation.GetCurrentFrameTexture().Height;
            this.Width = pCurrentAnimation.GetCurrentFrameTexture().Width;
        }
    }
}
