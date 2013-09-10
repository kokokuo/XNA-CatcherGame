using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


using CatcherGame.GameObjects;
using CatcherGame.GameStates.Screen;
namespace CatcherGame.GameStates.Dialog
{
    public abstract class GameDialog : GameState
    {
        protected GameScreen currentScreen;
        protected Texture2D background;
        protected Vector2 backgroundPos;
        protected Button closeButton;
        protected int countId;
        public GameDialog(GameScreen pCurrentScreen) {
            this.currentScreen = pCurrentScreen;
            countId = 0;
        }
        //座標由子類別(真正要用的Dialog)設定
        public override void LoadResource()
        {
            closeButton.LoadResource(TextureManager.TexturesKeyEnum.DIALOG_CLOSE_BUTTON);
        }
        /// <summary>
        /// 關閉Dialog
        /// </summary>
        protected void CloseDialog() {
            currentScreen.SetNextGameDialog(DialogStateEnum.EMPTY);
        }

        /// <summary>
        /// Set Main Game SpriteBatch to gameState
        /// </summary>
        /// <param name="gSpriteBatch"></param>
        public void SetSpriteBatch(SpriteBatch gSpriteBatch)
        {
            this.gameSateSpriteBatch = gSpriteBatch;
        }
    }
}
