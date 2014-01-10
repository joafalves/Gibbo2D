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
using FarseerPhysics.Collision;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics.Joints;

namespace FarseerPhysics.Dynamics
{
    /// <summary>
    /// This delegate is called when a contact is deleted
    /// </summary>
    public delegate void EndContactDelegate(Contact contact);

    /// <summary>
    /// This delegate is called when a contact is created
    /// </summary>
    public delegate bool BeginContactDelegate(Contact contact);

    public delegate void PreSolveDelegate(Contact contact, ref Manifold oldManifold);

    public delegate void PostSolveDelegate(Contact contact, ContactVelocityConstraint impulse);

    public delegate void FixtureDelegate(Fixture fixture);

    public delegate void JointDelegate(Joint joint);

    public delegate void BodyDelegate(Body body);

    public delegate void ControllerDelegate(Controller controller);

    public delegate bool CollisionFilterDelegate(Fixture fixtureA, Fixture fixtureB);

    public delegate void BroadphaseDelegate(ref FixtureProxy proxyA, ref FixtureProxy proxyB);

    public delegate bool BeforeCollisionEventHandler(Fixture fixtureA, Fixture fixtureB);

    public delegate bool OnCollisionEventHandler(Fixture fixtureA, Fixture fixtureB, Contact contact);

    public delegate void AfterCollisionEventHandler(Fixture fixtureA, Fixture fixtureB, Contact contact, ContactVelocityConstraint impulse);

    public delegate void OnSeparationEventHandler(Fixture fixtureA, Fixture fixtureB);
}