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
using System.Diagnostics;
namespace CatcherGame.GameStates.Dialog
{
    public class DictionaryDialog : GameDialog
    {
        Button leftButton;
        Button rightButton;
        //角色
        TextureLayer nicoleTexture;
        TextureLayer nicoleIntroTexture;
        TextureLayer littlegirlTexture;
        TextureLayer littlegirlIntroTexture;
        int roleStart;
        int roleEnd;
        public DictionaryDialog(GameState pCurrentState)
            : base(pCurrentState)
        {
        }
        public override void BeginInit()
        {
            roleStart = 1;
            roleEnd = 2;
            backgroundPos = new Vector2(0, 0);
            closeButton = new Button(base.currentState, base.countId++, 0, 0);
            leftButton = new Button(base.currentState, base.countId++, 0, 0);
            rightButton = new Button(base.currentState, base.countId++, 0, 0);

            nicoleTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            nicoleIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            littlegirlTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            littlegirlIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);

            stCurrent = DialogStateEnum.STATE_DICTIONARY;
            gtCurrent = DialogGameObjectEnum.DICTIONARY_NICOLE;

            AddgameObject(DialogGameObjectEnum.DICTIONARY_NICOLE, SetGameObject(nicoleTexture, nicoleIntroTexture));
            AddgameObject(DialogGameObjectEnum.DICTIONARY_LITTLEGIRL, SetGameObject(littlegirlTexture, littlegirlIntroTexture));
            AddObjectTable(DialogStateEnum.STATE_DICTIONARY, GetDialogGameObject);

            AddGameObject(closeButton);
            AddGameObject(leftButton);
            AddGameObject(rightButton);
            base.isInit = true;
        }
        public override void LoadResource()
        {
            background = currentState.GetTexture2DList(TextureManager.TexturesKeyEnum.DICTIONARY_BACKGROUND)[0];
            leftButton.LoadResource(TexturesKeyEnum.DICTIONARY_LEFT_BUTTON);
            rightButton.LoadResource(TexturesKeyEnum.DICTIONARY_RIGHT_BUTOTN);
            nicoleTexture.LoadResource(TexturesKeyEnum.DICTIONARY_NICOLE_TEXTURE);
            nicoleIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_NICOLE_INTRO_TEXTURE);
            littlegirlTexture.LoadResource(TexturesKeyEnum.DICTIONARY_LITTLEGIRL_TEXTURE);
            littlegirlIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_LITTLEGIRL_INTRO_TEXTURE);
            base.LoadResource();
        }
        public override void Update()
        {
            if (!base.currentState.IsEmptyQueue())
            {
                TouchCollection tc = base.currentState.GetCurrentFrameTouchCollection();
                bool isClickClose = false;
                if (tc.Count > 0)
                {
                    //所有當下的觸控點去判斷有無點到按鈕
                    foreach (TouchLocation touchLocation in tc)
                    {
                        if (!isClickClose)
                            isClickClose = closeButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                    }

                    //遊戲邏輯判斷
                    if (isClickClose)
                        base.CloseDialog(); //透過父類別來關閉


                    //使用觸控單次點擊方式
                    TouchLocation tL = base.currentState.GetTouchLocation();
                    if (tL.State == TouchLocationState.Released)
                    {

                        //左邊按鈕
                        if (leftButton.IsPixelClick(tL.Position.X, tL.Position.Y))
                        {
                            if ((int)gtCurrent > roleStart)
                                gtCurrent--;
                            Debug.WriteLine(gtCurrent);
                        }

                        //右邊按鈕
                        if (rightButton.IsPixelClick(tL.Position.X, tL.Position.Y))
                        {
                            if ((int)gtCurrent < roleEnd)
                                gtCurrent++;
                            Debug.WriteLine(gtCurrent);
                        }
                    }


                    //if (isRightButton)
                    //{
                    //    gtcurrent = gtcurrent + 0;
                    //    debug.writeline(gtcurrent);
                    //    isrightbutton = false;
                    //}

                    //gtCurrent = (DialogGameObjectEnum)Enum.ToObject(typeof(DialogGameObjectEnum), 2);
                    //gtCurrent = (DialogGameObjectEnum)Enum.ToObject(typeof(DialogGameObjectEnum),2);
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
