using Module.Dto.Base;
using Module.Dto.Enum;

namespace Module.Dto.Configuration
{
    public class DbConnectionDto : BaseDto<int>
    {
        public string ConnectionString { get; set; }

        public TipoConexaoEnum ConnectionType { get; set; }
    }
}