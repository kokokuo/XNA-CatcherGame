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
using CatcherGame.GameStates.Dialog;
using CatcherGame.TextureManager;

namespace CatcherGame.GameStates
{
    public abstract class GameState
    {
        protected SpriteBatch gameSateSpriteBatch;
        protected List<GameObject> gameObjects;
        protected int width;
        protected int height;
        protected bool isInit;
        protected List<int> willRemoveObjectId;
        protected Dictionary<DialogStateEnum, GameDialog> dialogTable;
        protected GameDialog pCurrentDialog;
        protected bool hasDialogShow;
        protected MainGame mainGame;

        public GameState(MainGame mainGamePointer)
        {
            gameObjects = new List<GameObject>();
            this.mainGame = mainGamePointer;
            
            isInit = false;
            width = 480;
            height = 800;
            hasDialogShow = false;
        }
        
        public abstract void LoadResource();
        public abstract void BeginInit();
        
        public virtual void Update()
        {
            
           
            //如果有顯示對話框,則更新對話框的物件
            if (hasDialogShow)
            {
                foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
                {
                    dialog.Value.Update();
                }
            }
            else {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update();
                }
            }
        }

        public virtual void Draw()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(gameSateSpriteBatch);
            }
            //如果有要顯示對話框,繪製對話框
            if (hasDialogShow)
            {
                pCurrentDialog.Draw();
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


        /// <summary>
        /// Set Main Game SpriteBatch to gameState
        /// </summary>
        /// <param name="gSpriteBatch"></param>
        public void SetSpriteBatch(SpriteBatch gSpriteBatch)
        {
            this.gameSateSpriteBatch = gSpriteBatch;
        }
        /// <summary>
        /// 將 id 放入準備要被刪除的 list
        /// </summary>
        /// <param name="id"></param>
        public void RemoveGameObject(int id)
        {
            willRemoveObjectId.Add(id);
        }

        /// <summary>
        /// 真正將 GameObject 刪除
        /// </summary>
        private void RemoveGameObjectFromList()
        {
            foreach (int removeId in willRemoveObjectId)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject.Id == removeId)
                    {
                        gameObjects.Remove(gameObject);
                        break;
                    }
                }
            }
            willRemoveObjectId.Clear();
        }

        /// <summary>
        /// 透過mainGame取得 Texture2DList
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Texture2D> GetTexture2DList(TexturesKeyEnum key)
        {
            return mainGame.GetTexture2DList(key);
        }

        /// <summary>
        /// 透過mainGame取得是否有輸入
        /// </summary>
        /// <returns></returns>
        public bool GetIsTouchDataQueueEmpty()
        {
            return mainGame.GetIsTouchDataQueueEmpty();
        }
        /// <summary>
        /// 透過mainGame取得輸入
        /// </summary>
        /// <returns></returns>
        public TouchLocation GetTouchData()
        {
            return mainGame.GetTouchLocation();
        }

        public void SetNextGameDialog(DialogStateEnum nextDialogKey)
        {
            if (nextDialogKey != DialogStateEnum.EMPTY)
            {
                pCurrentDialog = dialogTable[nextDialogKey];
                hasDialogShow = true;
            }
            else
                hasDialogShow = false;
        }
        public TimeSpan GetTimeSpan()
        {
            return mainGame.TargetElapsedTime;
        }
        public SpriteBatch GetSpriteBatch()
        {
            return gameSateSpriteBatch;
        }
    }
}
