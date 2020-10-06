using System;
using System.ComponentModel.DataAnnotations;

namespace AtualisaAniverssario.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        public string Criado { get; set; }
        public DateTime? CriadoData { get; set; }
        public string Modificado { get; set; }
        public DateTime ModificadoData { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Ramal { get; set; }
        public string SetorManualId { get; set; }
        public string CargoId { get; set; }
        public int? SetorResponsavelId { get; set; }
        public int? SetorMembroId { get; set; }
        public int? PAM { get; set; }
        public int? Ativo { get; set; }
        public DateTime? Nascimento { get; set; }
        public string SenhaGoogle { get; set; }

        public Usuarios()
        {
        }

        public Usuarios(int id, string criado, DateTime? criadoData, string modificado, DateTime modificadoData, string nome, string username, string email, string ramal, string setorManualId, string cargoId, int? setorResponsavelId, int? setorMembroId, int? pAM, int? ativo, DateTime? nascimento, string senhaGoogle)
        {
            Id = id;
            Criado = criado;
            CriadoData = criadoData;
            Modificado = modificado;
            ModificadoData = modificadoData;
            Nome = nome;
            Username = username;
            Email = email;
            Ramal = ramal;
            SetorManualId = setorManualId;
            CargoId = cargoId;
            SetorResponsavelId = setorResponsavelId;
            SetorMembroId = setorMembroId;
            PAM = pAM;
            Ativo = ativo;
            Nascimento = nascimento;
            SenhaGoogle = senhaGoogle;
        }
    }
}
