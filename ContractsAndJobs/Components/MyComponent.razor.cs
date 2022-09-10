using ContractsAndJobs.Models;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Components;

public partial class MyComponent
{
    [Parameter]
    public int ItemId { get; set; }

    [Parameter]
    public string? ItemName { get; set; }

    [Parameter]
    public DateTime? Dob { get; set; }

    [Parameter]
    public Contact? Contact { get; set; }

    private int itemId;
    private string? itemName;
    private DateTime? dob;
    private Contact? contact;
    private bool shouldRender = true;

    public (int Id, string? Name) Item { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        this.shouldRender = this.CheckShouldRenderAndSetPropertyFields();
        if (this.shouldRender)
        {
            base.OnParametersSet();
        }

    }

    private bool CheckShouldRenderAndSetPropertyFields()
    {
        var retVal = this.ItemId != this.itemId 
            || this.ItemName != this.itemName 
            || this.Dob != this.dob 
            || this.Contact?.Id != this.contact?.Id;
        if (retVal)
        {
            this.itemId = this.ItemId;
            this.itemName = this.ItemName;
            this.dob = this.Dob;
            this.contact = this.Contact;
        }
        return retVal;
    }

    protected override bool ShouldRender() => this.shouldRender;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
    }
}