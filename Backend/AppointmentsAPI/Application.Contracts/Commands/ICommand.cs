using MediatR;

namespace AppointmentsAPI.Application.Contracts.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand { }

public interface ICommand : IRequest, IBaseCommand { }
