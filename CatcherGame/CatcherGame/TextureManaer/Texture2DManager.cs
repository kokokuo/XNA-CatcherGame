using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using CatcherGame;
using Microsoft.Xna.Framework;
namespace CatcherGame.TextureManager
{
    /// <summary>
    /// 圖片管理器
    /// </summary>
    class Texture2DManager
    {
        private MainGame mainGame;
        private Dictionary<TexturesKeyEnum, List<Texture2D>> _dictionary;

        public Texture2DManager(MainGame mainGame)
        {
            Debug.WriteLine("Texture2DManager construct ...");
            this.mainGame = mainGame;
            _dictionary = new Dictionary<TexturesKeyEnum, List<Texture2D>>();

            Debug.WriteLine("Load Texture2Ds ...");
            ///Load圖片
            LoadTest();
            
            //載入選單相關圖像資源
            LoadMenuBackground();
            LoadMenuSide();
            LoadPlayButton();
            LoadHowToPlayButton();
            LoadTopScoreButton();
            LoadDictionaryButton();

            //載入對話框關閉用的按鈕
            LoadCloseDialog();

            //載入最高分對話框所用的圖像資源
            LoadTopScoreDialogBackground();

            //載入遊戲中的資源
            LoadPlayBackground();
            LoadPlayGamePauseButton();
            LoadPlayGameLeftMoveButton();
            LoadPlayGameRightMoveButton();
            LoadPlayGameFireman();
            LoadPlayGameSmokePicture();
            LoadPlayGameScorePicture();
            LoadPlayGameLifePicture();
            LoadPlayNet();

            //載入遊戲中的暫停對話框
            LoadPauseDialogBackground();
            LoadPauseDialogExitButton();
            LoadPauseDialogContinueButton();

            //載入字典對話框所用的圖像資源
            LoadDictionaryDialogBackground();
            LoadDictionaryDialogLeftButton();
            LoadDictionaryDialogRightButton();
            LoadDictionaryDialogRightButton();
            LoadDictionaryDialogContentTexture();
            LoadDictionaryDialogPictureTexture();

            Debug.WriteLine("Load Texture2Ds Done ");
        }
        /// <summary>
        /// 取得對應Key值的圖片組
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Texture2D> GetTexture2DList(TexturesKeyEnum key)
        {
            return _dictionary[key];
        }

        public static Texture2D Flip(Texture2D source, bool vertical, bool horizontal)
        {
            Texture2D flipped = new Texture2D(source.GraphicsDevice, source.Width, source.Height);
            Color[] data = new Color[source.Width * source.Height];
            Color[] flippedData = new Color[data.Length];

            source.GetData<Color>(data);

            for (int x = 0; x < source.Width; x++)
                for (int y = 0; y < source.Height; y++)
                {
                    int idx = (horizontal ? source.Width - 1 - x : x) + ((vertical ? source.Height - 1 - y : y) * source.Width);
                    flippedData[x + y * source.Width] = data[idx];
                }

            flipped.SetData<Color>(flippedData);

            return flipped;
        }

        //測試用
        private void LoadTest()
        {
            TexturesKeyEnum key = TexturesKeyEnum.TEST;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();

                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Test/test1"));
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Test/test2"));
                

                _dictionary.Add(key, texture2Ds);
            }
        }
        //選單------------------------------------------------------------------------------

        //開始遊戲按鈕
        private void LoadPlayButton() { 
             TexturesKeyEnum key = TexturesKeyEnum.MENU_PLAY_BUTTON;
             if (!_dictionary.ContainsKey(key)){
                 List<Texture2D> texture2Ds = new List<Texture2D>();
                 texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/start"));
                 _dictionary.Add(key, texture2Ds);
             }
        }

        private void LoadHowToPlayButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.MENU_HOW_TO_PLAY_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/how"));
                _dictionary.Add(key, texture2Ds);
            }
        }


        private void LoadTopScoreButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.MENU_TOP_SCORE_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/top"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        private void LoadDictionaryButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.MENU_DICTIONARY_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/dictionary"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        //選單
        private void LoadMenuBackground()
        {
            TexturesKeyEnum key = TexturesKeyEnum.MENU_BACKGROUND;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/menu_back"));
                _dictionary.Add(key, texture2Ds);
            }
        }
        //選單
        private void LoadMenuSide()
        {
            TexturesKeyEnum key = TexturesKeyEnum.MENU_SIDE;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/side"));
                _dictionary.Add(key, texture2Ds);
            }
        }
        //對話框的關閉按鈕
        private void LoadCloseDialog() {
            TexturesKeyEnum key = TexturesKeyEnum.DIALOG_CLOSE_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("close"));
                _dictionary.Add(key, texture2Ds);
            }
        }
        //最高分的對話框背景
        private void LoadTopScoreDialogBackground()
        {
            TexturesKeyEnum key = TexturesKeyEnum.TOP_SCORE_DIALOG_BACK;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("TopScore/top_score_back"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        //遊戲中元件--------------------------------------------------------

        //遊戲中的背景
        private void LoadPlayBackground() {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_BACKGROUND;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/play_back"));
                _dictionary.Add(key, texture2Ds);
            }
        
        }

        //遊戲中的煙霧
        private void LoadPlayGameSmokePicture() {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_SMOKE;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/smoke"));
                _dictionary.Add(key, texture2Ds);
            }

        }

        //遊戲中的救援失敗剩餘次數圖示
        private void LoadPlayGameLifePicture()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_LIFE;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/life"));
                _dictionary.Add(key, texture2Ds);
            }

        }
        
        //遊戲中的分數圖示
        private void LoadPlayGameScorePicture()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_SCORE;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/score"));
                _dictionary.Add(key, texture2Ds);
            }

        }
       
        //遊戲中的暫停鈕
        private void LoadPlayGamePauseButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_PAUSE_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/pause"));
                _dictionary.Add(key, texture2Ds);
            }

        }

        //遊戲中的左鍵
        private void LoadPlayGameLeftMoveButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_LEFT_MOVE_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/left_button"));
                _dictionary.Add(key, texture2Ds);
            }

        }

        //遊戲中的右鍵
        private void LoadPlayGameRightMoveButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_RIGHT_MOVE_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/right_button"));
                _dictionary.Add(key, texture2Ds);
            }

        }

        //遊戲中的消防員
        private void LoadPlayGameFireman()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_FIREMAN;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/fireman_1"));
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/fireman_2"));
                _dictionary.Add(key, texture2Ds);
            }

        }


        //遊戲中的救人網子
        private void LoadPlayNet()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PLAY_NET;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/net1"));
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Play/net2"));
                _dictionary.Add(key, texture2Ds);
            }

        }


        //暫停對話框的背景
        private void LoadPauseDialogBackground()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PAUSE_DIALOG_BACK;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Pause/pause_back"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        //暫停對話框的離開按鈕
        private void LoadPauseDialogExitButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PAUSE_EXIT;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Pause/exit"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        //暫停對話框的繼續遊戲按鈕
        private void LoadPauseDialogContinueButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.PAUSE_CONTINUE;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Pause/continue"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        //字典中元件------------------------------------------------------

        //字典背景
        private void LoadDictionaryDialogBackground()
        {
            TexturesKeyEnum key = TexturesKeyEnum.DICTIONARY_BACKGROUND;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Dictionary/dictionary_back"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        //字典左按鈕
        private void LoadDictionaryDialogLeftButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.DICTIONARY_LEFT_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Dictionary/dictionary_left"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        //字典右按鈕
        private void LoadDictionaryDialogRightButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.DICTIONARY_RIGHT_BUTOTN;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Dictionary/dictionary_right"));
                _dictionary.Add(key, texture2Ds);

            }
        }

        //字典內容
        private void LoadDictionaryDialogContentTexture()
        {
            TexturesKeyEnum key = TexturesKeyEnum.DICTIONARY_CONTENT_TEXTURE;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Dictionary/dictionary_content"));
                _dictionary.Add(key, texture2Ds);

            }
        }

        //字典人物
        private void LoadDictionaryDialogPictureTexture()
        {
            TexturesKeyEnum key = TexturesKeyEnum.DICTIONARY_PICTURE_TEXTURE;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Dictionary/dictionary_picture"));
                _dictionary.Add(key, texture2Ds);

            }
        }
    }
}
