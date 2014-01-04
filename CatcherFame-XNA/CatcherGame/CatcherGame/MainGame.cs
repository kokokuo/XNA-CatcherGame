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
using CatcherGame.FontManager;

using System.Diagnostics;
namespace CatcherGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Queue<TouchLocation> touchQueue;

        //遊戲狀態表
        Dictionary<GameStateEnum, GameState> gameStateTable;
        GameState pCurrentScreenState;

        //圖片管理器
        Texture2DManager texture2DManager;

        //文字管理器
        SpriteFontManager fontManager;

        //現在這張frame所擁有的所有觸控點集合
        TouchCollection currtenTouchCollection;
        private bool isMessageBoxShow = false;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Debug.WriteLine(_graphics.PreferredBackBufferHeight);
            Debug.WriteLine(_graphics.PreferredBackBufferWidth);

            _graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            // Windows Phone 預設的畫面播放速率為 30 fps。
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // 鎖定以延長電池壽命。
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            //遊戲狀態表
            gameStateTable = new Dictionary<GameStateEnum, GameState>();
            gameStateTable.Add(GameStateEnum.STATE_MENU, new HomeMenuState(this));
            gameStateTable.Add(GameStateEnum.STATE_START_COMIC, new GameStartComicState(this));
            gameStateTable.Add(GameStateEnum.STATE_PLAYGAME, new PlayGameState(this));
            gameStateTable.Add(GameStateEnum.STATE_GAME_OVER, new GameOverState(this));
            //設定水平橫向時的座標
            TouchPanel.DisplayOrientation = Microsoft.Xna.Framework.DisplayOrientation.LandscapeLeft;
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.FreeDrag | GestureType.None | GestureType.Hold;


            touchQueue = new Queue<TouchLocation>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // TODO: 在此新增初始化邏輯
            fontManager = new SpriteFontManager(this);
            texture2DManager = new Texture2DManager(this);
            pCurrentScreenState = gameStateTable[GameStateEnum.STATE_MENU];
            pCurrentScreenState.BeginInit();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // TODO: 在此使用 this.Content 來載入遊戲內容
            pCurrentScreenState.SetSpriteBatch(_spriteBatch);
            pCurrentScreenState.LoadResource();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            // 允許遊戲結束 預設方法
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            TouchCollection tc = TouchPanel.GetState();
            currtenTouchCollection = tc;
            foreach (TouchLocation location in tc)
            {
                touchQueue.Enqueue(location);
            }
            pCurrentScreenState.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // TODO: 在此新增您的繪圖程式碼
            _spriteBatch.Begin();
            //繪製現在的狀態
            pCurrentScreenState.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// 清除TouchQueue裡面的所有狀態
        /// </summary>
        public void ClearTouchQueue()
        {
            touchQueue.Clear();
        }

        public bool IsEmptyQueue()
        {
            if (touchQueue.Count != 0) return false;
            else return true;
        }
        public TouchLocation GetTouchLocation()
        {
            return touchQueue.Dequeue();
        }

        /// <summary>
        /// 取得當下觸碰畫面的所有觸控點
        /// </summary>
        /// <returns></returns>
        public TouchCollection GetCurrentFrameTouchCollection()
        {
            return currtenTouchCollection;
        }

        /// <summary>
        /// 取得對應Key的圖片集
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Texture2D> GetTexture2DList(TexturesKeyEnum key)
        {
            return texture2DManager.GetTexture2DList(key);
        }

        /// <summary>
        /// 取得文字資源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SpriteFont GetSpriteFontFromKeyByMainGame(SpriteFontKeyEnum key)
        {

            return fontManager.GetSpriteFontFromKey(key);
        }



        public void SetNextGameState(GameStateEnum nextStateKey)
        {
            //切換遊戲狀態
            pCurrentScreenState = gameStateTable[nextStateKey];
            if (!pCurrentScreenState.GetGameStateHasInit)
            {
                //進入新狀態所做的初始化
                pCurrentScreenState.BeginInit();
                //載入資源
                pCurrentScreenState.SetSpriteBatch(_spriteBatch);
                pCurrentScreenState.LoadResource();
            }

        }



        public int GetDeviceScreenWidth() {
            return _graphics.PreferredBackBufferWidth;
        }
        public int GetDeviceScreenHeight()
        {
            return _graphics.PreferredBackBufferHeight;
        }
    }
}
