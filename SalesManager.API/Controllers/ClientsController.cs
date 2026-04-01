using AutoMapper;
using DataTransferObjects.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SalesManager.API.Interfaces;
using System.Data;

namespace SalesManager.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientsService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientsService, IMapper mapper)
        {
            _clientsService = clientsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientGetDTO>>> GetClientsAsync([FromQuery] string value, [FromQuery] int idUser, [FromQuery] bool showInactive = true)
        {
            List<ClientGetDTO> clientsGetDTO = await _clientsService.GetClientsAsync(value, idUser, showInactive);

            return Ok(clientsGetDTO);
        }

        [HttpGet]
        [Route("GetClientById/{clientId}")]
        public async Task<ActionResult<ClientGetDTO>> GetClientsByIdAsync([FromRoute] int clientId)
        {
            Client client = await _clientsService.GetClientByIdAsync(clientId);

            if (client == null)
            {
                return NotFound("Nenhum registro encontrado");
            }

            ClientGetDTO clientGetDTO = _mapper.Map<ClientGetDTO>(client);

            return Ok(clientGetDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ClientGetDTO>> PostClientAsync([FromBody] ClientPostDTO clientPostDTO)
        {
            clientPostDTO.ClientCEP = clientPostDTO.ClientCEP.Replace("-", string.Empty);

            if (!int.TryParse(clientPostDTO.ClientCEP, out _))
            {
                return BadRequest("O CEP só pode conter números");
            }

            if (clientPostDTO.ClientCEP.Length != 8)
            {
                return BadRequest("O CEP deve ter 8 números (xxxxx-xxx)");
            }

            if (await _clientsService.ExistsByEmailAsync(clientPostDTO.ClientEmail, clientPostDTO.UserId))
            {
                return BadRequest($"Já existe um email com o nome {clientPostDTO.ClientEmail}");
            }

            Client client = _mapper.Map<Client>(clientPostDTO);
            await _clientsService.InsertAsync(client);

            //ProductGetDTO productGetDTO = new ProductGetDTO();
            //return CreatedAtAction("GetProductById", new { productId = product.Id }, productGetDTO);
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> PutProductAsync([FromBody] ClientPutDTO clientPutDTO)
        {
            Client client = await _clientsService.GetClientByIdAsync(clientPutDTO.Id);

            if (client == null)
            {
                return NotFound($"Nenhum registro encontrado com o id {clientPutDTO.Id}");
            }

            if (await _clientsService.ExistsByEmailUpdateAsync(clientPutDTO.ClientEmail, clientPutDTO.Id, clientPutDTO.UserId))
            {
                return BadRequest($"Já existe um produto com o nome {clientPutDTO.ClientEmail}");
            }

            client.ClientEmail = clientPutDTO.ClientEmail;
            client.ClientCEP = clientPutDTO.ClientCEP;
            client.ClientName = clientPutDTO.ClientName;
            client.ClientAddressCity = clientPutDTO.ClientAddressCity;
            client.ClientAddressState = clientPutDTO.ClientAddressState;
            client.Status = clientPutDTO.Status;

            try
            {
                await _clientsService.UpdateAsync(client);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest($"Erro interno do sistema: {e.Message}");
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{clientId}")]
        public async Task<ActionResult> DeleteClientAsync([FromRoute] int clientId)
        {
            Client client = await _clientsService.GetClientByIdAsync(clientId);

            if (client == null)
            {
                return NotFound($"Nenhum registro encontrado com o id {clientId}");
            }

            try
            {
                if (await _clientsService.ExistsStockMovement(clientId, client.UserId))
                {
                    client.Status = 0;
                    await _clientsService.UpdateAsync(client);
                }
                else
                {
                    await _clientsService.DeleteAsync(client);
                }
            }

            catch (DBConcurrencyException e)
            {
                return BadRequest($"Erro interno do sistema: {e.Message}");
            }

            return NoContent();
        }
    }
}
