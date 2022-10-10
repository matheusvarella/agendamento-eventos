using System;
using System.ComponentModel.DataAnnotations;

namespace AgendamentoEventos.ViewModels
{
    public class EditorEventViewModel
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Este campo deve conter entre 2 e 80 caractéres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O valor do ingresso é obrigatório.")]
        [Range(0, 9999.99, ErrorMessage = "O valor deve estar entre R$ 0,00 e R$ 9999,99")]
        public decimal Value { get; set; }
        
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Este campo deve conter entre 2 e 80 caractéres.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "A data inicial é obrigatória.")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "A data final é obrigatória.")]
        public DateTime FinalDate { get; set; }
    }
}
