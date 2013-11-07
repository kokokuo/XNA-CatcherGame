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
        TextureLayer fatdancerTexture;
        TextureLayer fatdancerIntroTexture;
        TextureLayer flyoldladyTexture;
        TextureLayer flyoldladyIntroTexture;
        TextureLayer manstubbleTexture;
        TextureLayer manstubbleIntroTexture;
        TextureLayer naughtyboyTexture;
        TextureLayer naughtyboyIntroTexture;
        TextureLayer oldmanTexture;
        TextureLayer oldmanIntroTexture;
        TextureLayer roxanneTexture;
        TextureLayer roxanneIntroTexture;

        int roleStart;
        int roleEnd;
        public DictionaryDialog(GameState pCurrentState)
            : base(pCurrentState)
        {
           
        }
        public override void BeginInit()
        {
            roleStart = 1;
            roleEnd = 8;
            backgroundPos = new Vector2(0, 0);
            closeButton = new Button(base.currentState, base.countId++, 0, 0);
            leftButton = new Button(base.currentState, base.countId++, 0, 0);
            rightButton = new Button(base.currentState, base.countId++, 0, 0);

            nicoleTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            nicoleIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            littlegirlTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            littlegirlIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            fatdancerTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            fatdancerIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            flyoldladyTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            flyoldladyIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            manstubbleTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            manstubbleIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            naughtyboyTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            naughtyboyIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            oldmanTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            oldmanIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            roxanneTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);
            roxanneIntroTexture = new TextureLayer(base.currentState, base.countId++, 0, 0);

            stCurrent = DialogStateEnum.STATE_DICTIONARY;
            gtCurrent = DialogGameObjectEnum.DICTIONARY_NICOLE;

            
            AddgameObject(DialogGameObjectEnum.DICTIONARY_NICOLE,new GameObject[]{nicoleTexture,nicoleIntroTexture,rightButton});
            AddgameObject(DialogGameObjectEnum.DICTIONARY_LITTLEGIRL,new GameObject[]{littlegirlTexture,littlegirlIntroTexture,leftButton,rightButton});
            AddgameObject(DialogGameObjectEnum.DICTIONARY_FATDANCER, new GameObject[] { fatdancerTexture, fatdancerIntroTexture ,leftButton,rightButton});
            AddgameObject(DialogGameObjectEnum.DICTIONARY_FLYOLDLADY, new GameObject[] { flyoldladyTexture, flyoldladyIntroTexture ,leftButton,rightButton});
            AddgameObject(DialogGameObjectEnum.DICTIONARY_MANSTUBBLE, new GameObject[] { manstubbleTexture, manstubbleIntroTexture,leftButton,rightButton });
            AddgameObject(DialogGameObjectEnum.DICTIONARY_NAUGHTYBOY, new GameObject[] { naughtyboyTexture, naughtyboyIntroTexture,leftButton,rightButton });
            AddgameObject(DialogGameObjectEnum.DICTIONARY_OLDMAN, new GameObject[] { oldmanTexture, oldmanIntroTexture,leftButton,rightButton });
            AddgameObject(DialogGameObjectEnum.DICTIONARY_ROXANNE, new GameObject[] { roxanneTexture, roxanneIntroTexture,leftButton });

            AddObjectTable(DialogStateEnum.STATE_DICTIONARY, GetDialogGameObject);

            AddGameObject(closeButton);
            base.isInit = true;
        }
        public override void LoadResource()
        {
            background = currentState.GetTexture2DList(TextureManager.TexturesKeyEnum.DICTIONARY_BACKGROUND)[0];
            leftButton.LoadResource(TexturesKeyEnum.DIALOG_LEFT_BUTTON);
            rightButton.LoadResource(TexturesKeyEnum.DIALOG_RIGHT_BUTTON);
            nicoleTexture.LoadResource(TexturesKeyEnum.DICTIONARY_NICOLE_TEXTURE);
            nicoleIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_NICOLE_INTRO_TEXTURE);
            littlegirlTexture.LoadResource(TexturesKeyEnum.DICTIONARY_LITTLEGIRL_TEXTURE);
            littlegirlIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_LITTLEGIRL_INTRO_TEXTURE);

            fatdancerTexture.LoadResource(TexturesKeyEnum.DICTIONARY_FATDANCER_TEXTURE);
            fatdancerIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_FATDANCER_INTRO_TEXTURE);
            flyoldladyTexture.LoadResource(TexturesKeyEnum.DICTIONARY_FLYOLDLADY_TEXTURE);
            flyoldladyIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_FLYOLDLADY_INTRO_TEXTURE);
            manstubbleTexture.LoadResource(TexturesKeyEnum.DICTIONARY_MANSTUBBLE_TEXTURE);
            manstubbleIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_MANSTUBBLE_INTRO_TEXTURE);
            naughtyboyTexture.LoadResource(TexturesKeyEnum.DICTIONARY_NAUGHTYBOY_TEXTURE);
            naughtyboyIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_NAUGHTYBOY_INTRO_TEXTURE);
            oldmanTexture.LoadResource(TexturesKeyEnum.DICTIONARY_OLDMAN_TEXTURE);
            oldmanIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_OLDMAN_INTRO_TEXTURE);
            roxanneTexture.LoadResource(TexturesKeyEnum.DICTIONARY_ROXANNE_TEXTURE);
            roxanneIntroTexture.LoadResource(TexturesKeyEnum.DICTIONARY_ROXANNE_INTRO_TEXTURE);


            base.LoadResource();
        }
        public override void Update()
        {
            if (!base.currentState.IsEmptyQueue())
            {
                stCurrent = DialogStateEnum.STATE_DICTIONARY;
                if(gtCurrent==DialogGameObjectEnum.EMPTY)
                gtCurrent = DialogGameObjectEnum.DICTIONARY_NICOLE;

                TouchCollection tc = base.currentState.GetCurrentFrameTouchCollection();
                bool isClickClose = false;
                bool isLeftButton = false;
                bool isRightButton = false;
                if (tc.Count > 0)
                {
                    //所有當下的觸控點去判斷有無點到按鈕
                    foreach (TouchLocation touchLocation in tc)
                    {
                        if (!isClickClose)
                            isClickClose = closeButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                        if (!isLeftButton)
                            isLeftButton = leftButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                        if (!isRightButton)
                            isRightButton = rightButton.IsPixelClick(touchLocation.Position.X, touchLocation.Position.Y);
                    }

                    //遊戲邏輯判斷
                    if (isClickClose)
                        base.CloseDialog(); //透過父類別來關閉

                    //按鈕方式2
                    //使用觸控單次點擊方式
                    TouchLocation tL = base.currentState.GetTouchLocation();
                    if (tL.State == TouchLocationState.Released)
                    {

                        //左邊按鈕
                        if (leftButton.IsPixelClick(tL.Position.X, tL.Position.Y))
                        {
                            if ((int)gtCurrent > roleStart)
                                gtCurrent--;
                        }

                        //右邊按鈕
                        if (rightButton.IsPixelClick(tL.Position.X, tL.Position.Y))
                        {
                            if ((int)gtCurrent < roleEnd)
                                gtCurrent++;
                        }
                    }

                    //清除TouchQueue裡的觸控點，因為避免Dequeue時候並不在Dialog中，因此要清除TouchQueue。
                    base.currentState.ClearTouchQueue();
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
