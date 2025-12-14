using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TTMS.Models.Task.Dtos;

namespace TTMS.Domains.Task.Commands
{
    public class CreateTaskCommand(CreateTaskDto Dto, Guid CreatedByUserId) : IRequest<Guid>
    {
        public CreateTaskDto Dto { get; } = Dto;
        public Guid CreatedByUserId { get; } = CreatedByUserId;
    }
}
