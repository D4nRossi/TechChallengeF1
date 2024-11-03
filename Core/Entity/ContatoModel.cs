using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entity
{
    [Table("CONTATO_CTT")]
    public class ContatoModel
    {
        [Key]
        [JsonIgnore]
        [Column("CTT_ID")]
        public int Id { get; set; }

        [Column("CTT_DTCRIACAO")]
        [JsonIgnore]
        public DateTime DataCriacao { get; set; }

        [Column("CTT_NOME")]
        public string ContatoNome { get; set; }

        [Column("CTT_EMAIL")]
        public string ContatoEmail { get; set; }

        [Column("CTT_DDD")]
        [JsonIgnore]
        public int ContatoDDD { get; set; }

        [Column("CTT_NUMERO")]
        public string ContatoNumero { get; set; }
    }
}
