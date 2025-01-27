using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionAssignment
{
    public class ATMSystem
    {
        private Bank _bank = new Bank();
        private SessionManager _sessionManager = new SessionManager();
        private EncryptionService _encryptionService = new EncryptionService();

        public string AuthenticateUser(string cardNumber, string enteredPin)
        {
            string encryptedPin = _encryptionService.Encrypt(enteredPin);
            if (_bank.VerifyCredentials(cardNumber, encryptedPin))
            {
                return _sessionManager.StartSession(); // Create and return a new session ID
            }
            return string.Empty;
        }

        public bool IsSessionActive(string sessionId)
        {
            return _sessionManager.IsSessionActive(sessionId);
        }

        public void EndSession(string sessionId)
        {
            _sessionManager.EndSession(sessionId);
        }

        public bool AuthorizeTransaction(string cardNumber, decimal amount)
        {
            return _bank.CheckFunds(cardNumber, amount);
        }

        public void PerformBankOperation(string operationType, string cardNumber, decimal amount)
        {
            _bank.ExecuteOperation(operationType, cardNumber, amount);
        }

        public decimal GetBalance(string cardNumber)
        {
            return _bank.GetBalance(cardNumber);
        }
    }

}
