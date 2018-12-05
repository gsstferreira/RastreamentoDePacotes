using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class PacoteViewModel
    {
        public Guid PacoteId { get; set; }

        [Required]
        [Display(Name ="Tag RFID")]
        [MinLength(10)]
        public string TagRfid { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime DataPostagem { get; set; }
        public string Remetente { get; set; }

        [Required]
        [Display(Name ="Destinatário")]
        [MinLength(5)]
        public string Destinatario { get; set; }
        public EnderecoViewModel Destino { get; set; }
        public bool Entregue { get; set; }
    }
}