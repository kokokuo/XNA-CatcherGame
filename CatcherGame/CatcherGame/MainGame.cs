using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
//執行緒
using System.Threading;
using CatcherGame.TextureManager;
using CatcherGame.GameStates;

using System.Diagnostics;

namespace CatcherGame
{
    /// <summary>
    /// 這是您遊戲的主要型別
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //觸控點擊管理器
        TouchPanelInput touchPanelInputManager;
        Queue<TouchLocation> touchDatas;
        Thread workInputThread;

        //遊戲狀態表
        GameState[] gameStateTable;
        GameState pCurrentScreenState;

        //圖片管理器
        Texture2DManager texture2DManager;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Debug.WriteLine(graphics.PreferredBackBufferHeight);
            Debug.WriteLine(graphics.PreferredBackBufferWidth);

            Content.RootDirectory = "Content";

            // Windows Phone 預設的畫面播放速率為 30 fps。
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // 鎖定以延長電池壽命。
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            //遊戲狀態表
            gameStateTable = new GameState[(int)GameStateEnum.TOTAL_STATE_NUMBER];
            gameStateTable[(int)GameStateEnum.STATE_MENU] = new HomeMenuState(this);

            //輸入設定
            touchDatas = new Queue<TouchLocation>();
            touchPanelInputManager = new TouchPanelInput();
            touchPanelInputManager.TouchEvent +=touchPanelInputManager_TouchEvent;
            workInputThread = new Thread(new ThreadStart(touchPanelInputManager.ScanInput));
            workInputThread.Start();
        }
        //接收新的座標資料
        private void touchPanelInputManager_TouchEvent(TouchLocation data)
        {
            touchDatas.Enqueue(data);
        }

        /// <summary>
        /// 取得觸控的資料
        /// </summary>
        /// <returns></returns>
        public TouchLocation GetTouchLocation() {
            return touchDatas.Dequeue();
        }

        /// <summary>
        /// 取得觸控資料是否為空
        /// </summary>
        /// <returns></returns>
        public bool GetIsTouchDataQueueEmpty()
        {
            if (touchDatas.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 允許遊戲先執行所需的初始化程序，再開始執行。
        /// 這是遊戲可查詢必要服務和載入任何非圖形相關內容
        /// 的地方。呼叫 base.Initialize 會列舉所有元件
        /// 並予以初始化。
        /// </summary>
        protected override void Initialize()
        {
            // TODO: 在此新增初始化邏輯
            texture2DManager = new Texture2DManager(this);
            pCurrentScreenState = gameStateTable[(int)GameStateEnum.STATE_MENU];
            pCurrentScreenState.BeginInit();
            base.Initialize();
        }

        /// <summary>
        /// 每次遊戲都會呼叫 LoadContent 一次，這是載入所有內容
        /// 的地方。
        /// </summary>
        protected override void LoadContent()
        {
            // 建立可用來繪製紋理的新 SpriteBatch。
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: 在此使用 this.Content 來載入遊戲內容
            pCurrentScreenState.SetSpriteBatch(spriteBatch);
            pCurrentScreenState.LoadResource();
        }

        /// <summary>
        /// 每次遊戲都會呼叫 UnloadContent 一次，這是解除載入
        /// 所有內容的地方。
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: 在此解除載入任何非 ContentManager 內容
        }

        /// <summary>
        /// 允許遊戲執行如更新世界、
        /// 檢查衝突、收集輸入和播放音訊的邏輯。
        /// </summary>
        /// <param name="gameTime">提供時間值的快照。</param>
        protected override void Update(GameTime gameTime)
        {
            // 允許遊戲結束
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: 在此新增您的更新邏輯
            pCurrentScreenState.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// 當遊戲應該自我繪製時會呼叫此項目。
        /// </summary>
        /// <param name="gameTime">提供時間值的快照。</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: 在此新增您的繪圖程式碼
            spriteBatch.Begin();
            //繪製現在的狀態
            pCurrentScreenState.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public List<Texture2D> GetTexture2DList(TexturesKeyEnum key)
        {
            return texture2DManager.GetTexture2DList(key);
        }


        public void SetNextGameState(GameStateEnum nextStateKey) {
            //切換遊戲狀態
            pCurrentScreenState = gameStateTable[(int)nextStateKey];
            if (!pCurrentScreenState.GetGameStateHasInit)
            {
                //進入新狀態所做的初始化
                pCurrentScreenState.BeginInit();
                //載入資源
                pCurrentScreenState.SetSpriteBatch(spriteBatch);
                pCurrentScreenState.LoadResource();
            }
        }
    }
}
