namespace EquilibraMais.Model.dto;

public record SearchDto<T> (string? term, int? page, int totalItems, List<T> data) { }