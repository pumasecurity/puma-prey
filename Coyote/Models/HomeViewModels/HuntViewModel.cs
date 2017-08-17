using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coyote.Models.HomeViewModels
{
    public class HuntViewModel : BaseViewModel
    {
        public HuntViewModel()
        {
            this.Animals = new List<AnimalViewModel>();
        }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public virtual List<AnimalViewModel> Animals { get; set; }
    }
}