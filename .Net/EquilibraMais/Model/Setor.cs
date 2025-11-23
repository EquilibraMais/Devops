using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EquilibraMais.Model;

[Table("SETOR")]
public class Setor : IBindableFromHttpContext<Setor>
{
    public static async ValueTask<Setor?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (!string.IsNullOrEmpty(context.Request.ContentType) && context.Request.ContentType.Contains("xml"))

        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Setor));
            return (Setor?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Setor>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificado único do Setor")]
    [JsonIgnore]
    public int Id { get; set; }
    
    [Column("DESCRICAO")]
    [Description("Descrição do Setor")]
    public string Descricao { get; set; } = string.Empty;
    
    [Column("EMPRESA_ID")]
    [Description("Identificador único da Empresa que o Setor pertence")]
    public int Empresa_id { get; set; }
    [JsonIgnore]
    public Empresa Empresa { get; set; }
}