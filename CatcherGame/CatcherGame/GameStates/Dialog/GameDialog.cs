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
using CatcherGame.GameStates;

namespace CatcherGame.GameStates.Dialog
{
    //基礎類別,如果有新的Dialog皆要繼承此 GameDialog
    public abstract class GameDialog  
    {
        protected GameState currentState;
        protected Texture2D background;
        protected Vector2 backgroundPos;
        protected Button closeButton;
        protected int countId;
        protected SpriteBatch gameSateSpriteBatch;
        protected List<GameObject> gameObjects;
        protected bool isInit;

        public GameDialog(GameState pCurrentState)
        {
            gameObjects = new List<GameObject>();
            this.currentState = pCurrentState;
            countId = 0;
        }

        public abstract void BeginInit();

        //座標由子類別(真正要用的Dialog)設定
        public virtual void LoadResource()
        {
            closeButton.LoadResource(TextureManager.TexturesKeyEnum.DIALOG_CLOSE_BUTTON);
        }
        /// <summary>
        /// 關閉Dialog,子類別Dialgo可以透過此來關閉
        /// </summary>
        protected void CloseDialog() {
            currentState.SetPopGameDialog(DialogStateEnum.EMPTY);
        }

        //更新在Dialog中的所有遊戲物件
        public virtual void Update()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update();
            }
        }

        public virtual void Draw () {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(gameSateSpriteBatch);
            }
        }
        /// <summary>
        /// Set Main Game SpriteBatch to gameState
        /// </summary>
        /// <param name="gSpriteBatch"></param>
        public void SetSpriteBatch(SpriteBatch gSpriteBatch)
        {
            this.gameSateSpriteBatch = gSpriteBatch;
        }

        /// <summary>
        /// 加入遊戲物件至此State
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
    }
}
