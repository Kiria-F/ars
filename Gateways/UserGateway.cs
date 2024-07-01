using ARS.Data;
using ARS.Models;

namespace ARS.Gateways;
// TODO: use async
public class UserGateway(ApplicationDbContext context) {
    public UserModel AddUser(UserModel user) {
        var addedEntity = context.Users.Add(user);
        context.SaveChanges();
        return addedEntity.Entity;
    }

    public void UpdateUser(UserModel user) {
        context.Users.Update(user);
        context.SaveChanges();
    }

    public void DeleteUser(UserModel user) {
        context.Users.Remove(user);
        context.SaveChanges();
    }

    public UserModel? GetUserByUsername(string username) {
        return context.Users.FirstOrDefault(u => u.Username == username);
    }

    public UserModel? GetUserById(long id) {
        return context.Users.FirstOrDefault(u => u.Id == id);
    }

    public ISet<UserModel> GetUsersByIds(ISet<long> userIds) {
        return context.Users.Where(u => userIds.Contains(u.Id)).ToHashSet();
    }
}