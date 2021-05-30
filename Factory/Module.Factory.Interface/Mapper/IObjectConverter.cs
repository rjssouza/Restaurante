using Module.Factory.Interface.Base;

namespace Module.Factory.Interface.Mapper
{
    public interface IObjectConverter : IBaseFactory
    {
        T ConvertTo<T>(object source)
            where T : class;
    }
}