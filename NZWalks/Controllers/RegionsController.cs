using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{

    // EndPoint : https:localhost:123/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //private readonly NZWalksDBContext _dBContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        #region constructor
        public RegionsController(NZWalksDBContext dBContext,IRegionRepository regionRepository,IMapper mapper)
        {
            //_dBContext = dBContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        } 
        #endregion

        //GET all regions
        //GET : https//localhost:123/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get Data from Database - DOmain Models
            var regions = await _regionRepository.GetAllRegionAsync();
            //Map Domain models to DTOs

            //var regionDto = new List<RegionDto>();

            //foreach (var region in regions)
            //{
            //    regionDto.Add(new RegionDto
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            var regionDto= _mapper.Map<List<RegionDto>>(regions); // Used the Automapper
            //Pass the DTOs
            return Ok(regionDto);
        }

        //Get Region By ID
        //GET:
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetRegionsById ([FromRoute] Guid Id)
        {

            //var region = _dBContext.Regions.Find(Id); // find work only for Primary Key
            //var region = await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            var region = await _regionRepository.GetRegionByIdAsysnc(Id);
            if (region is null)
            {
                return NotFound();
            }
            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = _mapper.Map<RegionDto>(region); // Used Automapper

            return Ok(regionDto);
        }

        //POST : Add the new Region
        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionDto region)
        {
            //var regionDomain = new Region
            //{
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDomain = _mapper.Map<Region>(region);
            regionDomain = await _regionRepository.AddRegionAsync(regionDomain);

            //Map DOmain Model Back to the DTOs
            //var regionDto = new RegionDto {
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            var regionDto = _mapper.Map<RegionDto>(regionDomain); // used automapper
            return CreatedAtAction(nameof(GetRegionsById), new { Id = regionDto.Id},regionDto);
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid Id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //Map region Dto to Region Domain
            //var regionDomain = new Region {
            //    Name = updateRegionDto.Name,
            //    Code=updateRegionDto.Code,
            //    RegionImageUrl=updateRegionDto.RegionImageUrl
            //};
            var regionDomain = _mapper.Map<Region>(updateRegionDto);
            regionDomain = await _regionRepository.UpdateRegionAsync(Id, regionDomain);
            if (regionDomain == null)
                return NotFound();

            //Mao DOmain model to the DTO   
            //var regionDto = new UpdateRegionDto {
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            var regionDto = _mapper.Map<UpdateRegionDto>(regionDomain);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid Id)
        {
           var region= await _regionRepository.DeleteRegionAsync(Id);

            if (region == null)
            {
                return NotFound();
            }
            //Map DomainModel back to the DTo
            //var regionDto = new RegionDto {
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }
    }
}
