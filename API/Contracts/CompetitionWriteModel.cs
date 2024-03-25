﻿namespace API.Contracts;

public class CompetitionWriteModel
{
    public required string Name { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required string Location { get; set; }
}