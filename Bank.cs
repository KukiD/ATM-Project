using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionAssignment
{
    public class Bank
    {
        private Dictionary<string, (string EncryptedPin, decimal Balance)> _accounts =
            new Dictionary<string, (string, decimal)>
            {
            { "12345", ("Encrypted(1234)", 500.00m) },
            { "67890", ("Encrypted(5678)", 1000.00m) }
            };

        private EncryptionService _encryptionService = new EncryptionService();

        public bool VerifyCredentials(string cardNumber, string encryptedPin)
        {
            return _accounts.ContainsKey(cardNumber) &&
                   _accounts[cardNumber].EncryptedPin == encryptedPin;
        }

        public decimal GetBalance(string cardNumber)
        {
            if (_accounts.ContainsKey(cardNumber))
            {
                return _accounts[cardNumber].Balance;
            }
            return 0m;
        }

        public bool CheckFunds(string cardNumber, decimal amount)
        {
            return _accounts.ContainsKey(cardNumber) &&
                   _accounts[cardNumber].Balance >= amount;
        }

        public void ExecuteOperation(string operationType, string cardNumber, decimal amount)
        {
            if (_accounts.ContainsKey(cardNumber))
            {
                if (operationType == OperationType.Deposit)
                {
                    _accounts[cardNumber] = (_accounts[cardNumber].EncryptedPin,
                        _accounts[cardNumber].Balance + amount);
                }
                else if (operationType == OperationType.Withdraw)
                {
                    if (_accounts[cardNumber].Balance >= amount)
                    {
                        _accounts[cardNumber] = (_accounts[cardNumber].EncryptedPin,
                            _accounts[cardNumber].Balance - amount);
                    }
                    else
                    {
                        throw new InvalidOperationException("Insufficient funds.");
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException("Account not found.");
            }
        }
    }

}
