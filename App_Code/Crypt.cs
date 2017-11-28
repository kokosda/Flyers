using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;

namespace FlyerMe
{
    /// <summary>
    /// Summary description for Crypt
    /// </summary>
    [Serializable]
    public sealed class Cryption
    {
        //members of the Cryption 
        //algorithm type in my case it’s RijndaelManaged
        private RijndaelManaged Algorithm;
        //memory stream
        private MemoryStream memStream;
        //ICryptoTransform interface
        private ICryptoTransform EncryptorDecryptor;
        //CryptoStream
        private CryptoStream crStream;
        //Stream writer and Stream reader
        private StreamWriter strWriter;
        private StreamReader strReader;
        //internal members
        private string m_key;
        private string m_iv;
        //the Key and the Inicialization Vector
        private byte[] key;
        private byte[] iv;
        //password view
        private string pwd_str;
        private byte[] pwd_byte;
        //Constructor
        public Cryption(string key_val, string iv_val)
        {
            key = new byte[32];
            iv = new byte[32];

            int i;
            m_key = key_val;
            m_iv = iv_val;
            //key calculation, depends on first constructor parameter
            for (i = 0; i < m_key.Length; i++)
            {
                key[i] = Convert.ToByte(m_key[i]);
            }
            //IV calculation, depends on second constructor parameter
            for (i = 0; i < m_iv.Length; i++)
            {
                iv[i] = Convert.ToByte(m_iv[i]);
            }

        }
        //Encrypt method implementation
        public string Encrypt(string s)
        {
            //new instance of algorithm creation
            Algorithm = new RijndaelManaged();

            //setting algorithm bit size
            Algorithm.BlockSize = 256;
            Algorithm.KeySize = 256;

            //creating new instance of Memory stream
            memStream = new MemoryStream();

            //creating new instance of the Encryptor
            EncryptorDecryptor = Algorithm.CreateEncryptor(key, iv);

            //creating new instance of CryptoStream
            crStream = new CryptoStream(memStream, EncryptorDecryptor,
            CryptoStreamMode.Write);

            //creating new instance of Stream Writer
            strWriter = new StreamWriter(crStream);

            //cipher input string
            strWriter.Write(s);

            //clearing buffer for currnet writer and writing any 
            //buffered data to //the underlying device
            strWriter.Flush();
            crStream.FlushFinalBlock();

            //storing cipher string as byte array 
            pwd_byte = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(pwd_byte, 0, (int)pwd_byte.Length);

            //storing cipher string as Unicode string
            pwd_str = new UnicodeEncoding().GetString(pwd_byte);

            return pwd_str;
        }

        //Decrypt method implementation 
        public string Decrypt(string s)
        {
            //new instance of algorithm creation
            Algorithm = new RijndaelManaged();

            //setting algorithm bit size
            Algorithm.BlockSize = 256;
            Algorithm.KeySize = 256;

            //creating new Memory stream as stream for input string      
            MemoryStream memStream = new MemoryStream(
               new UnicodeEncoding().GetBytes(s));

            //Decryptor creating 
            ICryptoTransform EncryptorDecryptor =
                Algorithm.CreateDecryptor(key, iv);

            //setting memory stream position
            memStream.Position = 0;

            //creating new instance of Crupto stream
            CryptoStream crStream = new CryptoStream(
                memStream, EncryptorDecryptor, CryptoStreamMode.Read);

            //reading stream
            strReader = new StreamReader(crStream);

            //returnig decrypted string
            return strReader.ReadToEnd();
        }
    }
}
