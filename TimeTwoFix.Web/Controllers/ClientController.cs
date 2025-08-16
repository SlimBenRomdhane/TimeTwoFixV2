using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTwoFix.Application.ClientServices.Dtos;
using TimeTwoFix.Application.ClientServices.Interfaces;
using TimeTwoFix.Core.Entities.ClientManagement;
using TimeTwoFix.Core.Interfaces;
using TimeTwoFix.Web.Models.ClientModels;

namespace TimeTwoFix.Web.Controllers
{
    [Authorize(Roles = "FrontDeskAssistant,GeneralManager")]
    public class ClientController : Controller
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IClientServices _clientServices;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper, IClientServices clientServices, ILogger<ClientController> logger)
        {
            //_unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientServices = clientServices;
            _logger = logger;
        }

        // GET: ClientController
        public async Task<IActionResult> Index(int pageNumber, int pageSize)
        {
            try
            {//var clients = await _clientServices.GetAllAsyncServiceGeneric();
                var clients = await _clientServices.GetAllActiveClientsAsync();
                var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(clients);
                var clientsViewModel = _mapper.Map<IEnumerable<ReadClientViewModel>>(clientsDto);
                if (clientsViewModel == null || !clientsViewModel.Any())
                {
                    TempData["ClientError"] = "No records found ";
                    return View(Enumerable.Empty<ReadClientViewModel>());
                }

                return View(clientsViewModel);

            }
            catch (Exception)
            {


                TempData["ClientError"] = "An Error occurred while loading clients";
                return View(Enumerable.Empty<ReadClientViewModel>());
            }

        }

        [Authorize(Roles = "GeneralManager")]
        public async Task<IActionResult> LoadDeleted()
        {
            var clients = await _clientServices.GetAllDeletedClients();
            var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(clients);
            var clientsViewModel = _mapper.Map<IEnumerable<ReadClientViewModel>>(clientsDto);

            return View(clientsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchName, string searchPhone, string searchEmail)
        {
            var clients = await _clientServices.GetClientByMultipleParam(searchName, searchPhone, searchEmail);

            var clientsDto = _mapper.Map<IEnumerable<ReadClientDto>>(clients);
            var clientsViewModel = _mapper.Map<IEnumerable<ReadClientViewModel>>(clientsDto);
            return View(clientsViewModel);
        }

        // GET: ClientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var client = await _clientServices.GetByIdAsyncServiceGeneric(id, c => c.Vehicles);
                if (client == null)
                {
                    return NotFound();
                }
                //var vehicleList = await _unitOfWork.Vehicles.GetVehiclesByClientIdAsync(client.Id);

                var clientDto = _mapper.Map<ReadClientDto>(client);
                var clientViewModel = _mapper.Map<ReadClientViewModel>(clientDto);
                return View(clientViewModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel createClientViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createClientViewModel);
                }



                // Check if the email already exists
                var existingClient = await _clientServices.GetClientByEmail(createClientViewModel.Email);
                if (existingClient != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(createClientViewModel);
                }

                var clientDto = _mapper.Map<CreateClientDto>(createClientViewModel);
                var client = _mapper.Map<Client>(clientDto);
                client.CreatedBy = User.Identity?.Name;
                var addedElement = await _clientServices.AddAsyncServiceGeneric(client);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientServices.GetByIdAsyncServiceGeneric(id);
            var clientDto = _mapper.Map<UpdateClientDto>(client);
            var clientView = _mapper.Map<UpdateClientViewModel>(clientDto);

            return View(clientView);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateClientViewModel updateClientViewModel)
        {
            try
            {
                var client = await _clientServices.GetByIdAsyncServiceGeneric(updateClientViewModel.Id);
                if (client == null)
                {
                    return NotFound();
                }
                var clientDto = _mapper.Map<UpdateClientDto>(updateClientViewModel);

                var updatedClient = _mapper.Map(clientDto, client);
                //updatedClient.UpdatedAt = DateTime.Now;
                updatedClient.UpdatedBy = User.Identity?.Name;
                await _clientServices.UpdateAsyncServiceGeneric(updatedClient);
                //await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(int id)
        {
            var client = await _clientServices.GetByIdAsyncServiceGeneric(id);
            if (client == null)
            {
                return NotFound();
            }
            var clientDto = _mapper.Map<DeleteClientDto>(client);
            var clientView = _mapper.Map<DeleteClientViewModel>(clientDto);
            return View(clientView);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GeneralManager")]
        public async Task<ActionResult> Delete(DeleteClientViewModel deleteClientViewModel)
        {
            var clientToDetete = await _clientServices.GetByIdAsyncServiceGeneric(deleteClientViewModel.Id);
            if (clientToDetete == null)
            {
                return NotFound();
            }
            clientToDetete.IsDeleted = true;
            clientToDetete.DeletedAt = DateTime.UtcNow;
            clientToDetete.DeletedBy = User.Identity?.Name;

            //await _unitOfWork.SaveChangesAsync();
            await _clientServices.AttachAsyncServiceGeneric(clientToDetete, EntityState.Modified);
            //var a = await _clientServices.SaveChangesServiceGeneric();
            //await _clientServices.UpdateAsyncServiceGeneric(clientToDetete);
            await _clientServices.SaveChangesServiceGeneric();
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "GeneralManager")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                var deletedClientDto = await _clientServices.GetDeletedClientByIdAsync(id);
                if (deletedClientDto == null)
                    return NotFound();
                var client = _mapper.Map<Client>(deletedClientDto);
                await _clientServices.DeleteAsyncServiceGeneric(deletedClientDto.Id);

                client.IsDeleted = false;
                client.DeletedAt = null;

                await _clientServices.UpdateAsyncServiceGeneric(client);

                return RedirectToAction(nameof(LoadDeleted)); // Or return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to restore client ID {ClientId}. Error: {ErrorMessage}", id, ex.Message);
                return BadRequest("An error occurred while restoring the client.");
            }
        }

        [Authorize(Roles = "GeneralManager")]
        public async Task<IActionResult> DeletePermanently(int id)
        {
            try
            {
                var x = await _clientServices.GetByIdAsyncServiceGeneric(id);
                //await _clientServices.AttachAsyncServiceGeneric(x, EntityState.Deleted);
                await _clientServices.DetachAsyncServiceGeneric(x);
                await _clientServices.DeleteAsyncServiceGeneric(x.Id);
                await _clientServices.SaveChangesServiceGeneric();

                return RedirectToAction(nameof(LoadDeleted));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to permanently delete client ID {ClientId}. Error: {ErrorMessage}", id, ex.Message);
                return BadRequest("An error occurred while restoring the client.");
            }
        }
    }
}