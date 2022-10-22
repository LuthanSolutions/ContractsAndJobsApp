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
        shouldRender = CheckShouldRenderAndSetPropertyFields();
        if (shouldRender)
        {
            base.OnParametersSet();
        }

    }

    private bool CheckShouldRenderAndSetPropertyFields()
    {
        var retVal = ItemId != itemId
            || ItemName != itemName
            || Dob != dob
            || Contact?.Id != contact?.Id;
        if (retVal)
        {
            itemId = ItemId;
            itemName = ItemName;
            dob = Dob;
            contact = Contact;
        }
        return retVal;
    }

    protected override bool ShouldRender() => shouldRender;

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
    }
}