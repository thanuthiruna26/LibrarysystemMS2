using api_library.DTOs;
using api_library.Interfaces;
using api_library.Models;
using api_library.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTOs;
using ProductAPI.Models;

namespace api_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _repository;
 

        public MemberController(IMemberRepository repository)
        {
            _repository = repository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            var member = await _repository.GetAllMembersAsync();
            return Ok(member);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int NIC)
        {
            var member = await _repository.GetMemberByIdAsync(NIC);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult> AddMember(MemberDto memberDto)
        {
            var member = new Member
            {
                NIC = memberDto.NIC,
                FirstName = memberDto.FirstName,
                LastName = memberDto.LastName,
                RegisteredDate = memberDto.RegisteredDate,
               
            };
            await _repository.AddMemberAsync(member);
            return Ok(member);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember(int NIC, MemberDto memberDto)
        {
            var member = await _repository.UpdateMemberAsync(NIC);
            if (member == null) return NotFound();

            member.NIC = memberDto.NIC;
            member.FirstName = memberDto.FirstName;
            member.LastName = memberDto.LastName;
            member.RegisteredDate = memberDto.RegisteredDate;

            await _repository.UpdateMemberAsync(member);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> MemberBook(int NIC)
        {
            var result = await _repository.DeleteMemberAsync(NIC);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}

