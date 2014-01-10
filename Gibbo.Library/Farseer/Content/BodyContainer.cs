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
using System.Collections.Generic;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;

namespace FarseerPhysics.Content
{
    public class FixtureTemplate
    {
        public Shape Shape;
        public float Restitution;
        public float Friction;
        public string Name;
    }

    public class BodyTemplate
    {
        public List<FixtureTemplate> Fixtures;
        public float Mass;
        public BodyType BodyType;

        public BodyTemplate()
        {
            Fixtures = new List<FixtureTemplate>();
        }

        public Body Create(World world)
        {
            Body body = new Body(world);
            body.BodyType = BodyType;

            foreach (FixtureTemplate fixtureTemplate in Fixtures)
            {
                Fixture fixture = body.CreateFixture(fixtureTemplate.Shape, fixtureTemplate.Name);
                fixture.Restitution = fixtureTemplate.Restitution;
                fixture.Friction = fixtureTemplate.Friction;
            }

            if (Mass > 0f)
                body.Mass = Mass;

            return body;
        }

        public BreakableBody CreateBreakable(World world)
        {
            List<Shape> shapes = new List<Shape>();
            foreach (FixtureTemplate f in Fixtures)
            {
                shapes.Add(f.Shape);
            }

            BreakableBody body = new BreakableBody(shapes, world);
            world.AddBreakableBody(body);

            return body;
        }
    }

    public class BodyContainer : Dictionary<string, BodyTemplate> { }
}
