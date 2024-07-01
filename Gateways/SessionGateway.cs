using ARS.Models;

namespace ARS.Gateways; 

public class SessionGateway {
    private readonly SortedSet<long> _sessions = new();
    
    public bool Add(long userId) {
        return _sessions.Add(userId);
    }
    
    public bool Remove(long userId) {
        return _sessions.Remove(userId);
    }

    public SortedSet<long> GetAll() {
        return _sessions;
    }
}