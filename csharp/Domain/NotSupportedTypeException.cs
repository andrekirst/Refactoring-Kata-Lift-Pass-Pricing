namespace Domain;

public class NotSupportedTypeException(string type) : Exception($"{type} is not a supported type for pricing table");