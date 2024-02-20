namespace ARS.Services;

public class ClientTestService : IClientTestService {
    private bool _flag = false;

    public uint Trigger() {
        _flag = !_flag;
        return _flag ? 0U : 1U;
    }
}