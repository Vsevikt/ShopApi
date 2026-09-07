using MediatR;
using ShopApplication.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Queries.Product;

public record GetProductByIdQuery(int id) : IRequest<ProductReadDTO?>;

