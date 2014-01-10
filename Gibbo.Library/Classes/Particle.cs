#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Gibbo.Library
{
    internal class Particle
    {
        Vector2 origin;
        public Vector2 Position;
        Vector2 StartDirection;
        Vector2 EndDirection;
        float LifeLeft;
        float StartingLife;
        float ScaleBegin;
        float ScaleEnd;
        float rotation = 0;
        Color StartColor;
        Color EndColor;
        ParticleEmitter Parent;
        float lifePhase;

        public Particle(Vector2 Position, Vector2 StartDirection, Vector2 EndDirection, float StartingLife, float ScaleBegin, float ScaleEnd, Color StartColor, Color EndColor, ParticleEmitter Yourself)
        {
            this.Position = Position;
            this.StartDirection = StartDirection;
            this.EndDirection = EndDirection;
            this.StartingLife = StartingLife;
            this.LifeLeft = StartingLife;
            this.ScaleBegin = ScaleBegin;
            this.ScaleEnd = ScaleEnd;
            this.StartColor = StartColor;
            this.EndColor = EndColor;
            this.Parent = Yourself;
            this.origin = new Vector2(Parent.texture.Width / 2, Parent.texture.Height / 2);
        }

        public bool Update(float dt)
        {
            LifeLeft -= dt;
            if (LifeLeft <= 0)
                return false;
            lifePhase = LifeLeft / StartingLife;      // 1 means newly created 0 means dead.
            Position += MathExtension.LinearInterpolate(EndDirection, StartDirection, lifePhase) * dt;
            rotation += (Parent.RotationStrength * dt);

            //if (Parent.RestrictToBoundries)
            //{
            //    
            //    //if (this.Position.X > Parent.Transform.Position.X + Parent.CollisionModel.Width / 2
            //    //    || this.Position.X < Parent.Transform.Position.X - Parent.CollisionModel.Width / 2)
            //    //{
            //    //    EndDirection.X *= -1;
            //    //    StartDirection.X *= -1;
            //    //}
            //    //if (this.Position.Y > Parent.Transform.Position.Y + Parent.CollisionModel.Height / 2
            //    //   || this.Position.Y < Parent.Transform.Position.Y - Parent.CollisionModel.Height / 2)
            //    //{
            //    //    EndDirection.Y *= -1;
            //    //    StartDirection.Y *= -1;
            //    //}
            //}

            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            float currScale = MathExtension.LinearInterpolate(ScaleEnd, ScaleBegin, lifePhase);
            Color currCol = MathExtension.LinearInterpolate(EndColor, StartColor, lifePhase);
            spriteBatch.Draw(Parent.texture, new Vector2((int)(Position.X), (int)(Position.Y)), null, currCol, rotation, origin, currScale, SpriteEffects.None, 0);
        }
    }
}
