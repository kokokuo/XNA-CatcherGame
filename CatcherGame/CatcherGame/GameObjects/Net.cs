using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

using CatcherGame.GameStates;
using CatcherGame.TextureManager;
using CatcherGame.Sprite;

namespace CatcherGame.GameObjects
{
    //網子類別:尚未製作完成
    public class Net : GameObject
    {
        AnimationSprite netStateAnimation; //網子動畫

        public Net(GameState currentGameState, int id, float x, float y)
            : base(currentGameState, id, x, y) 
        {
            Init();
        }

        protected override void Init()
        {
            this.x = x;
            this.y = y;
            netStateAnimation = new AnimationSprite(new Vector2(this.x, this.y), 300);
        }

        public override void LoadResource(TexturesKeyEnum key)
        {
            SetTexture2DList(key);
        }

        /// <summary>
        /// 設定載入的圖片組,使用給予Key方式設定載入
        /// </summary>
        /// <param name="key"></param>
        private void SetTexture2DList(TexturesKeyEnum key)
        {
            netStateAnimation.SetTexture2DList(base.gameState.GetTexture2DList(key));
            this.Height = netStateAnimation.GetCurrentFrameTexture().Height;
            this.Width = netStateAnimation.GetCurrentFrameTexture().Width;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            netStateAnimation.Draw(spriteBatch);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
