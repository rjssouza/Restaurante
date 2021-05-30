using Module.Service.Interface.Base;

namespace Module.Service.Interface.Utils
{
    public interface IUtilsService : IBaseService
    {
        bool GetByPassConfirmation();
    }
}