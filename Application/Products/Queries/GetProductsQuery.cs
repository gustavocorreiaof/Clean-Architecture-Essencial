using Domain.Entities;
using MediatR;

namespace Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
         
    }
}
