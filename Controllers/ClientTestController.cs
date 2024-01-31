using ARS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Controllers; 

[Route("test")]
public class ClientTestController : Controller {
    
    private readonly IClientTestService _clientTestService;
    
    public ClientTestController(IClientTestService clientTestService) {
        _clientTestService = clientTestService;
    }
    
    // GET
    public uint Index() {
        return _clientTestService.Trigger();
    }
}