using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionAssignment
{
    public class EncryptionService
    {
        public string Encrypt(string input)
        {
            // Placeholder for actual encryption logic
            return $"Encrypted({input})";
        }

        public string Decrypt(string input)
        {
            // Placeholder for actual decryption logic
            return input.Replace("Encrypted(", "").Replace(")", "");
        }
    }

}
