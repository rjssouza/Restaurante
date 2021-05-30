using Module.Dto.CustomException;
using Module.Repository.Entity.Base;
using Module.Service.Interface.Utils;
using Module.Service.Validation.Interface.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Module.Service.Validation.Base
{
    public class EntityValidation<TKey, TEntity> : BaseValidation, IEntityValidation<TKey, TEntity>
        where TEntity : BaseEntity<TKey>
    {
        private const string DEFAULT_DELETE_MESSAGE = "Confirma essa deleção?";
        public virtual string DeleteMessage => DEFAULT_DELETE_MESSAGE;
        public IUtilsService UtilsService { get; set; }

        public virtual void ValidateDelete(TEntity entity)
        {
            this.ShowConfirmDeletion();
            this.ValidateEntity(entity);

            this.Validate();
        }

        public virtual void ValidateInsert(TEntity entity)
        {
            this.ValidateEntity(entity);

            this.Validate();
        }

        public virtual void ValidateUpdate(TEntity entity)
        {
            this.ValidateEntity(entity);

            this.Validate();
        }

        protected virtual void ShowConfirmDeletion()
        {
            var mustByPassConfirmation = UtilsService.GetByPassConfirmation();
            if (!mustByPassConfirmation)
                throw new ConfirmationException(this.DeleteMessage);
        }

        private void ValidateEntity(TEntity entity)
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(entity, context, validationResults, true);
            if (!isValid)
            {
                foreach (var result in validationResults)
                    this.AddError(result.MemberNames.FirstOrDefault(), result.ErrorMessage);
            }
        }
    }
}