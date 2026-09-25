using MediatR;
using ShopApplication.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Commands.Category;

public record CreateCategoryCommand(string Name, string Slug, string Url, int? ParentId) : IRequest<int>;