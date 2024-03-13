using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CreateWalk
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                //map DTO to domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //mapDomain model to DTO
                return Ok(mapper.Map<WalkDTO>(walkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var walksDomainModel = await walkRepository.GetAllAsync();

            //map to DTO
            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            //map to DTO
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                // map to DTO
                return Ok(mapper.Map<WalkDTO>(walkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            //map domain model
            return Ok(mapper.Map<WalkDTO>(deletedWalkDomainModel));

        }

    }
}
