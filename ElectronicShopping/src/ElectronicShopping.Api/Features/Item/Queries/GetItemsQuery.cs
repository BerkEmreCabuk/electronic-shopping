using AutoMapper;
using ElectronicShopping.Api.Features.Item.Models;
using ElectronicShopping.Api.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicShopping.Api.Features.Item.Queries
{
    public class GetItemsQuery : IRequest<List<ItemModel>>
    {
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<ItemModel>>
    {
        private readonly Lazy<IItemRepository> _itemRepository;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(
            Lazy<IItemRepository> itemRepository, 
            IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemModel>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.Value.GetAllAsync(ct: cancellationToken);

            return _mapper.Map<List<ItemModel>>(items);
        }
    }
}
