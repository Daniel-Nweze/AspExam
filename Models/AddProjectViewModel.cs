using Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_One_Examination.Models;


    public class AddProjectViewModel
    {
        public AddProjectFormData FormData { get; set; } = new();
        public List<SelectListItem> Clients { get; set; } = [];
        public List<SelectListItem> Statuses { get; set; } = [];
    }

