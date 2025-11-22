using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace EquilibraMais.Model;

[Table("USUARIO")]
public class Usuario : IBindableFromHttpContext<Usuario>
{
    public static async ValueTask<Usuario?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (!string.IsNullOrEmpty(context.Request.ContentType) && context.Request.ContentType.Contains("xml"))

        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Usuario));
            return (Usuario?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Usuario>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificado único do Usuário")]
    public int Id { get; set; }

    [Column("NOME")]
    [Description("Nome do Usuário")]
    public string Nome { get; set; } = string.Empty;
    
    [Column("CARGO")]
    [Description("Cargo do Usuário")]
    public string Cargo { get; set; } = string.Empty;
    
    [Column("SETOR_ID")]
    [Description("Identificador único do Setor que o Usuário pertence")]
    public int Setor_id { get; set; }
    
    public required Setor Setor { get; set; }
}