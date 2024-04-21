using ARS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Controllers; 

public class GeneratorController : Controller {
    private readonly IRandomGeneratorService _randomGeneratorService;
    
    public GeneratorController(IRandomGeneratorService randomGeneratorService) {
        _randomGeneratorService = randomGeneratorService;
    }
    
    [Route("gen")]
    public int Gen() {
        return _randomGeneratorService.Generate(100);
    }
}