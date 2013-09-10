using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//XNA Tool
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using CatcherGame.GameObjects;


namespace CatcherGame.GameStates
{
    public abstract class GameState
    {
        protected SpriteBatch gameSateSpriteBatch;
        protected List<GameObject> gameObjects;
        protected int width;
        protected int height;
        protected bool isInit;

        public GameState() {
            gameObjects = new List<GameObject>();
            isInit = false;
        }
        
        public abstract void LoadResource();
        public abstract void BeginInit();
        
        public virtual void Update()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update();
            }
            
        }

        public virtual void Draw()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(gameSateSpriteBatch);
            }
        }
        /// <summary>
        /// 加入遊戲物件至此State
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
        

        /// <summary>
        /// 設定或取得遊戲狀態的寬(以遊戲狀態為背景),預設為手機的螢幕寬
        /// </summary>
        public int SetGetWidth
        {
            set { width = value; }
            get { return width; }
        }

        /// <summary>
        /// 設定或取得遊戲狀態的高(以遊戲狀態為背景),預設為手機的螢幕高
        /// </summary>
        public int SetGetHeight
        {
            set { height = value; }
            get { return height; }
        }
        /// <summary>
        /// 取得此State所有遊戲物件
        /// </summary>
        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
        }

        public bool GetGameStateHasInit {
            get { return isInit; }
        }

        
    }
}
