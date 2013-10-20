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
                //建立好要在Menu的Dialog
                dialogTable = new Dictionary<DialogStateEnum, GameDialog>();
                dialogTable.Add(DialogStateEnum.STATE_DICTIONARY, new DictionaryDialog(this));
                dialogTable.Add(DialogStateEnum.STATE_TOPSCORE, new TopScoreDialog(this));
                dialogTable.Add(DialogStateEnum.STATE_HOW_TO_PLAY, new HowToPlayDialog(this));
        }

        public override void LoadResource(){
            playButton.LoadResource(TexturesKeyEnum.MENU_PLAY_BUTTON);
            howToPlayButtion.LoadResource(TexturesKeyEnum.MENU_HOW_TO_PLAY_BUTTON);
            collectionDictionaryButton.LoadResource(TexturesKeyEnum.MENU_DICTIONARY_BUTTON);
            topScoreButton.LoadResource(TexturesKeyEnum.MENU_TOP_SCORE_BUTTON);

            //因為背景只有一張圖,所以這邊我們直接用index抽出圖片
            menuBackground = GetTexture2DList(TexturesKeyEnum.MENU_BACKGROUND)[0];

            menuSide.LoadResource(TexturesKeyEnum.MENU_SIDE);
            //載入對話框的圖片資源
            foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
            {
                //把繪製的元件 gameSateSpriteBatch 傳入進去,讓對話框可以透過此 gameSateSpriteBatch 來繪製
                dialog.Value.SetSpriteBatch(this.gameSateSpriteBatch);
                dialog.Value.LoadResource();
            }
        }

        public override void BeginInit()
        {
            base.x = 0; base.y = 0;
            backgroundPos = new Vector2(base.x, base.y);
            
            objIdCount = 0;
            playButton = new Button(this, objIdCount++,0,0);
            
            topScoreButton = new Button(this, objIdCount++, 0, 0);
            collectionDictionaryButton = new Button(this, objIdCount++, 0, 0);
            howToPlayButtion = new Button(this, objIdCount++, 0, 0);
            menuSide = new TextureLayer(this, objIdCount++, 0, 0);

            //這邊的加入有順序,越下面的遊戲元件在繪圖時也會被繪製在最上層
            AddGameObject(playButton);
            AddGameObject(topScoreButton);
            AddGameObject(collectionDictionaryButton);
            AddGameObject(howToPlayButtion);
            AddGameObject(menuSide);

            //對 對話框做初始化
            foreach (KeyValuePair<DialogStateEnum, GameDialog> dialog in dialogTable)
            {
                dialog.Value.BeginInit();
            }

            base.isInit = true; //設定有初始化過了
        }
        public override void Update()
        {
            //如果沒有要顯示Dialog的話,則進入選單中的按鈕判斷
            if (!base.hasDialogShow)
            {
                if(!base.IsEmptyQueue()){
                    TouchLocation touchLocation = base.GetTouchLocation();
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
                            //設定彈出GameDialog
                            base.SetPopGameDialog(DialogStateEnum.STATE_TOPSCORE);
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
