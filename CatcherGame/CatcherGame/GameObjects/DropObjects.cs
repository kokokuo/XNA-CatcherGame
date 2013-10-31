using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using CatcherGame;
using CatcherGame.Sprite;
using CatcherGame.GameStates;

using CatcherGame.TextureManager;
namespace CatcherGame.GameObjects
{
    public abstract class DropObjects : GameObject
    {
        protected float fallingSpeed, fallingWave;
        protected float fallingNextYPos; //接下來會掉落的Y座標
        protected float fallingNextXPos; //接下來會擺動的x座標
        protected bool isFalling, isCaught,isTouch;
        public DropObjects(GameState currentGameState, int id, float x, float y, float fallingSpeed, float fallingWave)
            : base(currentGameState, id, x, y)
        {
            this.fallingSpeed = fallingSpeed;
            this.fallingWave = fallingWave;
            this.isFalling = true;
            this.isCaught = this.isTouch = false;

        }

        public abstract void SetCaught();

        public void SetTouched() {
            this.isTouch = true;
        }

        public bool GetIsCaught() {
            return this.isCaught;
        }

        public bool GetIsTouch()
        {
            return this.isTouch;
        }
        public bool GetIsFalling(){
            return this.isFalling;
        }
    }
}
