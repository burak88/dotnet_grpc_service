using System;
using AutoMapper;
using Grpc.Core;
using grpcService.Protos;

namespace grpcService.Services.OrderGrpcServices;

public class OrderGrpcServices : Orders.OrdersBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderGrpcServices(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    public override async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, ServerCallContext context)
    {
        var mapRequestToOrder = _mapper.Map<Order>(request);
        var res = await _orderService.CreateOrderAsync(mapRequestToOrder);
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
        var res = await _orderService.UpdateOrderAsync(mapRequestToOrder);
        if (res is not null)
        {
            var mapRes = _mapper.Map<UpdateOrderResponse>(res);
            return await Task.FromResult(mapRes);
        }
        throw new RpcException(new Status(StatusCode.Cancelled, "Edit Order Failed!"));
    }

    public override async Task<DeleteOrderResponse> DeleteOrderAsync(DeleteOrderRequest request, ServerCallContext context)
    {
        var order = await _orderService.GetOrderByIdAsync(request.Id);
        if (order is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "There is no order to delete!"));
        }

        var res = _orderService.DeleteOrderAsync(request.Id);
        if (res == true)
        {
            var mapOrder = _mapper.Map<DeleteOrderResponse>(order);
            return await Task.FromResult(mapOrder);
        }

        throw new RpcException(new Status(StatusCode.Cancelled, "Delete Order Failed!"));
    }

    public override async Task<GetOrderResponse> GetOrderByIdAsync(GetOrderByIdRequest request, ServerCallContext context)
    {
        var order = await _orderService.GetOrderByIdAsync(request.Id);
        if (order is not null)
        {
            var response = _mapper.Map<GetOrderResponse>(order);
            return await Task.FromResult(response);
        }

        throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));
    }

    public override async Task<GetAllOrderResponse> GetOrderAsync(GetOrderRequest request, ServerCallContext context)
    {
        var response = new GetAllOrderResponse();

        var list = await _orderService.GetOrdersAsync();
        foreach (var item in list)
        {
            var mapModel = _mapper.Map<GetOrderResponse>(item);
            response.Order.Add(mapModel);
        }

        return await Task.FromResult(response);
    }
}
