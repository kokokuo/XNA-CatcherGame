using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatcherGame.GameObjects;
using CatcherGame.TextureManager;

using CatcherGame.GameStates.Dialog;
namespace CatcherGame.GameStates
{
    public class HomeMenuState : GameState
    {
        Button playButton;
        Button topScoreButton;
        Button collectionDictionaryButton;
        Button howToPlayButtion;
        Texture2D menuBackground;
        TextureLayer menuSide;
        Vector2 backgroundPos;
        int objIdCount;

        public HomeMenuState(MainGame gMainGame)
            : base(gMainGame) {
                dialogTable = new Dictionary<DialogStateEnum, GameDialog>();
                dialogTable.Add(DialogStateEnum.STATE_DICTIONARY, new DictionaryDialog(this));
                dialogTable.Add(DialogStateEnum.STATE_TOPSCORE, new TopScoreDialog(this));
                dialogTable.Add(DialogStateEnum.STATE_HOW_TO_PLAY, new HowToPlayDialog(this));
        }

        public override void LoadResource(){
            playButton.LoadResource(TexturesKeyEnum.PLAY_BUTTON);
            howToPlayButtion.LoadResource(TexturesKeyEnum.HOW_TO_PLAY_BUTTON);
            collectionDictionaryButton.LoadResource(TexturesKeyEnum.DICTIONARY_BUTTON);
            topScoreButton.LoadResource(TexturesKeyEnum.TOP_SCORE_BUTTON);

            menuBackground = GetTexture2DList(TexturesKeyEnum.MENU_BACKGROUND)[0];

            menuSide.LoadResource(TexturesKeyEnum.MENU_SIDE);
            foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
            {
                dialog.Value.SetSpriteBatch(this.gameSateSpriteBatch);
                dialog.Value.LoadResource();
            }
        }

        public override void BeginInit()
        {
            backgroundPos = new Vector2(0, 0);

            objIdCount = 0;
            playButton = new Button(this, objIdCount++,0,0);
            
            topScoreButton = new Button(this, objIdCount++, 0, 0);
            collectionDictionaryButton = new Button(this, objIdCount++, 0, 0);
            howToPlayButtion = new Button(this, objIdCount++, 0, 0);
            menuSide = new TextureLayer(this, objIdCount++, 0, 0);
            AddGameObject(playButton);
            AddGameObject(topScoreButton);
            AddGameObject(collectionDictionaryButton);
            AddGameObject(howToPlayButtion);
            AddGameObject(menuSide);

            foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
            {
                dialog.Value.BeginInit();
            }

            base.isInit = true;
        }
        public override void Update()
        {
            if (!base.hasDialogShow)
            {
                if (!mainGame.GetIsTouchDataQueueEmpty())
                {
                    TouchLocation touchLocation;
                    touchLocation = mainGame.GetTouchLocation();
                    if (touchLocation.State == TouchLocationState.Released)
                    {
                        if (playButton.IsPixelClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y))
                        {
                            Debug.WriteLine("CLICK!! STATE_STORY_ANIMATION");
                            //先直接進入遊戲狀態測試
                            mainGame.SetNextGameState(GameStateEnum.STATE_PLAYGAME);  
                        }
                        else if (howToPlayButtion.IsPixelClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y))
                        {
                            Debug.WriteLine("CLICK!! STATE_HOW_TO_PLAY");
                            //base.SetNextGameDialog(DialogStateEnum.STATE_HOW_TO_PLAY);
                        }
                        else if (collectionDictionaryButton.IsPixelClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y))
                        {
                            Debug.WriteLine("CLICK!! STATE_DICTIONARY");
                            //base.SetNextGameDialog(DialogStateEnum.STATE_DICTIONARY);

                        }
                        else if (topScoreButton.IsPixelClick((int)touchLocation.Position.X, (int)touchLocation.Position.Y))
                        {
                            Debug.WriteLine("CLICK!! STATE_TOPSCORE");
                            base.SetNextGameDialog(DialogStateEnum.STATE_TOPSCORE);
                        }
                        else
                        {
                            Debug.WriteLine("MISS!!");
                            Debug.WriteLine("X:" + touchLocation.Position.X + ", Y:" + touchLocation.Position.Y);
                        }
                    }
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            // 繪製主頁背景
            gameSateSpriteBatch.Draw(menuBackground, backgroundPos, Color.White);
            //繪製遊戲元件
            base.Draw();
            
        }

       
    }
}
