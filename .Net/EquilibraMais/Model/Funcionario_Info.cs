using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EquilibraMais.Model;

[Table("FUNCIONARIO_INFO")]
public class Funcionario_Info : IBindableFromHttpContext<Funcionario_Info>
{
    public static async ValueTask<Funcionario_Info?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        if (!string.IsNullOrEmpty(context.Request.ContentType) && context.Request.ContentType.Contains("xml"))

        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, context.RequestAborted);
            var serializer = new XmlSerializer(typeof(Funcionario_Info));
            return (Funcionario_Info?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return await context.Request.ReadFromJsonAsync<Funcionario_Info>();
    }
    
    [Column("ID")]
    [Key]
    [Description("Identificado único das informações do funcionário")]
    public int Id { get; set; }
    
    [Column("HUMOR")]
    [Description("Medidor de 1 a 5 do grau de humor do funcionário")]
    public int Humor { get; set; }
    
    [Column("ENERGIA")]
    [Description("Medidor de 1 a 5 do grau de energia do funcionário")]
    public int Energia { get; set; }
    
    [Column("CARGA")]
    [Description("Medidor de 1 a 5 do grau de carga de trabalho do funcionário")]
    public int Carga { get; set; }
    
    [Column("SONO")]
    [Description("Medidor de 1 a 5 do grau de sono do funcionário")]
    public int Sono { get; set; } 

    [Column("OBSERVACAO")]
    [Description("Campo para observações do funcionário")]
    public string Observacao { get; set; } = string.Empty;
    
    [Column("HISTORICO_MEDICO")]
    [Description("Campo para adicionar possível histórico médico do funcionário")]
    public string? Historico_medico { get; set; }
    
    [Column("USUARIO_ID")]
    [Description("Identificador único do Usuario que o referencia as descrições do funcionário")]
    public int Usuario_id { get; set; }
    
    [Column("DATA")]
    [Description("Data em que o check foi feito")]
    public DateTime Data { get; set; }
    
    public required Usuario Usuario { get; set; }
    
}