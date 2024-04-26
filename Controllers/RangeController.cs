using Microsoft.AspNetCore.Mvc;
using ARS.Services.Interfaces;

namespace ARS.Controllers; 

public class RangeController : Controller {
    private readonly IRangeService _rangeService;
    
    public RangeController(IRangeService rangeService) {
        _rangeService = rangeService;
    }
    
    [Route("range")]
    public List<uint> Range(uint target) {
        return _rangeService.GetRange(target);
    }
}