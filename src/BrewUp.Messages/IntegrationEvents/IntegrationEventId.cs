using Muflone.Core;

namespace BrewUp.Messages.IntegrationEvents;

public sealed class IntegrationEventId(Guid value) : DomainId(value.ToString());
