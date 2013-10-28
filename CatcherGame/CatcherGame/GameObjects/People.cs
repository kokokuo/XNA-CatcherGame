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
        }
        public override void LoadResource(TexturesKeyEnum key)
        {
            SetTexture2DList(key);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            if (isFalling) {
                pCurrentAnimation = animationList[0];
            }
            else if (isCaught) { 
                pCurrentAnimation = animationList[1];
                
            }
           
        }
    }
}
