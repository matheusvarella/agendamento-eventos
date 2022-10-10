using System.ComponentModel.DataAnnotations;

namespace AgendamentoEventos.ViewModels
{
    public class BuyTicketViewModel
    {
        [Required(ErrorMessage = "É obrigatório informar o identificador do evento.")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o identificador do participante.")]
        public int ParticipantId { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o nome do participante.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o cpf do participante.")]
        [RegularExpression("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})")]
        public string Cpf { get; set; }
    }
}
