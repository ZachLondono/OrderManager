using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Triggers;

public class FireTrigger {

	public record Command(Trigger Trigger) : IRequest;

	internal class Handler : IRequestHandler<Command> {

		private readonly TriggerController _controller;

		public Handler(TriggerController controller) {
			_controller = controller;
        }

		public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {

			// Get alll registered triggers for the current type
			var triggers = await _controller.GetTriggersByType(request.Trigger.Type);

            if (request.Trigger is null || request.Trigger.Order is null) throw new InvalidDataException("");

			// Create data model from dynamic parameters
            dynamic model = TriggerController.GenerateExpandedModel(request.Trigger.Order);

			// Execute the actions with the generated model
            foreach (var trigger in triggers)
				trigger?.Action?.DoAction(request.Trigger.Type, model);

			return new Unit();

		}

    }

}