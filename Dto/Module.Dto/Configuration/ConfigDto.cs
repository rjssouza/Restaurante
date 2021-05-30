using Module.Dto.Base;

namespace Module.Dto.Configuration
{
    public class ConfigDto : BaseDto<int>
    {
        public DbConnectionDto DbConnectionDto { get; set; }
        public DbConnectionDto DbConnectionQueryDto { get; set; }
    }
}