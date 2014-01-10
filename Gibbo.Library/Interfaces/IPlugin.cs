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
namespace Gibbo.Library
{
    /// <summary>
    /// The plugin interface
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// The name of the plugin.
        /// This is the name that will appear on the plugins menu
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The action that will be performed when clicking on the plugin menu item (editor)
        /// </summary>
        /// <param name="pluginContext">the context of the plugin</param>
        void PerformAction(PluginContext pluginContext);

        /// <summary>
        /// The action that will be performed when the editor starts
        /// </summary>
        void Initialize();
    }

    /// <summary>
    /// Plugin Context
    /// </summary>
    public class PluginContext
    {
        // OPTIONAL: add properties
    }
}
