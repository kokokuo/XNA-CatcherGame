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
    public class EffectItem : DropObjects
    {
        AnimationSprite itemAnimation;

        public EffectItem(GameState currentGameState, DropObjectsKeyEnum key, int id, float x, float y, float fallingSpeed, float fallingWave)
            : base(currentGameState, key, id, x, y, fallingSpeed, fallingWave)
        {
            Init();
        }
      
        public override void SetCaught()
        {
            base.isCaught = true;
            base.isFalling = false;
        }

        protected override void Init()
        {
            itemAnimation = new AnimationSprite(new Vector2(this.x, this.y), 300f);   
        }

        /// <summary>
        /// 設定載入的圖片組,使用給予Key方式設定載入
        /// </summary>
        /// <param name="key"></param>
        private void SetTexture2DList(TexturesKeyEnum key)
        {
            itemAnimation.SetTexture2DList(base.gameState.GetTexture2DList(key));
            
            this.Height = itemAnimation.GetCurrentFrameTexture().Height;
            this.Width = itemAnimation.GetCurrentFrameTexture().Width;
        }

        public override void LoadResource(TexturesKeyEnum key)
        {
            SetTexture2DList(key);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            itemAnimation.Draw(spriteBatch);
        }

        public override void Update()
        {
            if (isFalling) {
                fallingNextYPos = this.y + fallingSpeed;
                //判斷有無落到地板 ,沒有就繼續往下掉
                if (!base.IsFallingCollideFloor())
                {
                    this.y = fallingNextYPos;
                }
                else
                {
                    isDead = true;
                    isFalling = false;
                }    
            }
            else if (isCaught)
            {
                //設定屬性效果 
                //在螢幕上顯示(呼叫gameState方法)
            }
            else if (isDead) {
                //從DropObjectList中移除
                ((PlayGameState)gameState).RemoveDropObjs(this);
                //移除自己
                ((PlayGameState)this.gameState).RemoveGameObject(this.id);
            }
        }
    }
}
