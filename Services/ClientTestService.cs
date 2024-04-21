namespace ARS.Services;
using Interfaces;

public class ClientTestService : IClientTestService {
    private bool _flag;

    public uint Trigger() {
        _flag = !_flag;
        return _flag ? 0U : 1U;
    }
}