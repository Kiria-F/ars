namespace ARS.Services.Interfaces; 

public interface IRandomGeneratorService {
    public int Generate(uint maxValue = 100);
}