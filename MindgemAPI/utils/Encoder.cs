using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MindgemAPI.utils
{
    public class Encoder
    {
        public Encoder() { }
        public Byte[] sha256_hash(String valToHash)
        {
            Byte[] result = null;
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                 result = hash.ComputeHash(enc.GetBytes(valToHash));

            }
            return result;
        }

        public Byte[] hashMac_sha512(Byte[] messageToHash, String hashKey)
        {
            Byte[] keyByte = Convert.FromBase64String(hashKey);
            using (var hmacsha512 = new HMACSHA512(keyByte))
            {
                return hmacsha512.ComputeHash(messageToHash);
            }

        }

        public String Base64Encode(string textToEncode)
        {
            Byte[] bytesText = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(bytesText);
        }

        public String Base64Decode(string textToDecode)
        {
            Byte[] byteDecoded = Convert.FromBase64String(textToDecode);
            return Convert.ToString(byteDecoded);
        }

        public String generateNonce()
        {
            Int64 unixTimestamp = DateTime.Now.Ticks;
            return Convert.ToString(unixTimestamp);
        }

    }
}