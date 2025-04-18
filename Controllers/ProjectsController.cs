using ASP_One_Examination.Models;
using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace ASP_One_Examination.Controllers
{
    [Authorize]
    public class ProjectsController(IProjectService projectService, IBaseRepository<ClientEntity> baseRepository, UserManager<UserEntity> userManager, IStatusService statusService) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;

        private readonly IProjectService _projectService = projectService;
        private readonly IStatusService _statusService = statusService;
        private readonly IBaseRepository<ClientEntity> _baseRepository = baseRepository;


        public async Task<IActionResult> Index(string status = "all")
        {
            var result = await _projectService.GetAllProjectsAsync();
            if (!result.Succeeded || result.Data == null)
            {
                ViewBag.ErrorMessage = result.Error ?? "Kunde inte hämta projekt.";
                return View(new List<ProjectEntity>());
            }

            var projects = result.Data?.ToList();

            if (status == "started")
            {
                var startedStatus = await _statusService.GetStatusByNameAsync("Started");
                if (startedStatus.Succeeded && startedStatus.Data != null)
                    projects = projects?.Where(p => p.StatusId == startedStatus.Data.Id).ToList();
            }

            else if (status == "completed")
            {
                var completedStatus = await _statusService.GetStatusByNameAsync("Completed");
                if (completedStatus.Succeeded && completedStatus.Data != null)
                    projects = projects?.Where(p => p.StatusId == completedStatus.Data.Id).ToList();
            }


            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var clients = await _baseRepository.GetAllAsync();
            var statuses = await _statusService.GetAllStatusesAsync();
            var viewModel = new AddProjectViewModel
            {
                Clients = clients.Data?
                .Select(c => new SelectListItem { Value = c.Id, Text = c.ClientName })
                .ToList() ?? [], // Om data är null ändå, skicka tom lista istället för krasch. Chatgpt genererad

                Statuses = statuses.Data?
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StatusName })
                    .ToList() ?? []

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
               
                var clients = await _baseRepository.GetAllAsync();
                var statuses = await _statusService.GetAllStatusesAsync();

                viewModel.Clients = clients.Data?
                    .Select(c => new SelectListItem { Value = c.Id, Text = c.ClientName })
                    .ToList() ?? [];


                viewModel.Statuses = statuses.Data?
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StatusName })
                    .ToList() ?? [];


                return View(viewModel);
            }


            viewModel.FormData.UserId = _userManager.GetUserId(User)!;
            var result = await _projectService.CreateProjectAsync(viewModel.FormData);
            if (!result.Succeeded)
            {
                ViewBag.ErrorMessage = result.Error;

                var clients = await _baseRepository.GetAllAsync();
                viewModel.Clients = clients.Data?
                    .Select(c => new SelectListItem { Value = c.Id, Text = c.ClientName })
                    .ToList() ?? [];

                return View(viewModel);

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _projectService.GetProjectAsync(id);
            if (!result.Succeeded || result.Data == null)
                return RedirectToAction("Index");



            var clients = await _baseRepository.GetAllAsync();

            var viewModel = new AddProjectViewModel
            {

                FormData = new AddProjectFormData
                {
                    ProjectName = result.Data.ProjectName,
                    Description = result.Data.Description,
                    StartDate = result.Data.StartDate,
                    EndDate = result.Data.EndDate,
                    Budget = result.Data.Budget,
                    ClientId = result.Data.ClientId,
                    StatusId = result.Data.StatusId,
                    UserId = result.Data.UserId,
                    Image = result.Data.Image
                },
                Clients = clients.Data?.Select(c => new SelectListItem { Value = c.Id, Text = c.ClientName })
                    .ToList() ?? []


            };

            var statuses = await _statusService.GetAllStatusesAsync();
            viewModel.Statuses = statuses.Data?
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StatusName })
                .ToList() ?? [];


            ViewBag.ProjectId = result.Data.Id;
            ViewBag.Clients = viewModel.Clients;
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AddProjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var statuses = await _statusService.GetAllStatusesAsync();
                ViewBag.Statuses = statuses.Data?.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StatusName }).ToList();
                viewModel.Statuses = statuses.Data?
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StatusName })
                    .ToList() ?? [];       

                var clients = await _baseRepository.GetAllAsync();
                viewModel.Clients = clients.Data?
                    .Select(c => new SelectListItem { Value = c.Id, Text = c.ClientName })
                    .ToList() ?? [];

                return View(viewModel);
            }

            var projectEntity = viewModel.FormData.ToEntity(viewModel.FormData.StatusId);
            projectEntity.Id = id;

            var result = await _projectService.UpdateProjectAsync(projectEntity);
            if (!result.Succeeded)
            {
                ViewBag.ErrorMessage = result.Error;
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var projectResult = await _projectService.GetProjectAsync(id);
            if (!projectResult.Succeeded || projectResult.Data == null)
            {
                ViewBag.ErrorMessage = projectResult.Error;
                return RedirectToAction("Index");
            }

            var deleteResult = await _projectService.DeleteProjectAsync(projectResult.Data);
            if (!deleteResult.Succeeded)
            {
                ViewBag.ErrorMessage = deleteResult.Error;
                return RedirectToAction("Index");
            }

            ViewBag.Succeeded = projectResult.Succeeded;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var result = await _projectService.GetProjectAsync(id);
            if (!result.Succeeded || result.Data == null)
                return RedirectToAction("Index");

            var viewModel = result.Data.ToDetailsViewModel();
            return View(viewModel);
        }
    }
}