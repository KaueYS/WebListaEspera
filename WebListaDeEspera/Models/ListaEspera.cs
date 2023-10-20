using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebListaDeEspera.Models
{

    public class ListaEspera
    {
       
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Paciente")]
        public string Nome { get; set; }


        [Required]
        [StringLength(15)]
        [DisplayName("Telefone")]
        public string Telefone { get; set; }


        [Required]
        [StringLength(50)]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }


        [Required]
        [DisplayName("Data do Cadastro")]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Foi Agendado?")]
        public bool Finalizado { get; set; } = false;
    }
}
