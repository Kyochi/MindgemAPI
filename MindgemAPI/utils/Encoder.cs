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
        public String sha256_hash(String valToHash)
        {
            StringBuilder hashBuildder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(valToHash));

                foreach (Byte b in result)
                {
                    hashBuildder.Append(b.ToString("x2"));
                }
            }
            
            return hashBuildder.ToString();
        }

        public String hashMac_sha512(String messageToHash, String hashKey)
        {
            Encoding formatEncoding = Encoding.UTF8;
            var keyByte = formatEncoding.GetBytes(hashKey);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                hmacsha256.ComputeHash(formatEncoding.GetBytes(messageToHash));
                return Convert.ToString(hmacsha256.Hash);
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
            Int64 unixTimestamp = (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return Convert.ToString(unixTimestamp);
        }

    }
}