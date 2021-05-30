using System;

namespace Module.Dto.Base
{
    public class BaseDto<TKey>
    {
        public TKey Id { get; set; }
    }
}