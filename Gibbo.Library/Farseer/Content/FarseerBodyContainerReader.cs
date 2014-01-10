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
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Content;

namespace FarseerPhysics.Content
{
    public class FarseerBodyContainerReader : ContentTypeReader<BodyContainer>
    {
        protected override BodyContainer Read(ContentReader input, BodyContainer existingInstance)
        {
            BodyContainer bodies = existingInstance ?? new BodyContainer();

            int count = input.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string name = input.ReadString();
                BodyTemplate body = new BodyTemplate
                    {
                    Mass = input.ReadSingle(),
                    BodyType = (BodyType)input.ReadInt32()
                };
                int fixtureCount = input.ReadInt32();
                for (int j = 0; j < fixtureCount; j++)
                {
                    FixtureTemplate fixture = new FixtureTemplate
                        {
                        Name = input.ReadString(),
                        Restitution = input.ReadSingle(),
                        Friction = input.ReadSingle()
                    };
                    ShapeType type = (ShapeType)input.ReadInt32();
                    switch (type)
                    {
                        case ShapeType.Circle:
                            {
                                float density = input.ReadSingle();
                                float radius = input.ReadSingle();
                                CircleShape circle = new CircleShape(radius, density);
                                circle.Position = input.ReadVector2();
                                fixture.Shape = circle;
                            } break;
                        case ShapeType.Polygon:
                            {
                                Vertices verts = new Vertices(Settings.MaxPolygonVertices);
                                float density = input.ReadSingle();
                                int verticeCount = input.ReadInt32();
                                for (int k = 0; k < verticeCount; k++)
                                {
                                    verts.Add(input.ReadVector2());
                                }
                                PolygonShape poly = new PolygonShape(verts, density);
                                poly.MassData.Centroid = input.ReadVector2();
                                fixture.Shape = poly;
                            } break;
                        case ShapeType.Edge:
                            {
                                EdgeShape edge = new EdgeShape(input.ReadVector2(), input.ReadVector2());
                                edge.HasVertex0 = input.ReadBoolean();
                                if (edge.HasVertex0)
                                {
                                    edge.Vertex0 = input.ReadVector2();
                                }
                                edge.HasVertex3 = input.ReadBoolean();
                                if (edge.HasVertex3)
                                {
                                    edge.Vertex3 = input.ReadVector2();
                                }
                                fixture.Shape = edge;
                            } break;
                        case ShapeType.Chain:
                            {
                                Vertices verts = new Vertices();
                                int verticeCount = input.ReadInt32();
                                for (int k = 0; k < verticeCount; k++)
                                {
                                    verts.Add(input.ReadVector2());
                                }
                                fixture.Shape = new ChainShape(verts);
                            } break;
                    }
                    body.Fixtures.Add(fixture);
                }
                bodies[name] = body;
            }
            return bodies;
        }
    }
}