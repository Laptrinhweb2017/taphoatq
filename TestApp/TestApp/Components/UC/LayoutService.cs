using Microsoft.AspNetCore.Components;

public class LayoutService
{
    public Guid Key { get; private set; } = Guid.NewGuid();
    public RenderFragment? FormTemplate { get; private set; }

    public event Action? OnChange;

    public void SetForm(RenderFragment? content)
    {
        Key = Guid.NewGuid();
        FormTemplate = null;         // reset
        OnChange?.Invoke();

        Key = Guid.NewGuid();
        FormTemplate = content;      // set lại
        OnChange?.Invoke();
    }
}
