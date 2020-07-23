using AutoMapper;
using Chat.API.Model;
using Chat.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected new IActionResult Ok()
        {
            _unitOfWork.Commit();
            return base.Ok();
        }

        protected IActionResult Ok<T>(T result)
        {
            _unitOfWork.Commit();
            return base.Ok(result);
        }

        protected IActionResult Created<T>(T result, string redirectUri = "")
        {
            _unitOfWork.Commit();
            return base.Created(redirectUri, result);
        }

        protected IActionResult BadRequest(string message)
        {
            _unitOfWork.RollBack();
            return BadRequest(new ErrorModel() { Message = message });
        }
    }
}
