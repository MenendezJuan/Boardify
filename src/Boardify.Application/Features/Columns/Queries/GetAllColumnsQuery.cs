using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Columns.Queries
{
    public class GetAllColumnsQuery : IGetAllQuery<ColumnResponseModel>
    {
        private readonly IQueryRepository<Column> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetAllColumnsQuery(IQueryRepository<Column> queryRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<ColumnResponseModel>>> GetAllAsync()
        {
            try
            {
                var columns = await _queryRepository.GetAllAsync();
                var columnResponseModels = _mapper.Map<IEnumerable<ColumnResponseModel>>(columns);
                return _resultFactory.Success(columnResponseModels);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<IEnumerable<ColumnResponseModel>>($"An error occurred while retrieving columns: {ex.Message}");
            }
        }
    }
}