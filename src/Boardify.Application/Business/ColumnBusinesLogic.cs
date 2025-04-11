using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Interfaces.Business;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Columns;
using Boardify.Domain.Entities;
using FluentValidation;

namespace Boardify.Application.Business
{
    public class ColumnBusinesLogic : IColumnBusinessLogic
    {
        private readonly ICreateCommand<CreateColumnModel, CreateColumnResponseModel> _createColumnCommand;
        private readonly IValidator<CreateColumnModel> _createColumnValidator;
        private readonly IUpdateCommand<UpdateColumnModel, UpdateColumnResponseModel> _updateColumnCommand;
        private readonly IValidator<UpdateColumnModel> _updateColumnValidator;
        private readonly IDeleteCommand<Column> _deleteColumnCommand;
        private readonly IGetAllQuery<ColumnResponseModel> _getAllColumnsQuery;
        private readonly IUpdateColumnOrderCommand _updateColumnOrderCommand;
        private readonly IValidator<UpdateColumnOrderModel> _updateColumnOrderValidator;
        private readonly IResultFactory _resultFactory;

        public ColumnBusinesLogic(ICreateCommand<CreateColumnModel, CreateColumnResponseModel> createColumnCommand,
            IValidator<CreateColumnModel> createColumnValidator,
            IUpdateCommand<UpdateColumnModel, UpdateColumnResponseModel> updateColumnCommand,
            IValidator<UpdateColumnModel> updateColumnValidator,
            IDeleteCommand<Column> deleteColumnCommand,
            IGetAllQuery<ColumnResponseModel> getAllColumnsQuery,
            IUpdateColumnOrderCommand updateColumnOrderCommand,
            IValidator<UpdateColumnOrderModel> updateColumnOrderValidator,
            IResultFactory resultFactory)
        {
            _createColumnCommand = createColumnCommand ?? throw new ArgumentNullException(nameof(createColumnCommand));
            _createColumnValidator = createColumnValidator ?? throw new ArgumentNullException(nameof(createColumnValidator));
            _updateColumnCommand = updateColumnCommand ?? throw new ArgumentNullException(nameof(updateColumnCommand));
            _updateColumnValidator = updateColumnValidator ?? throw new ArgumentNullException(nameof(updateColumnValidator));
            _deleteColumnCommand = deleteColumnCommand ?? throw new ArgumentNullException(nameof(deleteColumnCommand));
            _getAllColumnsQuery = getAllColumnsQuery ?? throw new ArgumentNullException(nameof(getAllColumnsQuery));
            _updateColumnOrderCommand = updateColumnOrderCommand ?? throw new ArgumentNullException(nameof(updateColumnOrderCommand));
            _updateColumnOrderValidator = updateColumnOrderValidator ?? throw new ArgumentNullException(nameof(updateColumnOrderValidator));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<List<ColumnResponseModel>>> UpdateColumnOrder(UpdateColumnOrderModel model)
        {
            var validation = await _updateColumnOrderValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );
                return _resultFactory.Failure<List<ColumnResponseModel>>(errorDictionary);
            }

            var result = await _updateColumnOrderCommand.UpdateColumnOrder(model);
            return result;
        }

        public async Task<IResult<IEnumerable<ColumnResponseModel>>> GetAllColumnsAsync()
        {
            return await _getAllColumnsQuery.GetAllAsync();
        }

        public async Task<IResult<CreateColumnResponseModel>> CreateColumnAsync(CreateColumnModel model)
        {
            var validation = await _createColumnValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );
                return _resultFactory.Failure<CreateColumnResponseModel>(errorDictionary);
            }

            return await _createColumnCommand.Create(model);
        }

        public async Task<IResult<UpdateColumnResponseModel>> UpdateColumnAsync(UpdateColumnModel model)
        {
            var validation = await _updateColumnValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );
                return _resultFactory.Failure<UpdateColumnResponseModel>(errorDictionary);
            }

            return await _updateColumnCommand.Update(model);
        }

        public async Task<IResult<bool>> DeleteColumnAsync(int id)
        {
            return await _deleteColumnCommand.Delete(id);
        }
    }
}