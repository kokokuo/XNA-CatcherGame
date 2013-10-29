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
    public class People : GameObject
    {
        List<AnimationSprite> animationList;
        AnimationSprite pCurrentAnimation;
        bool isFalling, isCaught, isSaved;
        const int FALLING_KEY = 0, CAUGHT_KEY = 1, WALK_KEY = 2;
        public People(GameState currentGameState, int id, float x, float y, float fallingSpeed, float fallingWave)
            : base(currentGameState, id, x, y) 
        { 
            Init();
        }
        protected override void Init()
        {
            this.x = x;
            this.y = y;
            animationList = new List<AnimationSprite>();
            isFalling = true;
            isCaught = isSaved = false;
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
        public void SetCaught() {
            isCaught = true;
            isFalling = false;
            //切換圖片組
            pCurrentAnimation = animationList[CAUGHT_KEY];
        }
        public override void LoadResource(TexturesKeyEnum key)
        {
            SetTexture2DList(key);
            //設定Caught圖片組的第二張圖片延遲一秒 (從接住到站起來這段時間)
            //caught圖片組第一張是接住(Index =0),第二張是站起來,對接住的圖片delay 1秒(1000ms)
            animationList[CAUGHT_KEY].SetCertainFrameDelayTime(0, 1000);

            //設定目前的圖片組是掉下來
            pCurrentAnimation = animationList[FALLING_KEY];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            pCurrentAnimation.Draw(spriteBatch);
        }

        public override void Update()
        {
            if (isFalling) {
                //判斷有無落到地板 
            }
            else if (isCaught) { 
               
                if(pCurrentAnimation.GetCurrentFrameNumber == (pCurrentAnimation.GetFrameSize -1 )){
                    isCaught = false;
                    
                }
            }
            else if(isSaved)
        }
    }
}
