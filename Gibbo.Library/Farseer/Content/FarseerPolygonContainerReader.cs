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
using FarseerPhysics.Common;
using Microsoft.Xna.Framework.Content;

namespace FarseerPhysics.Content
{
    public class FarseerPolygonContainerReader : ContentTypeReader<PolygonContainer>
    {
        protected override PolygonContainer Read(ContentReader input, PolygonContainer existingInstance)
        {
            PolygonContainer paths = existingInstance ?? new PolygonContainer();

            int count = input.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string name = input.ReadString();
                bool closed = input.ReadBoolean();
                int vertsCount = input.ReadInt32();
                Vertices verts = new Vertices();
                for (int j = 0; j < vertsCount; j++)
                {
                    verts.Add(input.ReadVector2());
                }
                paths[name] = new Polygon(verts, closed);
            }

            return paths;
        }
    }
}
