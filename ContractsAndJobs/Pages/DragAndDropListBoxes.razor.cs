﻿namespace ContractsAndJobs.Pages;

public partial class DragAndDropListBoxes
{
    public List<CountryCode> GroupA = new List<CountryCode>
    {
        new CountryCode{ Name = "Australia", Code = "AU" },
        new CountryCode{ Name = "Bermuda", Code = "BM" },
        new CountryCode{ Name = "Canada", Code = "CA" },
        new CountryCode{ Name = "Cameroon", Code = "CM" },
        new CountryCode{ Name = "Denmark", Code = "DK" },
        new CountryCode{ Name = "France", Code = "FR" },
        new CountryCode{ Name = "Finland", Code = "FI" },
        new CountryCode{ Name = "Germany", Code = "DE" },
        new CountryCode{ Name = "Hong Kong", Code = "HK" }};

    public List<CountryCode> GroupB = new List<CountryCode>
    {
        new CountryCode{ Name = "India", Code = "IN" },
        new CountryCode{ Name = "Italy", Code = "IT" },
        new CountryCode{ Name = "Japan", Code = "JP" },
        new CountryCode{ Name = "Mexico", Code = "MX" },
        new CountryCode{ Name = "Norway", Code = "NO" },
        new CountryCode{ Name = "Poland", Code = "PL" },
        new CountryCode{ Name = "Switzerland", Code = "CH" },
        new CountryCode{ Name = "United Kingdom", Code = "GB" },
        new CountryCode{ Name = "United States", Code = "US" }};

    public class CountryCode
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }

    private void OnButtonClicked()
    {
        var countA = GroupA.Count;
    }
}
