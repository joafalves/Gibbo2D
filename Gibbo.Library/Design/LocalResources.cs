using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gibbo.Library
{
#if WINDOWS
    public static class LocalResources
    {
        private static readonly Type _ThisType = typeof(LocalResources);
        private static readonly ComponentResourceKey _FileBrowserEditorKey = new ComponentResourceKey(_ThisType, "FileBrowserEditorTemplate");
        private static readonly ComponentResourceKey _PercentEditorKey = new ComponentResourceKey(_ThisType, "PercentEditorTemplate");
        private static readonly ComponentResourceKey _XmlLanguageEditorKey = new ComponentResourceKey(_ThisType, "XmlLanguageEditorTemplate");

        public static ComponentResourceKey FileBrowserEditorKey
        {
            get { return _FileBrowserEditorKey; }
        }

        public static ComponentResourceKey PercentEditorKey
        {
            get { return _PercentEditorKey; }
        }

        public static ComponentResourceKey XmlLanguageEditorKey
        {
            get { return _XmlLanguageEditorKey; }
        }
    }
#endif
}
