namespace ARS.Services;
using Interfaces;

public class RandomGeneratorService : IRandomGeneratorService {
    private uint _currentVal;
    private bool _isNextPositive;

    private int PatchVal(uint randVal) {
        _isNextPositive = !_isNextPositive;
        if (_isNextPositive) {
            return (int) randVal;
        } else {
            return -(int)randVal;
        }
    }
    
    public int Generate(uint maxValue = 100) {
        _currentVal = _currentVal * 632452763 + 2312371;
        return PatchVal(_currentVal % maxValue);
    }
}