using MediatR;

namespace KooliProjekt.Application.Features.Clients
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
