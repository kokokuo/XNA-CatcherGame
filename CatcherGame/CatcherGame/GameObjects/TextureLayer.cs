using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

using CatcherGame.GameStates;
using CatcherGame.TextureManager;
using CatcherGame.Sprite;
namespace CatcherGame.GameObjects
{
    public class TextureLayer : GameObject
    {
        AnimationSprite layer;
        public TextureLayer(GameState currentGameState, int objId, float x, float y)
            : base(currentGameState, objId, x, y)
        {
            Init();
        }

        protected override void Init()
        {
            layer = new AnimationSprite(new Vector2(base.x,base.y), 300);
        }

        public override void LoadResource(TexturesKeyEnum key)
        {
            layer.SetTexture2DList(base.gameState.GetTexture2DList(key));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            layer.Draw(spriteBatch);
        }

        public override void Update() {
            layer.UpdateFrame(base.gameState.GetTimeSpan());
        }
    }
}
