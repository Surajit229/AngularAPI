using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Rota.Utility
{
    public static class PasswordHashing
    {
        //  Encrypt Key for Encryption & Decryption
        private const string EncryptKey = "ಲೋ�ꕝꔁꅴⴲꒌ⬄ⱙⳂ館⫸Ⰹ✈∰ꕢ㊧ᨁ❄㍻ꡁ𝕬￼ⶀힰഅ㐅∰𐒝㏵⣿𪾀〒ⵥꩴ";

        //  Init Vector for Encryption & Decryption
        private const string InitVector = "BankFileGenerate";

        //  Key Size for Encryption & Decryption
        private const int KeySize = 256;

        //  Encrypt method
        public static string Encrypt(this string Text)
        {
            try
            {
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(InitVector);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
                PasswordDeriveBytes password = new PasswordDeriveBytes(EncryptKey, null);
                byte[] keyBytes = password.GetBytes(KeySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] Encrypted = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return Convert.ToBase64String(Encrypted);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        //  Decrypt method
        public static string Decrypt(this string EncryptedText)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
                byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(EncryptKey, null);
                byte[] keyBytes = password.GetBytes(KeySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[DeEncryptedText.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
