using System;
using AutoMapper;
using Grpc.Core;
using grpcService.Contracts;
using grpcService.Protos;

namespace grpcService.Services.OrderGrpcServices;

public class OrderGrpcServices : Orders.OrdersBase
{
     private readonly IService<Order> _orderService;
    private readonly IMapper _mapper;

    public OrderGrpcServices(IService<Order> orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    public override async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, ServerCallContext context)
    {
        var mapRequestToOrder = _mapper.Map<Order>(request);
        var res = await _orderService.Create(mapRequestToOrder);
        if (res is not null)
        {
            var mapRes = _mapper.Map<CreateOrderResponse>(res);
            return await Task.FromResult(mapRes);
        }
        throw new RpcException(new Status(StatusCode.Cancelled, "Create Order Failed!"));
    }

    public override async Task<UpdateOrderResponse> UpdateOrderAsync(UpdateOrderRequest request, ServerCallContext context)
    {
        var mapRequestToOrder = _mapper.Map<Order>(request);
        var res = await _orderService.Update(mapRequestToOrder);
        if (res is not null)
        {
            var mapRes = _mapper.Map<UpdateOrderResponse>(res);
            return await Task.FromResult(mapRes);
        }
        throw new RpcException(new Status(StatusCode.Cancelled, "Edit Order Failed!"));
    }

    public override async Task<DeleteOrderResponse> DeleteOrderAsync(DeleteOrderRequest request, ServerCallContext context)
    {
        var order = await _orderService.GetById(request.Id);
        if (order is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "There is no order to delete!"));
        }

        var res = await _orderService.Delete(request.Id);
        if (res == true)
        {
            var mapOrder = _mapper.Map<DeleteOrderResponse>(order);
            return await Task.FromResult(mapOrder);
        }

        throw new RpcException(new Status(StatusCode.Cancelled, "Delete Order Failed!"));
    }

    public override async Task<GetOrderResponse> GetOrderByIdAsync(GetOrderByIdRequest request, ServerCallContext context)
    {
        var Order = await _orderService.GetById(request.Id);
        if (Order is not null)
        {
            var mapOrder = _mapper.Map<GetOrderResponse>(Order);
            return await Task.FromResult(mapOrder);
        }
        throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));
    }

    public override async Task<GetAllOrderResponse> GetOrderAsync(GetOrderRequest request, ServerCallContext context)
    {
        var response = new GetAllOrderResponse();

        var list = await _orderService.GetAll();
        foreach (var item in list)
        {
            var mapModel = _mapper.Map<GetOrderResponse>(item);
            response.Order.Add(mapModel);
        }

        return await Task.FromResult(response);
    }
}
