using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gibbo.Editor.Model
{
    internal static class EncryptionHelper
    {

        #region Encryption method of image encryption

        /// <summary>
        /// Image encryption
        /// </summary>
        /// <param name="filePath">The source file</param>
        /// <param name="savePath">Save the file name</param>
        /// <param name="keyStr">Key</param>

        public static void EncryptFile(string filePath, string savePath, string keyStr)
        {

            //Through the DES encryption

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Open the file by flow

            FileStream fs = File.OpenRead(filePath);

            //Get file binary characters

            byte[] inputByteArray = new byte[fs.Length];

            //Read the stream file

            fs.Read(inputByteArray, 0, (int)fs.Length);

            //Close the stream

            fs.Close();

            //Obtain the encrypted string of binary characters

            byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);

            //Calculation of the specified byte group designated area hash value

            SHA1 ha = new SHA1Managed();

            byte[] hb = ha.ComputeHash(keyByteArray);

            //The encryption key array

            byte[] sKey = new byte[8];

            //Encryption variables

            byte[] sIV = new byte[8];

            for (int i = 0; i < 8; i++)

                sKey[i] = hb[i];

            for (int i = 8; i < 16; i++)

                sIV[i - 8] = hb[i];

            //Access to the encryption key
            des.Key = sKey;

            //Set encryption initialization vector
            des.IV = sIV;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);

            cs.FlushFinalBlock();

            fs = File.OpenWrite(savePath);

            foreach (byte b in ms.ToArray())
            {

                fs.WriteByte(b);

            }

            fs.Close();

            cs.Close();

            ms.Close();

        }

        #endregion
    }
}
