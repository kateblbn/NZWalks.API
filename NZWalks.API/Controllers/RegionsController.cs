using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from Db - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map domain models to DTOs
            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            //return DTOs

            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        { 
            //get rearin domain model from db
            var regionsDomain = await regionRepository.GetByIdAsync(id);
            if (regionsDomain == null)
            {
                return NotFound();
            }
            //map / convert domain moddel to dto
            //var regionsDto = new RegionDto
            //{
            //    Id = regionsDomain.Id,
            //    Name = regionsDomain.Name,
            //    Code = regionsDomain.Code,
            //    RegionImageUrl = regionsDomain.RegionImageUrl
            //};
            var regionsDto = mapper.Map<RegionDto>(regionsDomain);

            return Ok(regionsDto);
        }


        // POST To create New Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequest addRegionRequest)
        {
            //map or conver DTO to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Name = addRegionRequest.Name,
            //    Code = addRegionRequest.Code,
            //    RegionImageUrl = addRegionRequest.RegionImageUrl
            //};

            var regionDomainModel = mapper.Map<Region>(addRegionRequest);

            //use DM to create Region
            await regionRepository.CreateAsync(regionDomainModel);

            //map DM to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }


        //Update Region
        //PUT 
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // map dto to domain
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null) {
                return NotFound();
            }
            //Convert domain model to DTO
            //var regionDto = new RegionDto { 
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};    
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); 

            return Ok(regionDto);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //return deleted Region back
            //map Domain Model DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}
