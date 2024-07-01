using ARS.Gateways;
using ARS.Models;

namespace ARS.Services;

public class SessionService(
    UserGateway userGateway,
    SessionGateway sessionGateway) {
    public bool OpenSession(long userId) {
        return sessionGateway.Add(userId);
    }

    public bool CloseSession(long userId) {
        return sessionGateway.Remove(userId);
    }

    public IEnumerable<UserModel> GetActiveUsers() {
        return userGateway.GetUsersByIds(sessionGateway.GetAll());
    }
}