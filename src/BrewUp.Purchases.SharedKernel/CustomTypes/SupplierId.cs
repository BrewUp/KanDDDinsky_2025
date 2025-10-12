using Muflone.Core;

namespace BrewUp.Purchases.SharedKernel.CustomTypes;

public sealed class SupplierId(string value) : DomainId(value);