using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EquilibraMais.Model;

[Table("EMPRESA")]
public class Empresa : IBindableFromHttpContext<Empresa>
{
    public static async ValueTask<Empresa?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (!string.IsNullOrEmpty(context.Request.ContentType) && context.Request.ContentType.Contains("xml"))

        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Empresa));
            return (Empresa?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Empresa>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificado único da Empresa")]
    public int Id { get; set; }
    
    [Column("NOME_EMPRESA")]
    [Description("Nome da empresa")]
    public string Nome_empresa { get; set; } = string.Empty;
}