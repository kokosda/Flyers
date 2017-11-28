using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for clsEncryptDecrypt
/// </summary>
public class clsEncryptDecrypt
{
    public string Encrypter(string realstr)
    {
        string original, encrypted, password;
        TripleDESCryptoServiceProvider des;
        MD5CryptoServiceProvider hashmd5;
        byte[] pwdhash, buff;

        //create a secret password. the password is used to encrypt
        //and decrypt strings. Without the password, the encrypted
        //string cannot be decrypted and is just garbage. You must
        //use the same password to decrypt an encrypted string as the
        //string was originally encrypted with.
        password = "password";

        //create a string to encrypt
        //original = "hi, my name is bill but you wouldn't know me";
        original = realstr;

        //generate an MD5 hash from the password. 
        //a hash is a one way encryption meaning once you generate
        //the hash, you cant derive the password back from it.
        hashmd5 = new MD5CryptoServiceProvider();
        pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
        hashmd5 = null;

        //implement DES3 encryption
        des = new TripleDESCryptoServiceProvider();

        //the key is the secret password hash.
        des.Key = pwdhash;

        //the mode is the block cipher mode which is basically the
        //details of how the encryption will work. There are several
        //kinds of ciphers available in DES3 and they all have benefits
        //and drawbacks. Here the Electronic Codebook cipher is used
        //which means that a given bit of text is always encrypted
        //exactly the same when the same password is used.
        des.Mode = CipherMode.ECB; //CBC, CFB


        //----- encrypt an un-encrypted string ------------
        //the original string, which needs encrypted, must be in byte
        //array form to work with the des3 class. everything will because
        //most encryption works at the byte level so you'll find that
        //the class takes in byte arrays and returns byte arrays and
        //you'll be converting those arrays to strings.
        buff = ASCIIEncoding.ASCII.GetBytes(original);

        //encrypt the byte buffer representation of the original string
        //and base64 encode the encrypted string. the reason the encrypted
        //bytes are being base64 encoded as a string is the encryption will
        //have created some weird characters in there. Base64 encoding
        //provides a platform independent view of the encrypted string 
        //and can be sent as a plain text string to wherever.
        encrypted = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buff, 0, buff.Length));


        return encrypted;

    }
    public string Decrypter(string encrypted)
    {
        string decrypted, password;
        TripleDESCryptoServiceProvider des;
        MD5CryptoServiceProvider hashmd5;
        byte[] pwdhash, buff;

        //create a secret password. the password is used to encrypt
        //and decrypt strings. Without the password, the encrypted
        //string cannot be decrypted and is just garbage. You must
        //use the same password to decrypt an encrypted string as the
        //string was originally encrypted with.
        password = "password";

        //generate an MD5 hash from the password. 
        //a hash is a one way encryption meaning once you generate
        //the hash, you cant derive the password back from it.
        hashmd5 = new MD5CryptoServiceProvider();
        pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
        hashmd5 = null;

        //implement DES3 encryption
        des = new TripleDESCryptoServiceProvider();

        //the key is the secret password hash.
        des.Key = pwdhash;

        //the mode is the block cipher mode which is basically the
        //details of how the encryption will work. There are several
        //kinds of ciphers available in DES3 and they all have benefits
        //and drawbacks. Here the Electronic Codebook cipher is used
        //which means that a given bit of text is always encrypted
        //exactly the same when the same password is used.
        des.Mode = CipherMode.ECB; //CBC, CFB


        //----- decrypt an encrypted string ------------
        //whenever you decrypt a string, you must do everything you
        //did to encrypt the string, but in reverse order. To encrypt,
        //first a normal string was des3 encrypted into a byte array
        //and then base64 encoded for reliable transmission. So, to 
        //decrypt this string, first the base64 encoded string must be 
        //decoded so that just the encrypted byte array remains.
        buff = Convert.FromBase64String(encrypted);

        //decrypt DES 3 encrypted byte buffer and return ASCII string
        decrypted = ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buff, 0, buff.Length));

        return decrypted;
    }
}
