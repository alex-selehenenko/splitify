using MediatR;
using Resulty;
using Splitify.BuildingBlocks.EventBus;
using Splitify.EventBus.Contracts;
using Splitify.Redirect.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Application.Commands
{
    public class DeleteRedirectCommandHandler : IRequestHandler<DeleteRedirectCommand, Result>
    {
        private readonly IRedirectRepository _redirectionRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IEventBus _eventBus;

        public DeleteRedirectCommandHandler(
            IRedirectRepository redirectionRepository,
            IDateTimeService dateTimeService,
            IEventBus eventBus)
        {
            _redirectionRepository = redirectionRepository;
            _dateTimeService = dateTimeService;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(DeleteRedirectCommand request, CancellationToken cancellationToken)
        {
            var redirect = await _redirectionRepository.FindAsync(request.RedirectId, cancellationToken);
            if (redirect is null)
            {
                await _eventBus.PublishAsync(new RedirectDeletedMessage(request.RedirectId));
                return Result.Success();
            }

            var result = redirect.DeleteRedirect(_dateTimeService);
            if (result.IsFailure)
            {
                return result;
            }

            _redirectionRepository.Remove(redirect);
            await _redirectionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
