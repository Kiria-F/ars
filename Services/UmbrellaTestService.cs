using ARS.Data;
using ARS.Models;

namespace ARS.Services; 

public class UmbrellaTestService(ApplicationDbContext context) {

    public ICollection<Umbrella> GetUmbrellas() {
        return context.Umbrellas.ToList();
    }
}