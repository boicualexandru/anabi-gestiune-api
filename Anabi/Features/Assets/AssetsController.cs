﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Anabi.Features.Assets.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Anabi.Controllers;
using Anabi.Domain.Asset.Commands;
using Anabi.Middleware;

namespace Anabi.Features.Assets
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/Assets")]
    public class AssetsController : BaseController
    {
        private readonly IMediator mediator;
        public AssetsController(IMediator _mediator)
        {
            mediator = _mediator;
        }


        [HttpGet]
        public async Task<IEnumerable<AssetSummary>> Get(SearchAsset filter)
        {
            var results = await mediator.Send(filter);

            return results;
        }



        // GET: api/Assets/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<AssetDetails> Get(int id)
        {
            var model = await mediator.Send(new GetAssetDetails { Id = id });

            return model;
        }


        /// <summary>
        /// Adds a new asset with minimal required details
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns the id of the newly added asset
        /// Validation Errors:
        /// NAME_NOT_EMPTY
        /// NAME_MAX_LENGTH_100 
        /// IDENTIFIER_MAX_LENGTH_100
        /// IDENTIFIER_NOT_EMPTY
        /// INVALID_CATEGORY_ID -- if CategoryId lower than or equal to zero, or the category does not exist
        /// USERCODE_NOT_EMPTY (for test use pop)
        /// USERCODE_MAX_LENGTH_20 -- if the user code is over 20 characters long
        /// INVALID_STAGE_ID -- if StageId lower than or equal to zero, or the stage id does not exist
        /// MEASUREUNIT_MAX_LENGTH_10 -- measure unit max 10 characters
        /// QUANTITY_MUST_BE_GREATER_THAN_ZERO -- quantity must be gt zero
        /// ESTIMATED_AMOUNT_GREATER_THAN_ZERO -- estimated amount must be gt zero
        /// ESTIMATED_AMT_CURRENCY_THREE_CHARS -- currency must have 3 characters (USD, RON, EUR)
        /// </para>
        /// </remarks>
        /// <response code="201">The id of the new asset</response>
        /// <response code="400">In case of validation errors</response>
        /// <response code="500">Server error</response>
        /// <param name="minimalAsset">The details of the new asset to be added</param>
        /// <returns>The id of the new asset</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("addminimalasset")]
        public async Task<IActionResult> AddMinimalAsset([FromBody]AddMinimalAsset minimalAsset)
        {
            var id = await mediator.Send(minimalAsset);
            return Created("api/assets", id);

        }


        /// <summary>
        /// Gets the possible stages for an asset
        /// </summary>
        /// <response code="200">The list of stages</response>
        /// <response code="400"></response>
        /// <response code="500">Server error</response>
        /// <returns>The possible stages</returns>
        [ProducesResponseType(typeof(List<StageViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("stages")]
        public async Task<IActionResult> GetStages()
        {
            var models = await mediator.Send(new GetStages());
            return Ok(models);
        }


        /// <summary>
        /// Gets the parent categories
        /// </summary>
        /// <response code="200">The list of parent categories</response>
        /// <response code="400"></response>
        /// <response code="500">Server error</response>
        /// <returns>The parent categories</returns>
        [ProducesResponseType(typeof(List<CategoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("parentcategories")]
        public async Task<IActionResult> GetParentCategories()
        {
            var models = await mediator.Send(new GetCategories() { ParentsOnly = true });
            return Ok(models);
        }


        /// <summary>
        /// Gets the children categories for a Parent Id
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns the children categories for a Parent Id
        /// Validation Errors:
        /// INVALID_ID
        /// </para>
        /// </remarks>
        /// <response code="200">The list of subcategories</response>
        /// <response code="400"></response>
        /// <response code="500">Server error</response>
        /// <returns>The subcategories</returns>
        [ProducesResponseType(typeof(List<CategoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AnabiExceptionResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("subcategories/{parentId:int}")]
        public async Task<IActionResult> GetSubCategories(int parentId)
        {
            var models = await mediator.Send(new GetCategories() { ParentsOnly = false, ParentId = parentId });
            return Ok(models);
        }
    }
}
