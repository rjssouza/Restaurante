using Module.Dto.Configuration;

namespace Module.IoC.Register.Interfaces.LifeCycle
{
    public interface IContainerLifeCycle
    {
        ConfigDto Config { get; }
    }
}