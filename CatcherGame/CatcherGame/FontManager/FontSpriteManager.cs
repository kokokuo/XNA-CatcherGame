using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using CatcherGame;
using Microsoft.Xna.Framework;
namespace CatcherGame.FontManager
{
    public class SpriteFontManager
    {
        private MainGame mainGame;
        private Dictionary<SpriteFontKeyEnum, SpriteFont> _dictionary;

        public SpriteFontManager(MainGame mainGame)
        {
            Debug.WriteLine("SpriteFontManager construct ...");
            this.mainGame = mainGame;
            _dictionary = new Dictionary<SpriteFontKeyEnum, SpriteFont>();
            //載入文字資源
            LoadTopScoreFont();
            LoadPlaySavedPeopleNumberFont();
            LoadPlayLostPeopleNumberFont();
        }

        /// <summary>
        /// 取得對應的Key的文字資源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SpriteFont GetSpriteFontFromKey(SpriteFontKeyEnum key) {

            return _dictionary[key];
        }

        private void LoadTopScoreFont(){
            SpriteFontKeyEnum key = SpriteFontKeyEnum.TOP_SCORE_FONT;
             if (!_dictionary.ContainsKey(key)){
                 _dictionary.Add(key, mainGame.Content.Load<SpriteFont>("TopScoreFont"));
             }
        }


        private void LoadPlaySavedPeopleNumberFont()
        {
            SpriteFontKeyEnum key = SpriteFontKeyEnum.PLAY_SAVED_PEOPLE_FONT;
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, mainGame.Content.Load<SpriteFont>("SavedPeopleFont"));
            }
        }

        private void LoadPlayLostPeopleNumberFont()
        {
            SpriteFontKeyEnum key = SpriteFontKeyEnum.PLAT_LOST_PEOPLE_FONT;
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, mainGame.Content.Load<SpriteFont>("LostPeopleFont"));
            }
        }
    }
}
