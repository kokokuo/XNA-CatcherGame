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

        private void LoadPlayButton() { 
             TexturesKeyEnum key = TexturesKeyEnum.PLAY_BUTTON;
             if (!_dictionary.ContainsKey(key)){
                 List<Texture2D> texture2Ds = new List<Texture2D>();
                 texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/start"));
                 _dictionary.Add(key, texture2Ds);
             }
        }

        private void LoadHowToPlayButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.HOW_TO_PLAY_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/how"));
                _dictionary.Add(key, texture2Ds);
            }
        }


        private void LoadTopScoreButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.TOP_SCORE_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("Menu/top"));
                _dictionary.Add(key, texture2Ds);
            }
        }

        private void LoadDictionaryButton()
        {
            TexturesKeyEnum key = TexturesKeyEnum.DICTIONARY_BUTTON;
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
        private void LoadCloseDialog() {
            TexturesKeyEnum key = TexturesKeyEnum.DIALOG_CLOSE_BUTTON;
            if (!_dictionary.ContainsKey(key))
            {
                List<Texture2D> texture2Ds = new List<Texture2D>();
                texture2Ds.Add(mainGame.Content.Load<Texture2D>("close"));
                _dictionary.Add(key, texture2Ds);
            }
        }

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
    }
}
