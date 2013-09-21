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
    public class PlayGameState  :GameState
    {
        Button pauseButton;
        Texture2D playBackground;
        Vector2 backgroundPos;
        int objIdCount;

        public PlayGameState(MainGame gMainGame) 
            :base(gMainGame)
        {
            dialogTable = new Dictionary<DialogStateEnum, GameDialog>();
        }

        public override void BeginInit()
        {
            backgroundPos = new Vector2(0, 0);

            objIdCount = 0;

            pauseButton = new Button(this, objIdCount++, 0, 0);

            //(pauseButton);
        }

        public override void LoadResource()
        {
            playBackground = base.GetTexture2DList(TexturesKeyEnum.PLAY_BACKGROUND)[0];
        }

       

        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            // 繪製主頁背景
            gameSateSpriteBatch.Draw(playBackground, backgroundPos, Color.White);
            base.Draw();
        }
    }
}
