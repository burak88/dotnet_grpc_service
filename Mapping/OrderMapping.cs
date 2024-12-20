using System;
using AutoMapper;
using grpcService.Protos;

namespace grpcService.Mapping;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<CreateOrderRequest, Order>();
        CreateMap<Order, CreateOrderResponse>();

        CreateMap<UpdateOrderRequest, Order>();
        CreateMap<Order, UpdateOrderResponse>();

        CreateMap<Order, DeleteOrderResponse>();
        CreateMap<DeleteOrderResponse, Order>();

        CreateMap<Order, GetOrderResponse>();
        CreateMap<GetOrderResponse, Order>();

    }
}
