using MediatR;
using ShopApplication.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Commands.Product;

public record DeleteProductByIdCommand(int id) : IRequest<int>;
