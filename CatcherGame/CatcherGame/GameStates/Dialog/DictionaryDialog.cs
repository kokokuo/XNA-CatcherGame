using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CatcherGame.GameStates;
using CatcherGame.GameObjects;
using CatcherGame.TextureManager;
namespace CatcherGame.GameStates.Dialog
{
    public class DictionaryDialog : GameDialog
    {
        Button leftButton;
        Button rightButton;
        TextureLayer contentTexture;
        TextureLayer pictureTexture;
        public DictionaryDialog(GameState pCurrentState)
            : base(pCurrentState)
        {
        }
        public override void BeginInit()
        {
            backgroundPos = new Vector2(0, 0);
            closeButton = new Button(base.currentState, base.countId++, 0, 0);
            leftButton = new Button(base.currentState, base.countId++, 0, 0);
            rightButton = new Button(base.currentState, base.countId++, 0, 0);
            contentTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            pictureTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);

            AddGameObject(closeButton);
            AddGameObject(leftButton);
            AddGameObject(rightButton);
            AddGameObject(contentTexture);
            AddGameObject(pictureTexture);

            base.isInit = true;
        }
        public override void LoadResource()
        {
            background = currentState.GetTexture2DList(TextureManager.TexturesKeyEnum.DICTIONARY_BACKGROUND)[0];
            leftButton.LoadResource(TexturesKeyEnum.DICTIONARY_LEFT_BUTTON);
            rightButton.LoadResource(TexturesKeyEnum.DICTIONARY_RIGHT_BUTOTN);
            contentTexture.LoadResource(TexturesKeyEnum.DICTIONARY_CONTENT_TEXTURE);
            pictureTexture.LoadResource(TexturesKeyEnum.DICTIONARY_PICTURE_TEXTURE);
            base.LoadResource();
        }
        public override void Update()
        {
            if (!base.currentState.IsEmptyQueue())
            {
                TouchLocation touchLocation = base.currentState.GetTouchLocation();
                if (touchLocation.State == TouchLocationState.Released)
                {
                    //若有觸及到關閉按鈕
                    if (closeButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y))
                    {
                        base.CloseDialog(); //透過父類別來關閉
                    }
                    //右邊按鈕
                    if (rightButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y))
                    {

                    }
                }

            }

            base.Update(); //更新遊戲元件
        }
        public override void Draw()
        {

            gameSateSpriteBatch.Draw(background, backgroundPos, Color.White);
            base.Draw(); //繪製遊戲元件
        }

    }
}
