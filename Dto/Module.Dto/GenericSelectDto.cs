using Module.Dto.Base;

namespace Module.Dto
{
    public class GenericSelectDto<TKey> : BaseDto<TKey>
    {
        public string Text { get; set; }
    }
}