using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractionAssignment
{
    public class SessionManager
    {
        private Dictionary<string, DateTime> _activeSessions = new Dictionary<string, DateTime>();

        public bool IsSessionActive(string sessionId)
        {
            if (_activeSessions.ContainsKey(sessionId) &&
                _activeSessions[sessionId] > DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public void EndSession(string sessionId)
        {
            if (_activeSessions.ContainsKey(sessionId))
            {
                _activeSessions.Remove(sessionId);
            }
        }

        public string StartSession()
        {
            string sessionId = Guid.NewGuid().ToString();
            _activeSessions[sessionId] = DateTime.Now.AddMinutes(5); // 5-minute session timeout
            return sessionId;
        }
    }
}
