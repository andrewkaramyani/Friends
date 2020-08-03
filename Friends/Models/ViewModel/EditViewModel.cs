using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Friends.Models.Enums;

namespace Friends.Models.ViewModel
{
    public class EditViewModel : RegisterViewModel
    {
        public string Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
