using System.ComponentModel.DataAnnotations;

namespace E_Wolontariat.Models
{
    public class ActionDto
    {

        [Required(ErrorMessage = "Tytuł jest wymagany"), MaxLength(100, ErrorMessage = "Tytuł nie może mieć więcej niż 100 znaków")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Opis jest wymagany"), MinLength(10, ErrorMessage = "Opis musi zawierać co najmniej 10 znaków")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Data jest wymagana")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowy format daty")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
        public string Location { get; set; } = "";
        [Required(ErrorMessage = "Maksymalna liczba uczestników jest wymagana")]
        public int MaxParticipants { get; set; }
    }
}
