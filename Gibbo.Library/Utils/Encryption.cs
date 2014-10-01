using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gibbo.Library
{
    public static class Encryption
    {
        //  Call this function to remove the key from memory after use for security
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        // Function to Generate a 64 bits Key.
        public static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

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

        #region Decryption method image decryption;

        /// <summary>
        /// Image decryption
        /// </summary>
        /// <param name="filePath">The source file</param>
        /// <param name="savePath">Save the file</param>
        /// <param name="keyStr">Key</param>

        public static MemoryStream DecryptFile(string filePath, string savePath, string keyStr)
        {

            //Through the des decryption

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Read through the documents flow

            FileStream fs = File.OpenRead(filePath);

            //Get file binary characters

            byte[] inputByteArray = new byte[fs.Length];

            //Reading the stream file

            fs.Read(inputByteArray, 0, (int)fs.Length);

            //Close the stream

            fs.Close();

            //A key array

            byte[] keyByteArray = Encoding.Default.GetBytes(keyStr);

            //Define hash variables

            SHA1 ha = new SHA1Managed();

            //Calculation of the specified byte group designated area hash value

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

            //Encryption variables

            des.IV = sIV;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);

            cs.FlushFinalBlock();

            //fs = File.OpenWrite(savePath);

            //foreach (byte b in ms.ToArray())
            //{

            //    fs.WriteByte(b);

            //}

            //fs.Close();

            cs.Close();

            ms.Close();

            return ms;

        }

        #endregion

        #region Encrypt & Decrypt Private Key

        public static int EncryptDataToStreamWithoutEntropy(byte[] Buffer, Stream S)
        {
            if (Buffer.Length <= 0)
                throw new ArgumentException("Buffer");
            if (Buffer == null)
                throw new ArgumentNullException("Buffer");

            if (S == null)
                throw new ArgumentNullException("S");

            int length = 0;

            // Encrypt the data in memory. The result is stored in the same same array as the original data.
            byte[] encrptedData = ProtectedData.Protect(Buffer, null, DataProtectionScope.CurrentUser);

            // Write the encrypted data to a stream.
            if (S.CanWrite && encrptedData != null)
            {
                S.Write(encrptedData, 0, encrptedData.Length);

                length = encrptedData.Length;
            }

            // Return the length that was written to the stream. 
            return length;

        }

        public static byte[] DecryptDataFromStreamWithoutEntropy(DataProtectionScope Scope, Stream S, int Length)
        {
            if (S == null)
                throw new ArgumentNullException("S");
            if (Length <= 0)
                throw new ArgumentException("Length");

            byte[] inBuffer = new byte[Length];
            byte[] outBuffer;

            // Read the encrypted data from a stream.
            if (S.CanRead)
            {
                S.Read(inBuffer, 0, Length);

                outBuffer = ProtectedData.Unprotect(inBuffer, null, Scope);
            }
            else
            {
                throw new IOException("Could not read the stream.");
            }

            // Return the length that was written to the stream. 
            return outBuffer;

        }


        #endregion
    }
}
