using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CatcherGame;
using CatcherGame.Sprite;
using CatcherGame.GameStates;
using CatcherGame.GameStates.Screen;
using CatcherGame.TextureManager;
namespace CatcherGame.GameObjects
{
    public class Button : GameObject , Clickable
    {
        private AnimationSprite buttonAnimation;
        private Texture2D currentTexture;
        public Button(GameScreen gameScreen, int id, float x, float y)
            : base(gameScreen, id, x, y)
        {
            Init();
        }

        protected override void Init()
        {
            this.x = x;
            this.y = y;
            buttonAnimation = new AnimationSprite(new Vector2(this.x, this.y), 300);
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
            buttonAnimation.SetTexture2DList(gameScreen.GetTexture2DList(key));
            this.Height = buttonAnimation.GetCurrentFrameTexture().Height;
            this.Width = buttonAnimation.GetCurrentFrameTexture().Width;
        }

        public override void Update()
        {
            buttonAnimation.UpdateFrame(gameScreen.GetTimeSpan());
            currentTexture = buttonAnimation.GetCurrentFrameTexture();
            //設定現在的圖片長寬為遊戲元件的長寬
            this.Height = buttonAnimation.GetCurrentFrameTexture().Height;
            this.Width = buttonAnimation.GetCurrentFrameTexture().Width;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            buttonAnimation.Draw(spriteBatch);
        }

        /// <summary>
        /// 設定載入的圖片組(尚未使用)
        /// </summary>
        /// <param name="texture2DList">直接給予List</param>
        private void SetTexture2DList(List<Texture2D> texture2DList)
        {
            buttonAnimation.SetTexture2DList(texture2DList);
            this.Height = buttonAnimation.GetCurrentFrameTexture().Height;
            this.Width = buttonAnimation.GetCurrentFrameTexture().Width;
        }
        
        
       
        /// <summary>
        /// 判斷有無點擊到Button(像素碰撞)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsClick(float x, float y)
        {
            Color[] currtentTextureColor = new Color[currentTexture.Width * currentTexture.Height];
            currentTexture.GetData<Color>(currtentTextureColor);
            //偵測按下去的座標換算成圖片圖片的像素位置
            Color clickPoint = currtentTextureColor[((int)x - currentTexture.Bounds.Left) + (((int)y) - currentTexture.Bounds.Top) * currentTexture.Bounds.Width];
            if (clickPoint.A != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
