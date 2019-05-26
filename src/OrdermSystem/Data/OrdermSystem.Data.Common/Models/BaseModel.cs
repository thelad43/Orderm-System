namespace OrdermSystem.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}