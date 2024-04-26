namespace ARS.Services; 
using Interfaces;

public class RangeService : IRangeService {
    
    private readonly IRandomGeneratorService _randomGeneratorService;
    
    public RangeService(IRandomGeneratorService randomGeneratorService) {
        _randomGeneratorService = randomGeneratorService;
    }
    
    public List<uint> GetRange(uint target) {
        var result = new List<uint>();
        for (var i = 0U; i < target; i++) {
            result.Add((uint)_randomGeneratorService.Generate(50) + 50);
        }
        return result;
    }
}