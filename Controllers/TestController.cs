using ARS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Controllers; 

[Route("test")]
public class TestController(UmbrellaTestService umbrellaTestService) : Controller {
    
    public string Test() {
        try {
            return umbrellaTestService.GetUmbrellas().Count.ToString();
        } catch (Exception e) {
            return e.Message;
        }
    }
}