using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(
            IOrderService orderService
            )
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllOrders(int pageIndex = 1, int pageSize = 10)
        {
            var res = await _orderService.GetAllOrderAsync(pageIndex, pageSize);
            return Ok(res);
        }
    }
}