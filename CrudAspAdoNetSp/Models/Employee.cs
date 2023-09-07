using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAspAdoNetSp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Apellido")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("Fecha de nacimiento")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Correo")]
        [Required]
        public string Email { get; set; }

        [DisplayName("Salario")]
        [Required]
        public double Salary { get; set; }

        [NotMapped]
        public string FullName {
            get { return FirstName + " " + LastName; }
        }
    }
}
