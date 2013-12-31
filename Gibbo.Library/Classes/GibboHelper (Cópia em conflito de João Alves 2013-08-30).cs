using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

#if WINRT
using Windows.Storage;
using Windows.Storage.Streams;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Diagnostics;

#endif

#if WINDOWS
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
#endif

namespace Gibbo.Library
{
    /// <summary>
    /// A static class that provides helpful methods to the game development editor or engine.
    /// </summary>
    public static class GibboHelper
    {
#if WINDOWS
        /// <summary>
        /// Copies one entire directory to a destination
        /// </summary>
        /// <param name="sourceDirName">Source directory path</param>
        /// <param name="destDirName">Destination path</param>
        /// <param name="copySubDirs">Copy sub directories</param>
        public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, true);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
#endif
        /// <summary>
        /// Encrypt a string using the MD5 algorithm
        /// </summary>
        /// <param name="input">The original string</param>
        /// <returns>The encrypted result</returns>
        public static string EncryptMD5(string input)
        {
#if WINDOWS
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
#elif WINRT
            // TODO: 
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// Splits a string using the Camel Case method
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SplitCamelCase(string str)
        {
            string camelCase = Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");

            // remove double spaces and return:
            return Regex.Replace(camelCase, @"\s+", " ");
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        /// <summary>
        /// Returns a safe random number with syncronize lock
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float RandomNumber(float min, float max)
        {
            lock (syncLock)
            {
                return min + (float)random.NextDouble() * (max - min);
            }
        }

        /// <summary>
        /// Returns a safe random number with syncronize lock
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }

        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static String MakeRelativePath(String fromPath, String toPath)
        {
            if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (String.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath;
        }

        /// <summary>
        /// Creates a relative path from one file or folder to another exclusivly.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path (exclusion).</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static String MakeExclusiveRelativePath(String fromPath, String toPath)
        {
            if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (String.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

#if WINDOWS
            return toPath.Replace(fromPath, string.Empty).TrimStart(Path.DirectorySeparatorChar);
#elif WINRT
            return toPath.Replace(fromPath, string.Empty).TrimStart('\\');
#endif
        }

        /// <summary>
        /// Serializes an object at the location specified
        /// </summary>
        /// <param name="filename">The source filename</param>
        /// <param name="objectToSerialize">The object to serialize</param>
#if WINDOWS
        public static void SerializeObject(string filename, object objectToSerialize)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                formatter.Serialize(stream, objectToSerialize);
                byte[] serializedData = stream.ToArray();

                File.WriteAllBytes(filename, serializedData);
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Error!\nError Message: {0}", ex.Message));
            }
        }
#elif WINRT
        public async static void SerializeObject(string filename, object objectToSerialize)
        {
            await Serializer.Serialize(ApplicationData.Current.LocalFolder, filename, objectToSerialize);
        }
#endif
#if WINDOWS
        public static void SerializeObjectXML(string filename, object objectToSerialize)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                DataContractSerializer serializer = new DataContractSerializer(objectToSerialize.GetType());
                var settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "\t"
                };

                using (var writer = XmlWriter.Create(stream, settings))
                {
                    serializer.WriteObject(writer, objectToSerialize);
                }
                byte[] serializedData = stream.ToArray();
                File.WriteAllBytes(filename, serializedData);
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Error!\nError Message: {0}", ex.Message));
            }
        }
#endif
        /// <summary>
        /// Deserializes an object at the location specified
        /// </summary>
        /// <param name="filename">The source filename</param>
        /// <returns>A deserializated game object</returns>
#if WINDOWS
        public static object DeserializeObject(string filename)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(filename);

                MemoryStream stream = new MemoryStream(bytes);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                formatter.Binder = new VersionConfigToNamespaceAssemblyObjectBinder();

                return (Object)formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Error!\nError Message: {0}", ex.Message));
                return null;
            }

        }
#elif WINRT
        public static object DeserializeObject(Type type, string filename)
        {
            Task<object> t = new Task<object>(() =>
            {
                return Serializer.Deserialize(type, ApplicationData.Current.LocalFolder, filename).Result;
            });

            t.Start();
            return t.Result;
        }
#endif

#if WINDOWS
        internal sealed class VersionConfigToNamespaceAssemblyObjectBinder : SerializationBinder
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="assemblyName"></param>
            /// <param name="typeName"></param>
            /// <returns></returns>
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type typeToDeserialize = null;

                try
                {
                    string ToAssemblyName = assemblyName.Split(',')[0];

                    Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (Assembly ass in Assemblies)
                    {
                        if (ass.FullName.Split(',')[0] == ToAssemblyName)
                        {
                            typeToDeserialize = ass.GetType(typeName);

                            break;
                        }
                    }
                }

                catch (System.Exception exception)
                {
                    throw exception;
                }

                return typeToDeserialize;
            }
        }
#endif
    }

#if WINRT
    internal class Serializer
    {
        public static async Task Serialize(StorageFolder folder, string fileName, object instance)
        {
            StorageFile newFile = await folder.CreateFileAsync(fileName,
                CreationCollisionOption.ReplaceExisting);

            Stream stream = await newFile.OpenStreamForWriteAsync();
            DataContractSerializer serializer = new DataContractSerializer(instance.GetType());
                            
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.WriteObject(writer, instance);
            }

            stream.Dispose();
        }
        public static async Task<object> Deserialize(Type type, StorageFolder folder, string fileName)
        {
            StorageFile newFile = await folder.GetFileAsync(fileName);
            Stream stream = await newFile.OpenStreamForReadAsync();
            DataContractSerializer serializer = new DataContractSerializer(type);
            object item = (object)serializer.ReadObject(stream);
            stream.Dispose();
            return item;
        }
    }
#endif
}
