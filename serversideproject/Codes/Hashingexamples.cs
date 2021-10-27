using System.Security.Cryptography;
using System.Text;
using BCrypt;

namespace serversideproject.Codes;

public class Hashingexamples : IHashingexamples
{
    public string MD5Hash(string valueToHash) 
    {
        byte[] valueAsBytes = ASCIIEncoding.ASCII.GetBytes(valueToHash);
        byte[] hashedMD5 = MD5.HashData(valueAsBytes);
        string hashedValueAsString = Convert.ToBase64String(hashedMD5);
        return hashedValueAsString; 
    }

    public string Bcrypthash(string valueToHash)
    {
        string hashed = BCrypt.Net.BCrypt.HashPassword(valueToHash, BCrypt.Net.SaltRevision.Revision2Y);
        return hashed;
    }
}

