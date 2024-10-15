using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;
using System.Text;

namespace AesEncription.Helper
{
    public class AesEncriptionHelper
    {
        private readonly string key;
        private readonly string iv;


        public AesEncriptionHelper(string key, string iv)
        {
            this.key = key;
            this.iv = iv;
        }

        public void Encrypt(string inputFile, string outputFilePath)
        {
            //using (Aes aesAlg = Aes.Create())
            //{
            //    aesAlg.Key = Encoding.UTF8.GetBytes(key);
            //    aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            //    aesAlg.Padding = PaddingMode.PKCS7;

            //    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            //    using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            //    using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            //    using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
            //    {
            //        inputFileStream.CopyTo(cryptoStream);
            //    }
            //}

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }
            }
            //using (Aes aesAlg = Aes.Create())
            //{
            //    aesAlg.Key = Encoding.UTF8.GetBytes(key);
            //    aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            //    aesAlg.Padding = PaddingMode.PKCS7;
            //    //Encrypt Dokumen
            //    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            //    using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            //    using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            //    using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor,CryptoStreamMode.Write))
            //    {
            //        inputFileStream.CopyTo(cryptoStream);
            //    }

            //    //ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            //    //using (MemoryStream msEncrypt = new MemoryStream())
            //    //{
            //    //    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            //    //    {
            //    //        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            //    //        {
            //    //            swEncrypt.Write(plaintext);
            //    //        }
            //    //        return msEncrypt.ToArray();
            //    //    }
            //    //}

            //}
        }

        public void Decrypt (string inputFilePath, string ouputFilePath)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read)) 
                using (FileStream outputFileSteam = new FileStream(ouputFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputFileSteam);
                }
                /*using (MemoryStream msDecrypt = new MemoryStream(chiperText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
                */
            }
        }
    }
}
