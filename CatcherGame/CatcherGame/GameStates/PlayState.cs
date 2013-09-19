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
    public class PlayState  :GameState
    {
        Button pauseButton;
        Texture2D menuBackground;
        Vector2 backgroundPos;
        int objIdCount;

        public PlayState(MainGame gMainGame) 
            :base(gMainGame)
        {
            dialogTable = new Dictionary<DialogStateEnum, GameDialog>();
        }

        public override void BeginInit()
        {
            backgroundPos = new Vector2(0, 0);

            objIdCount = 0;

            pauseButton = new Button(this, objIdCount++, 0, 0);

            AddGameObject(pauseButton);
        }

        public override void LoadResource()
        {
            throw new NotImplementedException();
        }

       

        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
