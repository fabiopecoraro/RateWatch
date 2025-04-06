namespace RateWatch.Domain.Entities;

public class SystemState
{
    public string Key { get; set; } = null!;

    /// <summary>
    /// Stato booleano associato alla chiave.
    /// </summary>
    public bool IsSet { get; set; }

    /// <summary>
    /// Data e ora dell'ultimo aggiornamento del flag.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
