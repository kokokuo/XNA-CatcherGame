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
using CatcherGame.GameStates.Dialog;
using CatcherGame.TextureManager;

namespace CatcherGame.GameStates.Screen
{
    public abstract class GameScreen : GameState
    {
        protected MainGame mainGame;
        protected List<int> willRemoveObjectId;
        protected Dictionary<DialogStateEnum,GameDialog> dialogTable;
        protected GameDialog pCurrentDialog;
        protected bool hasDialogShow;

        public GameScreen(MainGame mainGamePointer){
            this.mainGame = mainGamePointer;
            width = 480;
            height = 800;
            hasDialogShow = false;
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
        public override void Update()
        {
            
            //如果有顯示對話框,則更新對話框的物件
            if (hasDialogShow)
            {
                foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
                {
                    dialog.Value.Update();
                }
            }
            else
            {
                base.Update();
            }
        }
        public override void Draw()
        {
            base.Draw(); //顯示對話框,仍需要繪製背景畫面
            //如果有要顯示對話框,繪製對話框
            if (hasDialogShow)
            {
                pCurrentDialog.Draw();
            }
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
            if (nextDialogKey != DialogStateEnum.EMPTY){
                pCurrentDialog = dialogTable[nextDialogKey];
                hasDialogShow = true;
            }
            else
                hasDialogShow = false;
        }
        public TimeSpan GetTimeSpan() {
            return mainGame.TargetElapsedTime;
        }
        public SpriteBatch GetSpriteBatch() {
            return gameSateSpriteBatch;
        }
    }
}
