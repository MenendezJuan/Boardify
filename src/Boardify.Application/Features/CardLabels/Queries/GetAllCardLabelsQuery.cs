using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.CardLabels;

namespace Boardify.Application.Features.CardLabels.Queries
{

    public class GetAllCardLabelsQuery : IGetAllQuery<CardLabelModel>
    {
        private readonly ICardLabelRepository _cardLabelRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetAllCardLabelsQuery(
            IMapper mapper,
            ICardLabelRepository cardLabelRepository,
            IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cardLabelRepository = cardLabelRepository ?? throw new ArgumentNullException(nameof(cardLabelRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<CardLabelModel>>> GetAllAsync()
        {
            try
            {
                var cardLabels = await _cardLabelRepository.GetAllAsync();

                if (cardLabels == null || !cardLabels.Any())
                {
                    return _resultFactory.Failure<IEnumerable<CardLabelModel>>("No se encontraron etiquetas de tarjetas.");
                }

                var cardLabelModels = _mapper.Map<IEnumerable<CardLabelModel>>(cardLabels);

                return _resultFactory.Success(cardLabelModels);
            }
            catch (Exception ex)
            {
                return _resultFactory.Failure<IEnumerable<CardLabelModel>>($"Error interno: {ex.Message}");
            }
        }
    }
}