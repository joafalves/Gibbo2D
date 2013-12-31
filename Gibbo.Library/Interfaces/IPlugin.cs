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
