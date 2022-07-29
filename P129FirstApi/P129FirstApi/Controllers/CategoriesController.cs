using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P129FirstApi.Data;
using P129FirstApi.Data.Entities;
using P129FirstApi.DTOs.CatagoryDTOs;
using P129FirstApi.Interfaces;
using P129FirstApi.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P129FirstApi.Controllers
{
    /// <summary>
    /// Category Controller
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        /// <summary>
        /// Categor Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="jWTManager"></param>
        public CategoriesController(AppDbContext context, IMapper mapper, IJWTManager jWTManager)
        {
            _context = context;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        /// <summary>
        /// Category Yaranmasi Ucun Service
        /// </summary>
        /// <param name="categoryPostDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CategoryPostDto categoryPostDto)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];

            var userName = _jWTManager.GetUserNameByToken(token);

            if (categoryPostDto.AidOlduguUstCategoryId != null && !await _context.Categories.AnyAsync(c => c.Id == categoryPostDto.AidOlduguUstCategoryId && c.IsMain))
            {
                return BadRequest("ParentId Is InCorrect");
            }

            if (await _context.Categories.AnyAsync(c=>!c.IsDeleted && c.Name.ToLower() == categoryPostDto.Ad.Trim().ToLower()))
            {
                return Conflict($"Category {categoryPostDto.Ad} Is Already Exists");
            }

            //Category category = new Category();

            //category.Name = categoryPostDto.Ad.Trim();
            //category.CreatedAt = DateTime.UtcNow.AddHours(4);
            //category.IsMain = categoryPostDto.Esasdirmi;
            //category.ParentId = categoryPostDto.AidOlduguUstCategoryId;
            //category.Image = categoryPostDto.Esasdirmi ? categoryPostDto.Sekli : "1.jpg";

            Category category = _mapper.Map<Category>(categoryPostDto);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            //CategoryGetDto categoryGetDto = new CategoryGetDto
            //{
            //    Ad = category.Name,
            //    Sekil = category.Image,
            //    AidOlduguUstCategoryId = category.ParentId,
            //    Esasdirmi = category.IsMain,
            //    Id = category.Id
            //};

            CategoryGetDto categoryGetDto = _mapper.Map<CategoryGetDto>(category);

            return StatusCode(201, categoryGetDto);
        }

        /// <summary>
        /// Gets the list of all Categories.
        /// </summary>
        /// <returns>The list of Employees.</returns>
        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //Old Mapping
            //List<Category> categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            //List<CategoryListDto> categoryListDtos = new List<CategoryListDto>();

            //foreach (Category category in categories)
            //{
            //    CategoryListDto categoryListDto = new CategoryListDto
            //    {
            //        Id = category.Id,
            //        Ad = category.Name
            //    };

            //    categoryListDtos.Add(categoryListDto);
            //}

            //Old Mapping
            //List<CategoryListDto> categoryListDtos = await _context.Categories
            //    .Where(c => !c.IsDeleted)
            //    .Select(x=>new CategoryListDto
            //    {
            //        Id = x.Id,
            //        Ad = x.Name
            //    })
            //    .ToListAsync();

            //List<CategoryListDto> categoryListDtos = _mapper.Map<List<CategoryListDto>>(await _context.Categories.Where(c => !c.IsDeleted).ToListAsync());

            return Ok(_mapper.Map<List<CategoryListDto>>(await _context.Categories.Where(c => !c.IsDeleted).ToListAsync()));
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            CategoryGetDto categoryGetDto = await _context.Categories
                .Where(c => !c.IsDeleted && c.Id == id)
                .Select(x=>new CategoryGetDto 
                {
                    Id = x.Id,
                    Ad = x.Name,
                    AidOlduguUstCategoryId = x.ParentId,
                    Esasdirmi = x.IsMain,
                    Sekil = x.Image
                })
                .FirstOrDefaultAsync();

            if (categoryGetDto == null) return NotFound("Id Is InCorrect");

            //CategoryGetDto categoryGetDto = new CategoryGetDto
            //{
            //    Id = category.Id,
            //    Ad = category.Name,
            //    AidOlduguUstCategoryId = category.ParentId,
            //    Esasdirmi = category.IsMain,
            //    Sekil = category.Image
            //};

            return Ok(categoryGetDto);
        }

        /// <summary>
        /// Categoriyani Yenilemek Ucun Service
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/categories
        ///     {        
        ///       "ad": "Mike",
        ///       "esasdirmi": "Andrew",
        ///       "aidOlduguUstCategoryId": "Mike.Andrew@gmail.com",
        ///       "sekli":"Test.jpg"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="categoryPutDto"></param>
        /// <returns>A newly created employee</returns>
        /// <response code="204">Updated Successfuly category</response>
        /// <response code="400">If the item is null</response>  
        /// <response code="404">If the item is NotFount</response>  
        /// <response code="409">Name Is Alreade Exists</response>  
        [HttpPut]
        [Route("{id?}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [Produces("application/json")]
        public async Task<IActionResult> Put(int? id,CategoryPutDto categoryPutDto)
        {
            if (id == null) return BadRequest("id Is required");

            if (categoryPutDto.Id != id) return BadRequest("Id Is Not Mathed By Category Object");

            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);

            if (dbCategory == null) return NotFound("Id Is InCorrect");

            if (categoryPutDto.AidOlduguUstCategoryId != null && !await _context.Categories.AnyAsync(c => c.Id == categoryPutDto.AidOlduguUstCategoryId && c.IsMain))
            {
                return BadRequest("AidOlduguUstCategoryId Is InCorrect");
            }

            if (await _context.Categories.AnyAsync(c => !c.IsDeleted && c.Id != id &&  c.Name.ToLower() == categoryPutDto.Ad.Trim().ToLower()))
            {
                return Conflict($"Category {categoryPutDto.Ad} Is Already Exists");
            }

            dbCategory.Name = categoryPutDto.Ad.Trim();
            dbCategory.IsMain = categoryPutDto.Esasdirmi;
            dbCategory.ParentId = categoryPutDto.AidOlduguUstCategoryId;
            dbCategory.Image = categoryPutDto.AidOlduguUstCategoryId != null? "1.jpg":categoryPutDto.Sekli;
            dbCategory.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest("id Is required");

            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);

            if (dbCategory == null) return NotFound("Id Is InCorrect");

            dbCategory.IsDeleted = true;
            dbCategory.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpOptions]
        [Route("restore/{id?}")]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return BadRequest("id Is required");

            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted && c.Id == id);

            if (dbCategory == null) return NotFound("Id Is InCorrect");

            dbCategory.IsDeleted = false;
            dbCategory.DeletedAt = null;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
