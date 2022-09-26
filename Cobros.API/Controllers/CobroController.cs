﻿using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Model.Authorize;
using Cobros.API.Core.Model.DTO.Cobro;
using Cobros.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cobros.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CobrosController : ControllerBase
    {
        private readonly ICobroBusiness _cobroBusiness;

        public CobrosController(ICobroBusiness cobroBusiness)
        {
            _cobroBusiness = cobroBusiness;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cobroBusiness.GetCobroById(id);
            return Ok(result);
        }

        [Authorize(Role.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CobroUpdateDto cobroUpdateDto)
        {
            await _cobroBusiness.UpdateCobro(id, cobroUpdateDto);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CobroCreateDto cobroCreateDto)
        {
            throw new NotImplementedException();
        }
    }
}
