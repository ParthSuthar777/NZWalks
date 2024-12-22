using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    //api/walks/
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepository _walksRepository;

        public WalksController(IMapper mapper,IWalksRepository walksRepository)
        {
            _mapper = mapper;
            _walksRepository = walksRepository;
        }
        // Create Walks
        [HttpPost]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalksRequestDto addWalksRequest)
        {
            // Map Dto to Domain model
            var walksDomainModel = _mapper.Map<Walks>(addWalksRequest);
            walksDomainModel= await _walksRepository.AddWalkAsync(walksDomainModel);
            //Map Domain MOdel to DTo
            var walksDto = _mapper.Map<WalksDto>(walksDomainModel);
            return Ok(walksDto);
        }
        // GET Walks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
           var walks= await _walksRepository.GetAllWalksAsync();
            return Ok(_mapper.Map<List<WalksDto>>(walks));
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid Id)
        {
            var walk = await _walksRepository.GetWalkByIdAsync(Id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalksDto>(walk));
        }

        //Update Walks By ID
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateWalksById([FromRoute]Guid Id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {
            //Map Dto to Domain
            var walk = _mapper.Map<Walks>(updateWalksRequestDto);
            walk=await _walksRepository.UpdateWalkAsync(Id, walk);
            if (walk == null)
                return NotFound();
            //Map Domain Model to the Dto
            return Ok(_mapper.Map<WalksDto>(walk));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute]Guid Id)
        {
           var walk= await _walksRepository.DeleteAsysnc(Id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalksDto>(walk));
        }
    }
}
